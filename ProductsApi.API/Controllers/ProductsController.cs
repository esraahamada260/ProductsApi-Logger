using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.Application.DTOs;
using ProductsApi.Application.Products.Commands;
using ProductsApi.Application.Products.Queries;

namespace ProductsApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var products = await _sender.Send(
            new GetProductsQuery(),
            cancellationToken);

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var product = await _sender.Send(
            new GetProductByIdQuery(id),
            cancellationToken);

        if (product is null)
        {
            return NotFound(new
            {
                message = $"Product with ID {id} was not found."
            });
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product);
    }
}