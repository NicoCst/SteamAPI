using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;

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
//
// builder.Services.AddScoped<IAuthService, AuthService>();
//
// builder.Services.AddScoped<IJwtService, JwtService>(x => new JwtService(builder.Configuration["JWT:SecretKey"], builder.Configuration["JWT:ExpirationDays"]));

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

app.Run();
