using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealTimeBoard.Api;
using RealTimeBoard.Api.Extension;
using RealTimeBoard.Api.Services;
using RealTimeBoard.Domain.EntitySQL;
using RealTimeBoard.Infrustructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("DbPostgres"));
});

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.JwtOptionsKey));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapHub<DrawingHub>("/drawing-hub");

app.RegisterAllEndpoints();


app.Run();