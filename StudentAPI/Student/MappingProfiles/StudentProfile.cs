using AutoMapper;
using Student.Data.Entities;
using Student.Domain;
using Student.Shared.MessageContracts;
using Student.Shared.ResponseDTOs;

namespace Student.MappingProfiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<StudentDomain, StudentEntity>();
        CreateMap<StudentEntity, StudentDomain>();

        CreateMap<StudentDomain, StudentGetResponseDTO>();

        CreateMap<IStudentCreateContract, StudentDomain>();
        CreateMap<StudentDomain, StudentCreateResponseDTO>();

        CreateMap<StudentDomain, StudentEnrollResponseDTO>();

        CreateMap<List<StudentDomain>, StudentGetBatchResponseDTO>()
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src));
    }
}
