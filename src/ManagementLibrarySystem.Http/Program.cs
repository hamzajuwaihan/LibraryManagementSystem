using ManagementLibrarySystem.Infrastructure;
using ManagementLibrarySystem.Application;
using ManagementLibrarySystem.Presentation.Api.Routes;
using ManagementLibrarySystem.Presentation.Api;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructure()
.AddApplication()
.AddPresentation();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapBooksEndpoint();
app.MapLibraryEndpoint();
app.MapMemberEndpoint();

app.Run();
/// <summary>
/// for creating a client for unit testing for API routes
/// </summary>
public partial class Program { }