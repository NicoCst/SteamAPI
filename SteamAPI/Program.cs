using System.Text;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>(x => new UserRepository(builder.Configuration.GetConnectionString("Main")));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IGameRepository, GameRepository>(x => new GameRepository(builder.Configuration.GetConnectionString("Main")));
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IJwtService, JwtService>(x => new JwtService(builder.Configuration["JWT:SecretKey"], builder.Configuration["JWT:ExpirationDays"]));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policyOptions =>
    {
        policyOptions.AllowAnyOrigin();
        policyOptions.AllowAnyHeader();
        policyOptions.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("default");

app.MapControllers();

app.Run();
