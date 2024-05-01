﻿using AutoMapper;
using Course.Shared.Services;
using Course.Data.Repositories;
using Course.Domain;
using Course.Shared.DTOs.RequestDTOs;
using Course.Shared.DTOs.ResponseDTOs;
using Student.Shared.Services;

namespace Course.Services;

public class CourseService(IMapper mapper, ICourseRepository repository, Lazy<IStudentService> studentService) : ICourseService
{
    public async Task<CourseGetResponseDTO> GetAsync(int id)
    {
        var course = await repository.GetAsync(id);
        var studentBatchResponse = await studentService.Value.GetBatchAsync([.. course.StudentIds]);

        var response = mapper.Map<CourseGetResponseDTO>(course);
        response.StudentNames = studentBatchResponse.Students.Select(s => s.Name).ToList();

        return response;
    }

    public async Task<CourseEnrollResponseDTO> EnrollAsync(CourseEnrollRequestDTO newEnrollment)
    {
        var studentResponse = await studentService.Value.GetAsync(newEnrollment.StudentId);
        var course = await repository.AddStudentToCourseAsync(newEnrollment.Id, studentResponse.Id);
        await studentService.Value.EnrollStudentAsync(studentResponse.Id, course.Id);

        var studentBatchResponse = await studentService.Value.GetBatchAsync([.. course.StudentIds]);
        var response = mapper.Map<CourseEnrollResponseDTO>(course);
        response.StudentNames = studentBatchResponse.Students.Select(s => s.Name).ToList();

        return response;
    }

    public async Task<CourseCreateResponseDTO> CreateAsync(CourseCreateRequestDTO newCourse)
    {
        var course = mapper.Map<CourseDomain>(newCourse);
        course = await repository.CreateAsync(course);
        return mapper.Map<CourseCreateResponseDTO>(course);
    }
}