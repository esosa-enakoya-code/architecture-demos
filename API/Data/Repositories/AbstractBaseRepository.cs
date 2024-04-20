namespace API.Data.Repositories;

public abstract class AbstractBaseRepository(BaseDbContext context)
{
    protected BaseDbContext Context { get; } = context;
}
