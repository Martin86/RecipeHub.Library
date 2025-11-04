using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RecipeHub.Library.Data;

namespace RecipeHub.Library.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly RecipeHubDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(RecipeHubDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public RecipeHubDbContext GetDbContext() => _context;
    public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public async Task AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
