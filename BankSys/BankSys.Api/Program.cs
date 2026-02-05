using BankSys.Application.Interfaces;
using BankSys.Application.Services;
using BankSys.Application.Validators;
using BankSys.Domain.Interfaces;
using BankSys.Persistence.Context;
using BankSys.Persistence.Repositories;
using BankSys.Persistence.UoW;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IContaBancariaService, ContaBancariaService>();
builder.Services.AddScoped<ITransferenciaBancariaRepository, TransferenciaBancariaRepository>();
builder.Services.AddScoped<IContaBancariaRepository, ContaBancariaRepository>();
builder.Services.AddScoped<IUoW, UoW>();

builder.Services.AddValidatorsFromAssemblyContaining<CriarContaBancariaValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddDbContext<BankSysContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
