using AutoMapper;
using Course.Data.Repositories;
using Course.Domain;
using Course.Shared.DTOs.RequestDTOs;
using Course.Shared.DTOs.ResponseDTOs;
using Shared.Util;
using Student.Shared.DTOs.RequestDTOs;
using Student.Shared.DTOs.ResponseDTOs;

namespace Course.Services;

public class CourseService(IMapper mapper, ICourseRepository repository, IHttpClientFactory httpClientFactory) : ICourseService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("StudentAPI");

    public async Task<CourseGetResponseDTO> GetAsync(int id)
    {
        var course = await repository.GetAsync(id);
        var studentBatchRequest = new StudentGetBatchRequestDTO([.. course.StudentIds]);
        var studentBatchResponse = await _httpClient.FetchAsync<StudentGetBatchResponseDTO>(HttpMethod.Get, studentBatchRequest, $"student/batch");

        var response = mapper.Map<CourseGetResponseDTO>(course);
        response.StudentNames = studentBatchResponse?.Students.Select(s => s.Name).ToList() ?? [];

        return response;
    }

    public async Task<CourseEnrollResponseDTO> EnrollAsync(CourseEnrollRequestDTO newEnrollment)
    {
        var studentResponse = (await _httpClient.FetchAsync<StudentGetResponseDTO>(HttpMethod.Get, $"student/{newEnrollment.StudentId}"))
            ?? throw new KeyNotFoundException($"No student with id '{newEnrollment.StudentId}' found");
        var course = await repository.AddStudentToCourseAsync(newEnrollment.Id, studentResponse.Id);

        _ = (await _httpClient.FetchAsync<StudentGetResponseDTO>(HttpMethod.Post, $"student/{studentResponse.Id}/enroll/{course.Id}"))
            ?? throw new KeyNotFoundException($"No student with id '{newEnrollment.StudentId}' found");

        var studentBatchRequest = new StudentGetBatchRequestDTO([.. course.StudentIds]);
        var studentBatchResponse = await _httpClient.FetchAsync<StudentGetBatchResponseDTO>(HttpMethod.Get, studentBatchRequest, $"student/batch");

        var response = mapper.Map<CourseEnrollResponseDTO>(course);
        response.StudentNames = studentBatchResponse?.Students.Select(s => s.Name).ToList() ?? [];

        return response;
    }

    public async Task<CourseCreateResponseDTO> CreateAsync(CourseCreateRequestDTO newCourse)
    {
        var course = mapper.Map<CourseDomain>(newCourse);
        course = await repository.CreateAsync(course);
        return mapper.Map<CourseCreateResponseDTO>(course);
    }
}
