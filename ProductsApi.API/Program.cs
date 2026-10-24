using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsApi.API.Exceptions;
using ProductsApi.Application.Behaviors;
using ProductsApi.Application.Interfaces;
using ProductsApi.Application.Products.Commands;
using ProductsApi.Application.Validators;
using ProductsApi.Infrastructure.Data;
using ProductsApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssembly(
        typeof(CreateProductCommand).Assembly));

builder.Services.AddScoped<
    IValidator<CreateProductCommand>,
    CreateProductCommandValidator>();

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

var app = builder.Build();
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();