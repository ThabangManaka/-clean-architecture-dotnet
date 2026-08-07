
using Common.Logging;
using Discount.API.Services;
using Discount.Application.Handlers;
using Discount.Core.Repositories;
using Discount.Infrastructure.Repositories;
using Discount.Infrastructure.Settings;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Mediatr
var assemblies = new Assembly[]
    {
        Assembly.GetExecutingAssembly(), typeof(CreateDiscountHandler).Assembly
    };

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();

builder.Services.Configure<DatabaseSettings>(options =>
{
    options.ConnectionString =
        builder.Configuration.GetConnectionString("discountdb")!;
});

//Register Logging
builder.Host.UseSerilog(Logging.ConfigureLogger);

//builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
app.MigrateDatabase();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
});


app.Run();
