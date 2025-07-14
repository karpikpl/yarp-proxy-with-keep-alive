
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy;
using System.Net.Http;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


// Configure the default SocketsHttpHandler for all YARP outbound requests
builder.Services.AddReverseProxy()
    .ConfigureHttpClient((context, handler) =>
    {
        if (handler is SocketsHttpHandler socketsHandler)
        {
            socketsHandler.PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2);
            socketsHandler.KeepAlivePingDelay = TimeSpan.FromSeconds(30); // TCP keep-alive
            socketsHandler.KeepAlivePingTimeout = TimeSpan.FromSeconds(10);
            socketsHandler.KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always;
        }
    })

// Add YARP and configure the proxy
    .LoadFromMemory(new[]
    {
        new Yarp.ReverseProxy.Configuration.RouteConfig
        {
            RouteId = "default",
            ClusterId = "cluster1",
            Match = new Yarp.ReverseProxy.Configuration.RouteMatch
            {
                Path = "{**catch-all}"
            }
        }
    },
    new[]
    {
        new Yarp.ReverseProxy.Configuration.ClusterConfig
        {
            ClusterId = "cluster1",
            Destinations = new Dictionary<string, Yarp.ReverseProxy.Configuration.DestinationConfig>
            {
                ["destination1"] = new Yarp.ReverseProxy.Configuration.DestinationConfig
                {
                    Address = "https://slow-api--p833t0c.lemonmeadow-e3f71a42.eastus2.azurecontainerapps.io"
                }
            }
        }
    });


var app = builder.Build();


app.MapReverseProxy();

app.Run();
