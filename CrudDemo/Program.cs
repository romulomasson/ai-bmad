using CrudDemo.Models;
using CrudDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositório em memória (bom para demo em talk)
builder.Services.AddSingleton<ItemsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/items", (ItemCreateRequest req, ItemsRepository repo) =>
{
    if (req is null || string.IsNullOrWhiteSpace(req.Name))
        return Results.BadRequest(new { error = "`name` is required" });

    var created = repo.Create(req.Name.Trim(), req.Description ?? string.Empty);
    return Results.Created($"/items/{created.Id}", created);
})
.WithName("CreateItem")
.WithOpenApi();

app.MapGet("/items", (ItemsRepository repo) =>
{
    return Results.Ok(repo.GetAll());
})
.WithName("GetItems")
.WithOpenApi();

app.MapGet("/items/{id:int}", (int id, ItemsRepository repo) =>
{
    var item = repo.GetById(id);
    if (item is null) return Results.NotFound(new { error = "Not found" });
    return Results.Ok(item);
})
.WithName("GetItemById")
.WithOpenApi();

app.MapPut("/items/{id:int}", (int id, ItemUpdateRequest req, ItemsRepository repo) =>
{
    if (req is null || string.IsNullOrWhiteSpace(req.Name))
        return Results.BadRequest(new { error = "`name` is required" });

    var updated = repo.Update(id, req.Name.Trim(), req.Description ?? string.Empty);
    if (updated is null) return Results.NotFound(new { error = "Not found" });
    return Results.Ok(updated);
})
.WithName("UpdateItem")
.WithOpenApi();

app.MapDelete("/items/{id:int}", (int id, ItemsRepository repo) =>
{
    var deleted = repo.Delete(id);
    return deleted ? Results.NoContent() : Results.NotFound(new { error = "Not found" });
})
.WithName("DeleteItem")
.WithOpenApi();

app.Run();
