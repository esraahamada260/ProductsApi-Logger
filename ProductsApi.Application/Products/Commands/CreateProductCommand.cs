using MediatR;
using ProductsApi.Application.DTOs;

namespace ProductsApi.Application.Products.Commands;

public record CreateProductCommand(
    string Name,
    decimal Price
) : IRequest<ProductDto>;