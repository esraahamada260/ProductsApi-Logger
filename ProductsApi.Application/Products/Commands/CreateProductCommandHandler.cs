using MediatR;
using ProductsApi.Application.DTOs;
using ProductsApi.Application.Interfaces;
using ProductsApi.Domain.Entities;

namespace ProductsApi.Application.Products.Commands;

public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        var createdProduct = await _productRepository.AddAsync(
            product,
            cancellationToken);

        return new ProductDto
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Price = createdProduct.Price
        };
    }
}