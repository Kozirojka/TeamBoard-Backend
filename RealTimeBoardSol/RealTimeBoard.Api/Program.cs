using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealTimeBoard.Application.interfaces;
using RealTimeBoard.Application.Processors;
using RealTimeBoard.Application.Repository;
using RealTimeBoard.Application.services;
using RealTimeBoard.Api;
using RealTimeBoard.Api.Extension;
using RealTimeBoard.Domain;
using RealTimeBoard.Domain.EntitySQL;
using RealTimeBoard.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("DbPostgres"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.JwtOptionsKey));

// builder.Services.AddCors(opt =>
// {
//     opt.AddPolicy("CorsPolicy", options =>
//     {
//         options.AllowAnyHeader().AllowAnyMethod().AllowCredentials().AllowAnyOrigin();
//     });
// });

builder.Services.AddScoped<IAuthTokenProcessor, AuthTokenProcessor>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwagger();
builder.Services.AddOpenApi();

//builder.Services.RegisterAddAuthLogin(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddFeatures(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Readtime board API V1");
        options.RoutePrefix = "";
    });
}

//app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapHub<DrawingHub>("/drawing-hub");

app.RegisterAllEndpoints();


app.Run();