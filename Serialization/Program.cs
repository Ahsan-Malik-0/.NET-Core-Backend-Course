using System.Text.Json;
using System.Xml.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var person = new Person
{
    FirstName = "Jason",
    PersonAge = "30"
};

app.MapGet("/", () => "Hello World!");

app.MapGet("/manual-json", () =>
{
    var jasonSting = JsonSerializer.Serialize(person);
    return TypedResults.Text(jasonSting, "application/json");
});

app.MapGet("/custom-json", () => { 
    var option = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };

    var jasonSting = JsonSerializer.Serialize(person, option);
    return TypedResults.Text(jasonSting, "application/json");
});

app.MapGet("/json", () =>
{
    return TypedResults.Json<Person>(person);
});

app.MapGet("/auto", () =>
{
    return person;
});

app.MapGet("/XML", () =>
{
    var xmlSerializer = new XmlSerializer(typeof(Person));
    var stringWriter = new StringWriter();
    xmlSerializer.Serialize(stringWriter, person);
    var result = stringWriter.ToString();

    return TypedResults.Text(result, "application/xml");
});

app.Run();


public class Person
{
    public required string FirstName { get; set; }
    public required string PersonAge { get; set; }
}