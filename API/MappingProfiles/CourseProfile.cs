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
        CreateMap<CourseEntity, Course>()
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));

        CreateMap<Course, CourseGetResponseDTO>()
            .ForMember(dest => dest.StudentNames, opt => opt.MapFrom(src => src.Students.Select(s => s.Name)));

        CreateMap<CourseCreateRequestDTO, Course>();
        CreateMap<Course, CourseCreateResponseDTO>();
            
        CreateMap<Course, CourseEnrollResponseDTO>()
            .ForMember(dest => dest.StudentNames, opt => opt.MapFrom(src => src.Students.Select(s => s.Name)));
    }
}
