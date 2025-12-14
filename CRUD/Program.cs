var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<Blog> blogs = new List<Blog>
{
    new Blog { Title = "First blog", Body = "This is my First blog" },
    new Blog { Title = "Second blog", Body = "This is my Second blog" }
};

app.MapGet("/", () => {
    return "Hello World";
});

// Read
app.MapGet("/blogs", () => 
{
    return blogs;
});

// Read by id
app.MapGet("/blogs/{id}", (int id) =>
{
   if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound("Not Found");
    }
    return Results.Ok(blogs[id]);
});

// Create
app.MapPost("/blogs", (Blog blog) =>
{
    blogs.Add(blog);
    return Results.Created($"/blogs/{blog.Title}", blog);
});

// Delete by id
app.MapDelete("/blogs/{id}", (int id) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    blogs.RemoveAt(id);
    return Results.NoContent();
});

// Edit by id
app.MapPut("/blogs/{id}", (int id, Blog blog) =>
{
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    blogs[id] = blog;
    return Results.Ok(blog);
});

app.Run();

class Blog
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}