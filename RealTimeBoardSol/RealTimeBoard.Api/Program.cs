using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReadTimeBoard.Application.interfaces;
using ReadTimeBoard.Application.services;
using RealTimeBoard.Api;
using RealTimeBoard.Api.Extension;
using RealTimeBoard.Api.Services;
using RealTimeBoard.Domain.EntitySQL;
using RealTimeBoard.Infrustructure;
using RealTimeBoard.Infrustructure.Processors;
using RealTimeBoard.Infrustructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("DbPostgres"));
});

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.JwtOptionsKey));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:5173");
    });
});

builder.Services.AddScoped<IAuthTokenProcessor, AuthTokenProcessor>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();



builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    
    
    .AddDefaultTokenProviders();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwagger();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.RegisterAddAuthLogin(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddFeatures(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
        options.RoutePrefix = "";
    });
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapHub<DrawingHub>("/drawing-hub");

app.RegisterAllEndpoints();


app.Run();