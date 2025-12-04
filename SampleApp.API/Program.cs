using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using SampleApp.API.Data;
using SampleApp.API.Extensions;
using SampleApp.API.Interfaces;
using SampleApp.API.Repositories;
using SampleApp.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddScoped<IUserRepository, UsersRepository>();
builder.Services.AddSingleton<IRoleRepository, RolesMemoryRepository>();
builder.Services.AddScoped<SampleAppContext, SampleAppContext>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddDbContext<SampleAppContext>(o => o.UseNpgsql(builder.Configuration["ConnectionStrings:PostgreSQL"]));
builder.Services.AddJwtServices(builder.Configuration);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();