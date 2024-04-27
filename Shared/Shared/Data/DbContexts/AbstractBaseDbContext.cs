using Microsoft.EntityFrameworkCore;

namespace Shared.Data.DbContexts;

public abstract class AbstractBaseDbContext<T> : DbContext where T : DbContext
{
    protected AbstractBaseDbContext()
    {
    }

    protected AbstractBaseDbContext(DbContextOptions<T> options)
        : base(options)
    {
    }
}
