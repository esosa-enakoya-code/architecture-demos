using Shared.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Course.Data.Entities;

public sealed class CourseEntity : AbstractBaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public List<int> StudentIds { get; set; } = [];
}
