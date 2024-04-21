using AutoMapper;
using Course.Domain;
using Course.Data.Entities;
using Course.Shared.DTOs.ResponseDTOs;
using Course.Shared.DTOs.RequestDTOs;

namespace Course.MappingProfiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CourseDomain, CourseEntity>();
        CreateMap<CourseEntity, CourseDomain>();

        CreateMap<CourseDomain, CourseGetResponseDTO>();

        CreateMap<CourseCreateRequestDTO, CourseDomain>();
        CreateMap<CourseDomain, CourseCreateResponseDTO>();

        CreateMap<CourseDomain, CourseEnrollResponseDTO>();
    }
}
