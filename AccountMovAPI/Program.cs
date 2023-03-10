using AccountMovAPI.Helpers;
using CORE.Account.Extensions;
using Infrastructure.Persistence.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddCors(opt =>{    opt.AddPolicy("CorsRule", rule =>    {rule.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();});});
builder.Services.AddLogging(loggingBuilder =>
{
    //loggingBuilder.AddFile("logs.txt");
    loggingBuilder.AddProvider(new FileLoggerProvider("logs.txt"));
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//injectando dependencias
builder.Services.UseInfrastructurePersistence(builder.Configuration);
builder.Services.UseCoreServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("CorsRule");

app.UseAuthorization();

app.MapControllers();

app.Run();
