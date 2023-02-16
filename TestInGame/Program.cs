using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestInGame.Data;
using TestInGame.Data.Entities;
using TestInGame.Data.Interfaces;
using TestInGame.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InGameDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/authors", async (IAuthorRepository repository) =>
    {
        return await repository.GetAuthorsAsync();
    })
    .Produces<List<Author>>(StatusCodes.Status200OK)
    .WithName("GetAllAuthors")
    .WithTags("Getters");
app.MapGet("/authors/{id}", async (int id, IAuthorRepository repository) =>
    {
        return await repository.GetAuthorAsync(id);
    })
    .Produces<Author>(StatusCodes.Status200OK)
    .WithName("GetAuthor")
    .WithTags("Getters");
app.MapPost("/authors", async ([FromBody] Author author, IAuthorRepository repository) =>
    {
        await repository.InsertAuthorAsync(author);
        await repository.SaveAsync();
    })
    .Accepts<Author>("application/json")
    .Produces<Author>(StatusCodes.Status201Created)
    .WithName("CreateAuthor")
    .WithTags("Creators");
app.MapPut("/authors", async([FromBody] Author author, IAuthorRepository repository) =>
    {
        await repository.UpdateAuthorAsync(author);
        await repository.SaveAsync();
    })
    .Accepts<Author>("application/json")
    .WithName("UpdateAuthor")
    .WithTags("Updaters");
app.MapDelete("/authors{id}", async (int id, IAuthorRepository repository) =>
    {
        await repository.DeleteAuthorAsync(id);
        await repository.SaveAsync();
    })
    .WithName("DeleteAuthor")
    .WithTags("Deleters");


app.MapGet("/books", async (IBookRepository repository) =>
    {
        return await repository.GetBooksAsync();
    })
    .Produces<List<Book>>(StatusCodes.Status200OK)
    .WithName("GetAllBooks")
    .WithTags("Getters");
app.MapGet("/books/{id}", async (int id, IBookRepository repository) =>
    {
        return await repository.GetBookAsync(id);
    })
    .Produces<List<Book>>(StatusCodes.Status200OK)
    .WithName("GetBook")
    .WithTags("Getters");
app.MapPost("/books", async ([FromBody] Book book, IBookRepository repository) =>
    {
        await repository.InsertBookAsync(book);
        await repository.SaveAsync();
    })
    .Accepts<Book>("application/json")
    .Produces<Book>(StatusCodes.Status201Created)
    .WithName("CreateBook")
    .WithTags("Creators");
app.MapPut("/books", async ([FromBody] Book book, IBookRepository repository) =>
    {
        await repository.UpdateBookAsync(book);
        await repository.SaveAsync();
    })
    .Accepts<Book>("application/json")
    .WithName("UpdateBook")
    .WithTags("Updaters");
app.MapDelete("/books{id}", async (int id, IBookRepository repository) =>
    {
        await repository.DeleteBookAsync(id);
        await repository.SaveAsync();
    })
    .WithName("DeleteBook")
    .WithTags("Deleters");


app.MapGet("/genres", (InGameDbContext db) => db.Genres.Include(b => b.Books).ToList())
    .Produces<List<Genre>>(StatusCodes.Status200OK)
    .WithName("GetAllGenre")
    .WithTags("Getters");

app.UseHttpsRedirection();

app.Run();