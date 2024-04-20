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
        CreateMap<StudentEntity, Student>();

        CreateMap<Student, StudentGetResponseDTO>();

        CreateMap<StudentCreateRequestDTO, Student>();
        CreateMap<Student, StudentCreateResponseDTO>();
    }
}
