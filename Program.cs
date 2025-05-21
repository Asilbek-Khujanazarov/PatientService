using Microsoft.EntityFrameworkCore;
using PatientRecovery.PatientService.Data;
using PatientRecovery.PatientService.Repository;
using System.Reflection;
using PatientRecovery.PatientService.Mapping;
using MediatR;
using PatientRecovery.Shared.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<PatientDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Repositories
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

// RabbitMQ service registration
builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PatientDbContext>();
    db.Database.Migrate();
}

app.Run();