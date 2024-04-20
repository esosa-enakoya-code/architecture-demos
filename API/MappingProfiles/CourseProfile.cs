using API.Data.Entities;
using API.Domain;
using API.DTOs.CourseDTOs;
using AutoMapper;

namespace API.MappingProfiles;

public class CourseProfile : Profile
{
    public CourseProfile() 
    {
        CreateMap<Course, CourseEntity>();
        CreateMap<CourseEntity, Course>();

        CreateMap<Course, CourseGetResponseDTO>();

        CreateMap<CourseCreateRequestDTO, Course>();
        CreateMap<Course, CourseCreateResponseDTO>();
    }
}
