var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/Division", (int val1, int val2) =>
{
    try
    {
        var result = val1 / val2;
        return Results.Ok($"Result: {result}");
    }
    catch (DivideByZeroException)
    {
        return Results.BadRequest("Division by zero is not allowed.");
    }
});

app.Run();