using Microsoft.EntityFrameworkCore;  
using HomeLibrary.Models;            
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HomeLibraryDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("HomeLibraryDb")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
      app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapGet("/library", async (HomeLibraryDb db) =>
await db.Books.ToListAsync());

app.MapGet("/library/{id}", async (int id, HomeLibraryDb db) => 
await db.Books.FindAsync(id));

app.MapGet("/library/wishlist", async (HomeLibraryDb db) =>
await db.Books.Where(b => b.wishlist == true).ToListAsync());


app.MapPost("/library", async ([FromBody] Book book, HomeLibraryDb db) =>
{
    db.Books.Add(book);
    await db.SaveChangesAsync();
    return Results.Created($"/library/{book.Id}", book);
});

app.MapPut("/library/{id}", async (int id, [FromBody] Book book, HomeLibraryDb db) =>
{
    book.Id = id;
    db.Update(book);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/library/{id}", async (int id, HomeLibraryDb db) => {
    var book = await db.Books.FindAsync(id);
    if (book is null) return Results.NotFound();
    db.Books.Remove(book);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
