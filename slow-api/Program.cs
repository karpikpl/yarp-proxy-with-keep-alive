var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Minimal API to simulate a timeout (waits for the specified number of minutes)
app.MapGet("/simulate-timeout/{minutes:int}", async (int minutes, CancellationToken cancellationToken) =>
{
    await Task.Delay(TimeSpan.FromMinutes(minutes), cancellationToken);
    return Results.Ok($"Waited {minutes} minute(s)");
});

app.MapGet("/", () => "Hello World!");

app.Run();
