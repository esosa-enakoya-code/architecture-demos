using AutoMapper;
using Course.Domain;
using Course.Data.Entities;
using Course.Shared.ResponseDTOs;
using Course.Shared.MessageContracts;

namespace Course.MappingProfiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CourseDomain, CourseEntity>();
        CreateMap<CourseEntity, CourseDomain>();

        CreateMap<CourseDomain, CourseGetResponseDTO>();

        CreateMap<ICourseCreateContract, CourseDomain>();
        CreateMap<CourseDomain, CourseCreateResponseDTO>();

        CreateMap<CourseDomain, CourseEnrollResponseDTO>();
    }
}
