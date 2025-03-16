using RealTimeBoard.Api;
using RealTimeBoard.Api.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddFeatures(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.MapHub<DrawingHub>("/drawingHub");

app.Run();

