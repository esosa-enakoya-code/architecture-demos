using Microsoft.AspNetCore.Mvc;
using Course.Shared.DTOs.RequestDTOs;
using Course.Services;

namespace Course.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CourseController(ICourseService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id) 
        => Ok(await service.GetAsync(id));

    [HttpPost("{id}/[action]/{studentId}")]
    public async Task<IActionResult> Enroll([FromRoute] int id, [FromRoute] int studentId) 
        => Ok(await service.EnrollAsync(new CourseEnrollRequestDTO(id, studentId)));

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] CourseCreateRequestDTO request) 
        => Ok(await service.CreateAsync(request));
}
