using AFI.Application.Services;
using AFI.Application.Services.CustomerReg;
using AFI.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<IAFIDbContext, AFIDbContext>(options => 
{
    options.UseSqlite(builder.Configuration.GetConnectionString("AFI"));
});
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddControllers().AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CustomerValidator>());
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

EnsureDatabaseCreated();

app.Run();

void EnsureDatabaseCreated()
{
    var optionsBuilder = new DbContextOptionsBuilder();
    if (app.Environment.IsDevelopment()) optionsBuilder.UseSqlite(app.Configuration.GetConnectionString("AFI"));

    var context = new AFIDbContext(optionsBuilder.Options);
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

