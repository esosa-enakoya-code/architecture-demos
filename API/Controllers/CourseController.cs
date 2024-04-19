using API.DTOs.Course;
using API.DTOs.Student;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(GetType().Name);
    }

    [HttpPost("{id}/[action]/{studentId}")]
    public IActionResult Enroll([FromRoute] int id, [FromRoute] int studentId)
    {
        return Ok(GetType().Name);
    }

    [HttpPost("[action]")]
    public IActionResult Create([FromBody] CourseCreateRequestDTO request)
    {
        return Ok(GetType().Name);
    }
}
