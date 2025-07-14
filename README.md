# YARP Reverse Proxy

This project is an ASP.NET Core reverse proxy using [YARP (Yet Another Reverse Proxy)](https://microsoft.github.io/reverse-proxy/).

## What does this proxy do?
- **Proxies all HTTP traffic**: Forwards all incoming HTTP requests to a backend service.
- **Backend target**: The backend is currently set to `https://slow-api--p833t0c.lemonmeadow-e3f71a42.eastus2.azurecontainerapps.io`.
- **TCP keep-alive**: Outbound connections to the backend use TCP keep-alive for reliability.
- **Long timeouts**: The proxy is configured to allow requests to run for up to 2 hours, so it can handle very slow or long-running backend operations without timing out.
- **Configuration**: All routes and clusters are configured in `appsettings.json` using the latest YARP format (routes as objects, not arrays).

## How it works
- The proxy listens for all incoming requests (`{**catch-all}`) and forwards them to the configured backend.
- The backend and timeout settings can be changed in `appsettings.json` under the `ReverseProxy` section.
- The proxy uses YARP's built-in configuration provider to load its settings at startup.

## Running the proxy
1. Restore dependencies:
   ```sh
   dotnet restore
   ```
2. Build and run the project:
   ```sh
   dotnet run --project yarp-proxy/yarp-proxy.csproj
   ```
3. The proxy will listen on the default Kestrel port (e.g., http://localhost:5276).

## Customization
- **Change the backend**: Edit the `Address` field in `appsettings.json`.
- **Change the timeout**: Edit the `Timeout` field in the cluster's `HttpClient` config in `appsettings.json`.
- **Add more routes or clusters**: Add new entries to the `Routes` or `Clusters` objects in `appsettings.json`.

## References
- [YARP Documentation](https://microsoft.github.io/reverse-proxy/)
- [YARP Timeouts](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/yarp/timeouts?view=aspnetcore-9.0)
