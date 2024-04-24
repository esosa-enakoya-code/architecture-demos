using Microsoft.AspNetCore.Mvc;
using Student.Shared.DTOs.RequestDTOs;
using Student.Services;

namespace Student.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StudentController(IStudentService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id) 
        => Ok(await service.GetAsync(id));

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] StudentCreateRequestDTO request) 
        => Ok(await service.CreateAsync(request));
}
