using API.DTOs.CourseDTOs;
using API.Services.CourseServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CourseController(IMapper mapper, ICourseService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var course = await service.GetAsync(id);
        return Ok(mapper.Map<CourseGetResponseDTO>(course));
    }

    [HttpPost("{id}/[action]/{studentId}")]
    public IActionResult Enroll([FromRoute] int id, [FromRoute] int studentId)
    {
        return Ok(GetType().Name);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] CourseCreateRequestDTO request)
    {
        var course = await service.CreateAsync(request);
        return Ok(mapper.Map<CourseCreateResponseDTO>(course));
    }
}
