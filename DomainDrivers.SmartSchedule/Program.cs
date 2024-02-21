using System.Data;
using System.Text.Json;
using DomainDrivers.SmartSchedule;
using DomainDrivers.SmartSchedule.Allocation;
using DomainDrivers.SmartSchedule.Availability;
using DomainDrivers.SmartSchedule.Allocation.CapabilityScheduling;
using DomainDrivers.SmartSchedule.Allocation.Cashflow;
using DomainDrivers.SmartSchedule.Planning;
using DomainDrivers.SmartSchedule.Resource;
using DomainDrivers.SmartSchedule.Resource.Device;
using DomainDrivers.SmartSchedule.Resource.Employee;
using DomainDrivers.SmartSchedule.Shared;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Postgres");
var dataSource = new NpgsqlDataSourceBuilder(connectionString)
    .ConfigureJsonOptions(new JsonSerializerOptions() { IgnoreReadOnlyProperties = true, IgnoreReadOnlyFields = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase})
    .EnableDynamicJson()
    .Build();
builder.Services.AddDbContext<SmartScheduleDbContext>(options => { options.UseNpgsql(dataSource); });
builder.Services.AddScoped<IDbConnection>(sp => sp.GetRequiredService<SmartScheduleDbContext>().Database.GetDbConnection());
builder.Services.AddShared();
builder.Services.AddPlanning();
builder.Services.AddAvailability();
builder.Services.AddAllocation();
builder.Services.AddCashFlow();
builder.Services.AddEmployee();
builder.Services.AddDevice();
builder.Services.AddResource();
builder.Services.AddCapabilityPlanning();

var app = builder.Build();

app.Run();

public partial class Program;