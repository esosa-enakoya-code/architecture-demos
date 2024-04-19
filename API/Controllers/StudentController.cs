using API.DTOs.Student;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(GetType().Name);
    }

    [HttpPost("[action]")]
    public IActionResult Create([FromBody] StudentCreateRequestDTO request)
    {
        return Ok(GetType().Name);
    }
}
