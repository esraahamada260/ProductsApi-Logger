using MediatR;
using ProductsApi.Application.DTOs;

namespace ProductsApi.Application.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;