using Microsoft.EntityFrameworkCore;
using ProductsApi.Application.Interfaces;
using ProductsApi.Domain.Entities;
using ProductsApi.Infrastructure.Data;

namespace ProductsApi.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
private readonly AppDbContext _context;
public ProductRepository(AppDbContext context)
{
    _context = context;
}

public async Task<List<Product>> GetAllAsync(
    CancellationToken cancellationToken)
{
    return await _context.Products
        .AsNoTracking()
        .ToListAsync(cancellationToken);
}

public async Task<Product?> GetByIdAsync(
    int id,
    CancellationToken cancellationToken)
{
    return await _context.Products
        .FirstOrDefaultAsync(
            product => product.Id == id,
            cancellationToken);
}

public async Task<Product> AddAsync(
    Product product,
    CancellationToken cancellationToken)
{
    _context.Products.Add(product);

    await _context.SaveChangesAsync(cancellationToken);

    return product;
}
}