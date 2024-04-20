using API.DTOs.StudentDTOs;
using API.Services.StudentServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StudentController(IMapper mapper, IStudentService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var student = await service.GetAsync(id);
        return Ok(mapper.Map<StudentGetResponseDTO>(student));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] StudentCreateRequestDTO request)
    {
        var student = await service.CreateAsync(request);
        return Ok(mapper.Map<StudentCreateResponseDTO>(student));
    }
}
