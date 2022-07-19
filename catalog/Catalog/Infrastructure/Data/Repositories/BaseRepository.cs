
using System.Linq.Expressions;
using Catalog.Domain.Utils;
using Microsoft.EntityFrameworkCore;

public interface IRepository<TEntity> where TEntity : class
{
    Task<Result> Add(TEntity entity);
    Result Remove(TEntity entity);
    Task<Result<TEntity>> Get(string id);
    Result<IEnumerable<TEntity>> GetAll();
    Result<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
}

public class Repository<TEntity> : IRepository<TEntity> where TEntity: class
{
    protected readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public async Task<Result<TEntity>> Get(string id)
    {
        try
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            return Result.Ok(entity);
        }
        catch
        {
            return Result.Fail<TEntity>("Could not retrive in database");
        }
    }

    public Result<IEnumerable<TEntity>> GetAll()
    {
        try
        {
            IEnumerable<TEntity> entityList = _context.Set<TEntity>().ToList();
            return Result.Ok(entityList);
        }
        catch
        {
            return Result.Fail<IEnumerable<TEntity>>("Could not retrieve entity list from database");
        }
    }

    public Result<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            IEnumerable<TEntity> entityList = _context.Set<TEntity>().Where(predicate);
            return Result.Ok(entityList);
        }
        catch
        {
            return Result.Fail<IEnumerable<TEntity>>("Could not retrieve entity list from database");
        }
    }

    public async Task<Result> Add(TEntity entity)
    {
        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return Result.Ok();
        }
        catch
        {
            return Result.Fail("Could not add to database");
        }
        
    }

    public Result Remove(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Remove(entity);;
            return Result.Ok();
        }
        catch
        {
            return Result.Fail("Could not remove from database");
        }
    }
}