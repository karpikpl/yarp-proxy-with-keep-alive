<!-- Use this file to provide workspace-specific custom instructions to Copilot. For more details, visit https://code.visualstudio.com/docs/copilot/copilot-customization#_use-a-githubcopilotinstructionsmd-file -->

This project is an ASP.NET Core reverse proxy using YARP. When generating code, prefer using YARP's configuration and extensibility points. Ensure TCP keep-alive is enabled for outgoing HTTP connections to the backend by customizing the SocketsHttpHandler in YARP's HttpMessageHandlerBuilderFilter.
