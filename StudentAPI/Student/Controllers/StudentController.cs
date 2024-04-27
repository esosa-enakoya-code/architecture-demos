using Microsoft.AspNetCore.Mvc;
using MassTransit;
using Student.Shared.MessageContracts;
using Student.Shared.ResponseDTOs;
using Student.Shared.RequestDTOs;

namespace Student.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StudentController(IBus bus) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var client = bus.CreateRequestClient<IStudentGetContract>();
        return Ok(await client.GetResponse<StudentGetResponseDTO>(new { Id = id }));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] StudentCreateRequestDTO request) {
        var client = bus.CreateRequestClient<IStudentCreateContract>();
        return Ok(await client.GetResponse<StudentCreateResponseDTO>(request));
    }
}
