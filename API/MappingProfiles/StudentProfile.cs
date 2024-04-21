using API.Data.Entities;
using API.Domain;
using API.DTOs.StudentDTOs;
using AutoMapper;

namespace API.MappingProfiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentEntity>();
        CreateMap<StudentEntity, Student>()
            .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course));

        CreateMap<Student, StudentGetResponseDTO>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course!.Name))
            .ForMember(dest => dest.CourseName, opt => opt.Condition(src => src.Course is not null));

        CreateMap<StudentCreateRequestDTO, Student>();
        CreateMap<Student, StudentCreateResponseDTO>();
    }
}
