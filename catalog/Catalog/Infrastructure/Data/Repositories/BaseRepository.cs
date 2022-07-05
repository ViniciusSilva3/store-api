
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    TEntity Get(string id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
}

public class Repository<TEntity> : IRepository<TEntity> where TEntity: class
{
    protected readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public TEntity Get(string id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().Where(predicate);
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
}