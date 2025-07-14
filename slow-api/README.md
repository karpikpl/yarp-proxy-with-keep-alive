# slow-api

A minimal ASP.NET Core API that simulates a timeout by waiting for a specified number of minutes before responding.

## Usage

- **Endpoint:** `/simulate-timeout/{minutes}`
- **Method:** GET
- **Description:** Waits for the specified number of minutes, then returns a message.

### Example

```
GET http://localhost:5138/simulate-timeout/10
```

Returns after 10 minutes:
```
{"result": "Waited 10 minute(s)"}
```

## Development

- Run locally:
  ```sh
  dotnet run --project slow-api/slow-api.csproj
  ```
- Test with the included `slow-api.http` file using the REST Client extension in VS Code.

## Docker

Build and run with Docker:
```sh
docker build -t slow-api ./slow-api
# Run on port 8080
docker run -p 8080:8080 slow-api
```

## License
MIT
