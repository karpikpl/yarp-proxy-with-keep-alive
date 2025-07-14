
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy;
using System.Net.Http;
using System.Net;


var builder = WebApplication.CreateBuilder(args);


// Configure TCP keep-alive for all outbound YARP connections
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .ConfigureHttpClient((context, handler) =>
    {
        if (handler is SocketsHttpHandler socketsHandler)
        {
            Console.WriteLine("Configuring SocketsHttpHandler for TCP keep-alive settings.");
            socketsHandler.PooledConnectionIdleTimeout = TimeSpan.FromHours(2);
            socketsHandler.ConnectTimeout = TimeSpan.FromHours(2);
            socketsHandler.KeepAlivePingDelay = TimeSpan.FromSeconds(30); // TCP keep-alive
            socketsHandler.KeepAlivePingTimeout = TimeSpan.FromSeconds(10);
            socketsHandler.KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always;
        }
    });

// Add a 2-hour timeout policy for proxied requests
builder.Services.AddRequestTimeouts(options =>
{
    options.AddPolicy("2h", TimeSpan.FromHours(2));
});



var app = builder.Build();

app.UseRequestTimeouts();


app.MapReverseProxy()
.WithRequestTimeout(TimeSpan.FromHours(2));

app.Run();
