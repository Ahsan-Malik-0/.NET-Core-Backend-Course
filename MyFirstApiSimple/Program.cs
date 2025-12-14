var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/users/{userId}/posts/{slug}", (int userId, string slug) =>
{
    return $"User Id: {userId}, Posts: {slug}";
});

app.MapGet("/reports/{year?}", (int? year) =>
{
    return year.HasValue ? $"Report for year: {year}" : "Report for all years";
});

app.MapGet("/search", (string? q, int page = 1) =>
{
    return $"Searching for {q} on page {page}";
});

app.MapGet("/store/{category}/{productId:int?}/{extraPath?}", ( string category, int? productId, string? extraPath ) => {});

app.Run();
