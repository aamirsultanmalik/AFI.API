## Getting Started

Use these instructions to get the project up and running.

### Prerequisites

You will need the following tools:

- [Visual Studio Code or Visual Studio 2022 17.1.5 or later](https://visualstudio.microsoft.com/vs/preview/)
- [.NET SDK 6](https://dotnet.microsoft.com/download/dotnet-core/6.0)

### Setup

Follow these steps to get your development environment set up:

1. Clone the repository
2. At the root directory, restore required packages by running:
   ```
   dotnet restore
   ```
3. Next, build the solution by running:
   ```
   dotnet build
   ```
4. Next, apply migrations:
   ```
   dotnet ef database update -p AFI.Infrastructure -s AFI.API
   ```
5. Once the front end has started, within the `MapPortal.WebUI` directory, launch the back end by running:
   ```
   dotnet run
   ```

## Technologies

- .NET 6.0
- ASP.NET Core 6.0
- Entity Framework Core 6.0
- FluentValidations 10.4.0
- FluentAssertions 6.6.0
