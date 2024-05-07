using OrderApi.Data;
using OrderApi.Entities;
using OrderApi.Services;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseInMemoryDatabase("OrderDb");
});

builder.Services.AddScoped<IClientService, ClientService>();
var app = builder.Build();

// configure exception middleware
app.UseStatusCodePages(async statusCodeContext =>
    await Results
        .Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
        .ExecuteAsync(statusCodeContext.HttpContext)
);

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet(
        "/Clientele",
        async (IClientService clientservice) =>
            await clientservice.Client() is List<Client> clients
                ? Results.Ok(clients)
                : Results.NotFound()
    )
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Get all client",
        Description = "Returns information about all the Clients",
        Tags = new List<OpenApiTag> { new() { Name = "Client service" } }
    });

app.MapGet(
        "/GetClientbyId/{id}",
        async (IClientService clientService, int id) =>
            await clientService.GetClientByIdAsync(id) is Client client
                ? Results.Ok(client)
                : Results.NotFound()
    )
    .WithName("GetClientByIdAsync")
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Get Client By Id",
        Description = "Returns information about selected Client.",
        Tags = new List<OpenApiTag> { new() { Name = "Clioent Service" } }
    });

app.MapPost(
        "/AddClient",
        async (Client client, IClientService clientService) =>
        {
            await clientService.CreateClientAsync(client);
            return Results.Created($"/AddClient/{client.ClientId}", client);
        }
    )
    .WithOpenApi(x => new OpenApiOperation(x)
    {
        Summary = "Add Client",
        Description = "Add a new Client",
        Tags = new List<OpenApiTag> { new() { Name = "Client Service" } }
    });

app.Run();
