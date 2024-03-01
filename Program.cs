
using ApiBiblioteca.Application.Mapping;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Infra.Data;
using ApiBiblioteca.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration to use a file with the connection string
            builder.Configuration.AddJsonFile("env.json", optional: false, reloadOnChange: true);

            // Add services to the container.
            // Dependency injection repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();

            // Dependency injection AutoMapper
            builder.Services.AddAutoMapper(typeof(DomainToDTOMapping));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Configuration for connecting to the database.
            builder.Services.AddDbContext<Context>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("BibliotecaApiDb"))
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/error-development");
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
