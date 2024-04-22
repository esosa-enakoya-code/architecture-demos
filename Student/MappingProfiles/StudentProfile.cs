using AutoMapper;
using Student.Domain;
using Student.Data.Entities;
using Student.Shared.DTOs.ResponseDTOs;
using Student.Shared.DTOs.RequestDTOs;

namespace Student.MappingProfiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<StudentDomain, StudentEntity>();
        CreateMap<StudentEntity, StudentDomain>();

        CreateMap<StudentDomain, StudentGetResponseDTO>();

        CreateMap<StudentCreateRequestDTO, StudentDomain>();
        CreateMap<StudentDomain, StudentCreateResponseDTO>();

        CreateMap<StudentDomain, StudentEnrollResponseDTO>();

        CreateMap<List<StudentDomain>, StudentGetBatchResponseDTO>()
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src));
    }
}
