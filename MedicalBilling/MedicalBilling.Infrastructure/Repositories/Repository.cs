using MedicalBilling.Domain.Interfaces;
using MedicalBilling.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalBilling.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly HealthDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(HealthDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null) { _dbSet.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public virtual async Task<bool> ExistsAsync(int id) => await _dbSet.FindAsync(id) != null;
}
