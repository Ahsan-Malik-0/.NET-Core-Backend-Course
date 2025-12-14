var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/person", (Person person) =>
{
    return TypedResults.Ok(person);
});

app.MapPost("/json", async (HttpContext context) => {
    var person = context.Request.ReadFromJsonAsync<Person>().Result;
    return TypedResults.Ok(person);
});

app.Run();

class Person
{
    public required string Name { get; set; }
    public int? Age { get; set; }
}