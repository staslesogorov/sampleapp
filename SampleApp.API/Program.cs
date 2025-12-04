using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using SampleApp.API.Data;
using SampleApp.API.Interfaces;
using SampleApp.API.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddScoped<IUserRepository, UsersRepository>();
builder.Services.AddSingleton<IRoleRepository, RolesMemoryRepository>();
builder.Services.AddScoped<SampleAppContext, SampleAppContext>();
builder.Services.AddDbContext<SampleAppContext>(o => o.UseNpgsql(builder.Configuration["ConnectionStrings:PostgreSQL"]));
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "SampleApp",
            Version = "v1",
            Description = "API для пользователей",
            Contact = new OpenApiContact
            {
                Url = new Uri("https://github.com/staslesogorov/sampleapp"),
                Email = "14ib233@prep.scc",
            },
        }
    );
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader());
app.MapControllers();
app.Run();