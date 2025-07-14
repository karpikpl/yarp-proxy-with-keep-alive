
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy;
using System.Net.Http;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Use YARP with configuration from appsettings.json
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


var app = builder.Build();


app.MapReverseProxy();

app.Run();
