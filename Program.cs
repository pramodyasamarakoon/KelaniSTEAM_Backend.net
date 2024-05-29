using Microsoft.EntityFrameworkCore;
using KelaniSTEAM_Backend.Models;
using KelaniSTEAM_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services
void ConfigureService<TSettings, TService>(WebApplicationBuilder builder, string sectionName)
    where TSettings : class
    where TService : class
{
    builder.Services.Configure<TSettings>(builder.Configuration.GetSection(sectionName));
    builder.Services.AddSingleton<TService>();
}

// Add services to the container
ConfigureService<BookStoreDatabaseSettings, BooksService>(builder, "BookStoreDatabase");
ConfigureService<DatabaseSettings, AlbumsService>(builder, "AlbumsDatabase");
ConfigureService<DatabaseSettings, BookingService>(builder, "BookingsDatabase");
ConfigureService<DatabaseSettings, ProjectService>(builder, "ProjectsDatabase");
ConfigureService<DatabaseSettings, TshirtOrderService>(builder, "TshirtOrdersDatabase");
ConfigureService<DatabaseSettings, AuthService>(builder, "UserDatabase");

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:3000")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
        });
});

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
