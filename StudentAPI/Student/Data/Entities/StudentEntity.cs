using Shared.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Student.Data.Entities;

public sealed class StudentEntity : AbstractBaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public int? CourseId { get; set; }
}
