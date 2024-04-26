using AutoMapper;
using Course.Shared.DTOs.ResponseDTOs;
using Shared.Util;
using Student.Data.Repositories;
using Student.Domain;
using Student.Shared.DTOs.RequestDTOs;
using Student.Shared.DTOs.ResponseDTOs;

namespace Student.Services;
public class StudentService(IMapper mapper, IStudentRepository repository, IHttpClientFactory httpClientFactory) : IStudentService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CourseAPI");

    public async Task<StudentGetResponseDTO> GetAsync(int id)
    {
        var student = await repository.GetAsync(id);
        var course = await _httpClient.FetchAsync<CourseCreateResponseDTO>(HttpMethod.Get, $"course/{id}");

        var response = mapper.Map<StudentGetResponseDTO>(student);
        response.CourseName = course?.Name;
        return response;
    }

    public async Task<StudentGetBatchResponseDTO> GetBatchAsync(StudentGetBatchRequestDTO request)
    {
        var students = await repository.GetBatchAsync(request.Ids);
        return mapper.Map<StudentGetBatchResponseDTO>(students);
    }

    public async Task<StudentCreateResponseDTO> CreateAsync(StudentCreateRequestDTO studentCreateRequest)
    {
        var student = mapper.Map<StudentDomain>(studentCreateRequest);
        student = await repository.CreateAsync(student);
        return mapper.Map<StudentCreateResponseDTO>(student);
    }

    public async Task<StudentEnrollResponseDTO> EnrollStudentAsync(int studentId, int courseId)
    {
        var students = await repository.AddCourseToStudentAsync(studentId, courseId);
        return mapper.Map<StudentEnrollResponseDTO>(students);
    }
}
