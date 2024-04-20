using System.ComponentModel.DataAnnotations;

namespace API.Data.Entities;

public sealed class StudentEntity : AbstractBaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}
