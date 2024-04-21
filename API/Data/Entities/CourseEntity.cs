using System.ComponentModel.DataAnnotations;

namespace API.Data.Entities;

public sealed class CourseEntity : AbstractBaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public List<StudentEntity> Students { get; set; } = [];
}
