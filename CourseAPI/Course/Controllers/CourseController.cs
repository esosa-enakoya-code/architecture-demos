using Microsoft.AspNetCore.Mvc;
using Course.Shared.RequestDTOs;
using MassTransit;
using Course.Shared.MessageContracts;
using Course.Shared.ResponseDTOs;

namespace Course.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CourseController(IBus bus) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id) 
    {
        var client = bus.CreateRequestClient<ICourseGetContract>();
        return Ok(await client.GetResponse<CourseGetResponseDTO>(new { Id = id }));
    }

    [HttpPost("{id}/[action]/{studentId}")]
    public async Task<IActionResult> Enroll([FromRoute] int id, [FromRoute] int studentId) 
    {
        var client = bus.CreateRequestClient<ICourseEnrollContract>();
        return Ok(await client.GetResponse<CourseEnrollResponseDTO>(new { CourseId = id, StudentId = studentId }));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] CourseCreateRequestDTO request) 
    {
        var client = bus.CreateRequestClient<ICourseCreateContract>();
        return Ok(await client.GetResponse<CourseCreateResponseDTO>(request));
    }
}
