using MediatR;
using ProductsApi.Application.DTOs;

namespace ProductsApi.Application.Products.Queries;

public record GetProductsQuery : IRequest<List<ProductDto>>;