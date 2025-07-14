# YARP Proxy with TCP Keep-Alive

This project is an ASP.NET Core reverse proxy using YARP (Yet Another Reverse Proxy) that forwards all traffic to https://dm-walmart.lucernex.com/rest/.

## Features
- Proxies all HTTP traffic to the specified backend.
- Ensures TCP keep-alive is enabled for outgoing connections to the backend by customizing the HTTP handler.

## How to Run

1. Ensure you have .NET 8 SDK installed.
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Build and run the project:
   ```sh
   dotnet run
   ```
4. The proxy will listen on the default Kestrel port (e.g., http://localhost:5000) and forward requests to the backend.

## Customization
- The TCP keep-alive settings are configured in code via a custom `HttpMessageHandlerBuilderFilter`.

## References
- [YARP Documentation](https://microsoft.github.io/reverse-proxy/)
- [SocketsHttpHandler.KeepAlivePingPolicy](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.socketshttphandler.keepalivepingpolicy)
