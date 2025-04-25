<h1 align="center">ScreenSearch</h1>

**ScreenSearch** is a **.NET 9 Web API** that enables searching for your favourite movies and TV shows.

The project was bootstrapped using a custom 'dotnet-new' template.
This project uses [TMDB API](https://developer.themoviedb.org/) for relevant data.

## Architecture

The API implements [**Domain-driven design**](https://en.wikipedia.org/wiki/Domain-driven_design)
and is divided into several projects (layers):

- Configuration
    - holds configuration classes used to implement the [options pattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-9.0)
- IoC
    - central place for the entire DI ([dependency inversion](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles#dependency-inversion)) setup
- Api
    - contains the **API entry point**
- Application
    - contains the business logic
- Domain
    - contains the domain models
- Infrastructure
    - contains the database context
- Tests
    - contains tests

## Run the project locally

**Note:**
> Before running the project, you will need to create an account at [TMDB](https://developer.themoviedb.org). Read their [docs](https://developer.themoviedb.org/docs/getting-started) in order
to successfully set up your account.

1. Clone the project
2. Open the solution (**ScreenSearch.Api.sln**) in Visual Studio
3. Set **ScreenSearch.Api** as the Startup project
4. Configure **user secrets** necessary for local development: 
    - Right-click on the **ScreenSearch.Api** project and click 'Manage User Secrets'
    - a secrets.json file will be created
    - populate the file with the following values:

 ```javascript
{
    "ScreenSearchSettings": {
        "TMDBAPISettings": {
            "AccessToken": [YOUR_TMDB_ACCESS_TOKEN]
        }
    }
}
```

**Note:**
> Replace the values in brackets with your own values. 

5. Run the project

## CI/CD

This repository uses Github Actions for CI/CD.

Currently there are two workflows defined in the  **.github/workflows** folder:

1. *build-and-test* 
    - builds and tests the application and is triggered **manually**