var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var blog = new List<Blog>
{
    new Blog { Title = "This is my first blog" , Body = "This is the content of my first blog post."},
    new Blog { Title = "This is my second blog" , Body = "This is the content of my second blog post."} 
};

app.Use(async (context, next) =>
{
    Console.WriteLine(context.Request.Path);
    await next();
    Console.WriteLine(context.Response.StatusCode);
});


// Middleware to check for Password header on non-GET requests
app.UseWhen(
    context => context.Request.Method != "GET",
    appBuilder => appBuilder.Use(async (context, next) =>
    {
        var extractedPasswprd = context.Request.Headers["Password"];
        if (extractedPasswprd == "secret")
        {
            await next.Invoke();
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
        }
    })
);

app.MapGet("/", () => "Hello World!");

app.MapGet("/blog", () =>
{
    return Results.Ok(blog);
});

app.MapPost("/blog", (Blog newBlog) =>
{
    blog.Add(newBlog);
    return Results.Created("/blog", blog);
});

app.Run();

class Blog
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}
