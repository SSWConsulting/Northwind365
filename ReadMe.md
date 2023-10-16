![SSW Banner](https://raw.githubusercontent.com/SSWConsulting/SSW.Rules.Content/main/_docs/images/ssw-banner.png)

# Northwind 365

<div align="center">

[![SSW TV | YouTube](https://img.shields.io/youtube/channel/views/UCBFgwtV9lIIhvoNh0xoQ7Pg?label=SSW%20TV%20%7C%20Views&style=social)](https://youtube.com/@SSWTV)

[![.NET](https://github.com/SSWConsulting/Northwind365/actions/workflows/dotnet.yml/badge.svg)](https://github.com/SSWConsulting/Northwind365/actions/workflows/dotnet.yml)
[![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/dwyl/esta/issues)

</div>

Northwind 365 is a sample application built using ASP.NET Core and Entity Framework Core. The architecture and
design of the project is explained in the video:

* [Clean Architecture with ASP.NET Core](https://youtu.be/_lwCVE_XgqI) ([slide deck](https://github.com/SSWConsulting/Northwind365/raw/main/Slides.pdf))

The initial construction of this project is explained in the following blog posts:

* [Code: Northwind Traders with Entity Framework Core](http://www.codingflow.net/northwind-traders-with-entity-framework-core/)
* [Create Northwind Traders Code First with Entity Framework Core – Part 1](http://www.codingflow.net/create-northwind-traders-code-first-with-entity-framework-core-part-1/)
* [Create Northwind Traders Code First with Entity Framework Core – Part 2](http://www.codingflow.net/create-northwind-traders-code-first-with-entity-framework-core-part-2/)

## Getting Started

Use these instructions to get the project up and running.

### Prerequisites

You will need the following tools:

* [Visual Studio or VS Code](https://visualstudio.microsoft.com/downloads/), or [Rider](https://www.jetbrains.com/rider/download/)
* [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
* [Node.js](https://nodejs.org/en/) (version 10 or later) with npm (version 6.11.3 or later)
* Angular CLI (version 8.3.23 or later) - install by running `npm install -g @angular/cli`

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

4. Next, within the `\Src\WebUI\ClientApp` directory, launch the front end by running:

```bash
npm start
```

5. Once the front end has started, within the `\Src\WebUI` directory, launch the back end by running:

```bash
dotnet run
```

5. Launch [https://localhost:44376/](http://localhost:44376/) in your browser to view the Web UI

6. Launch [https://localhost:44376/api](http://localhost:44376/api) in your browser to view the API

## Technologies

* .NET 7
* ASP.NET Core 7
* Entity Framework Core 7
* Angular 8

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
