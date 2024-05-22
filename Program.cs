using Microsoft.EntityFrameworkCore;
using KelaniSTEAM_Backend.Models;
using KelaniSTEAM_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection("BookStoreDatabase"));
builder.Services.AddSingleton<BooksService>();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("AlbumsDatabase"));
builder.Services.AddSingleton<AlbumsService>();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("BookingsDatabase"));
builder.Services.AddSingleton<BookingService>();
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("ProjectsDatabase"));
builder.Services.AddSingleton<ProjectService>();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

// Enable CORS
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
