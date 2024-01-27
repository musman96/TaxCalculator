
using Microsoft.EntityFrameworkCore;
using TaxCalculationAPI.Data;
using TaxCalculationAPI.Repositories;
using TaxCalculationAPI.Services;

namespace TaxCalculationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddDbContext<TaxCalculationDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AssignmentCS")));

            // Add services to the container.
            builder.Services.AddTransient<ITaxCalculator, ProgressiveTaxCalculator>();
            builder.Services.AddTransient<ITaxCalculator, FlatValueTaxCalculator>();
            builder.Services.AddTransient<ITaxCalculator, FlatRateTaxCalculator>();

            builder.Services.AddTransient<ITaxCalculatorFactory, TaxCalculatorFactory>();
            builder.Services.AddTransient<ITaxService, TaxService>();
            builder.Services.AddTransient<IRateService, RateService>();
            builder.Services.AddScoped<TaxService>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
        }
    }
}