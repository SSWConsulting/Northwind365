# NorthwindTraders

Northwind Traders is a sample application built using ASP.NET Core and Entity Framework Core. The architecture and design of the project is explained in the video:

* [Clean Architecture with ASP.NET Core](https://youtu.be/_lwCVE_XgqI) ([slide deck](https://github.com/JasonGT/NorthwindTraders/raw/master/Slides.pdf))

The initial construction of this project is explained in the following blog posts:

* [Code: Northwind Traders with Entity Framework Core](http://www.codingflow.net/northwind-traders-with-entity-framework-core/)
* [Create Northwind Traders Code First with Entity Framework Core – Part 1](http://www.codingflow.net/create-northwind-traders-code-first-with-entity-framework-core-part-1/)
* [Create Northwind Traders Code First with Entity Framework Core – Part 2](http://www.codingflow.net/create-northwind-traders-code-first-with-entity-framework-core-part-2/)

For information on upcoming features and fixes, take a look at the [product roadmap](https://github.com/JasonGT/NorthwindTraders/wiki/Roadmap).

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.3 or later)
* [.NET Core SDK 3](https://dotnet.microsoft.com/download/dotnet-core/3.0)
 * [Node.js](https://nodejs.org/en/) (version 10 or later) with npm (version 6.11.3 or later)

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
  4. Next, within the `\Src\WebUI\ClientApp` directory, launch the front end by running:
      ```
     npm start
     ```
  5. Once the front end has started, within the `\Src\WebUI` directory, launch the back end by running:
     ```
	 dotnet run
	 ```
  5. Launch [https://localhost:44376/](http://localhost:44376/) in your browser to view the Web UI
  
  6. Launch [https://localhost:44376/api](http://localhost:44376/api) in your browser to view the API

## Technologies
* .NET Core 3
* ASP.NET Core 3
* Entity Framework Core 3
* Angular 8

## Marketing
Marketing site can be found at [northwind365.com](https://northwind365.com/) which is deployed from [Northwind365.Website](https://github.com/SSWConsulting/Northwind365.Website).

## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/JasonGT/NorthwindTraders/blob/master/LICENSE.md) file for details.
