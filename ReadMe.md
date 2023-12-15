![SSW Banner](https://raw.githubusercontent.com/SSWConsulting/SSW.Rules.Content/main/_docs/images/ssw-banner.png)

# Northwind 365

<div align="center">

[![SSW TV | YouTube](https://img.shields.io/youtube/channel/views/UCBFgwtV9lIIhvoNh0xoQ7Pg?label=SSW%20TV%20%7C%20Views&style=social)](https://youtube.com/@SSWTV)

[![.NET](https://github.com/SSWConsulting/Northwind365/actions/workflows/dotnet.yml/badge.svg)](https://github.com/SSWConsulting/Northwind365/actions/workflows/dotnet.yml)
[![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/SSWConsulting/Northwind365/issues)

</div>

Northwind 365 is a sample application built using .NET 8, ASP.NET Core, and Entity Framework Core.

The initial construction of this project is explained in the following blog posts:

* [Code: Northwind Traders with Entity Framework Core](https://jasontaylor.dev/northwind-traders-with-entity-framework-core/)
* [Create Northwind Traders Code First with Entity Framework Core – Part 1](https://jasontaylor.dev/create-northwind-traders-code-first-with-entity-framework-core-part-1/)
* [Create Northwind Traders Code First with Entity Framework Core – Part 2](https://jasontaylor.dev/create-northwind-traders-code-first-with-entity-framework-core-part-2/)

## Learning Resources

You're interested learning more about Clean Architecture, please see this excellent video by Matt Goldman:

* [Clean Architecture with ASP.NET Core and MAUI](https://www.youtube.com/live/K9ryHflmQJE?si=VC2FtSZiAA3CxSsK)

Alternatively, SSW has many great rules about Clean Architecture:

* [SSW Rules - Clean Architecture](https://www.ssw.com.au/rules/rules-to-better-clean-architecture/)

If you'd like to start a new project using Clean Architecture, you can use the SSW Clean Architecture template:

* [SSW Clean Architecture Template](https://github.com/SSWConsulting/SSW.CleanArchitecture/)

You can also find a collection of commumity projects built on Clean Architecture here:

* [Awesome Clean Architecture](https://github.com/SSWConsulting/awesome-clean-architecture)

## Getting Started

Use these instructions to get the project up and running.

### Prerequisites

You will need the following tools:

* [Visual Studio or VS Code](https://visualstudio.microsoft.com/downloads/), or [Rider](https://www.jetbrains.com/rider/download/)
* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
* [Node.js](https://nodejs.org/en/) version 20.10 LTS or later - Recommended to install using [nvm](https://github.com/nvm-sh/nvm) or [nvm for Windows](https://github.com/coreybutler/nvm-windows)
* Angular CLI (version 17 or later) - install by running `npm install -g @angular/cli`

### Setup

Follow these steps to get your development environment set up:

1. Clone the repository
2. At the root directory, restore required packages by running:

```bash
 dotnet restore
```

3. Next, build the solution by running:

```bash
dotnet build
```

5. Once the front end has started, within the `\Src\WebUI` directory, launch the back end by running:

```bash
dotnet run
```

6. Launch [https://localhost:44427/](https://localhost:44427/) in your browser to view the Web UI

7. Launch [https://localhost:44376/api](http://localhost:44376/api) in your browser to view the API

## Technologies

* .NET 8
* ASP.NET Core 8
* Entity Framework Core 8
* Angular 15

### Other Packages

* MediatR
* FluentValidation
* AutoMapper
* Ardalis.Specification
* Ardalis.GuardClauses

### Testing Packages

* xUnit
* NSubstitute
* TestContainers
* Fluent Assertions
* Respawn
* Bogus

## Marketing

Marketing site can be found at [northwind365.com](https://northwind365.com/) which is deployed
from [Northwind365.Website](https://github.com/SSWConsulting/Northwind365.Website).

## License

This project is licensed under the MIT License - see
the [LICENSE.md](https://github.com/SSWConsulting/Northwind365/blob/main/LICENSE) file for details.
