using Microsoft.EntityFrameworkCore;

namespace Shared.Data.Repositories;

public class AbstractBaseRepository<T>(T context) where T : DbContext
{
    public T Context { get; } = context;
}
