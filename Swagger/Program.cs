using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var blogs = new List<Blog>
{
    new Blog { Title = "First Post", Body = "This is the body of the first post." },
    new Blog { Title = "Second Post", Body = "This is the body of the second post." }
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/blogs", () =>
{
    return Results.Ok(blogs);
});

app.MapGet("/blogs/{id}", Results<Ok<Blog>, NotFound> (int id) => {
    if (id < 0 || id >= blogs.Count)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(blogs[id]);
});

app.MapPost("/blogs", (Blog blog) =>
{
    blogs.Add(blog);
    return Results.Created($"/blogs/{blogs.Count - 1}", blog);
});

app.MapPut("/blogs/{id}", (int id, Blog updatedBlog) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    blogs[id] = updatedBlog;
    return Results.Ok();
});

app.MapDelete("/blogs/{id}", (int id) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    blogs.RemoveAt(id);
    return Results.Ok();
});

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.Run();

class Blog
{
    required public string Title { get; set; }
    required public string Body { get; set; }
}
