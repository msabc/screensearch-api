<h1 align="center">ScreenSearch</h1>

**ScreenSearch** is a **.NET 9 Web API** that enables searching for your favourite movies and TV shows.

The project was bootstrapped using a custom 'dotnet-new' template.

## About
The project uses:
- [TMDB API](https://developer.themoviedb.org/) for querying movie and TV show data.
- [Kinocheck API](https://api.kinocheck.com/) for querying videos about a movie or a TV show.

## Multilingual support
The API supports multiple languages with the following precedance (from most important to least):
1. Query string parameter ('language')
2. Route parameter ('language')
3. 'Accept-Language' HTTP request header

## Rate limiting
- ScreenSearch.Api does not use rate limiting, although TMDB API requires it: [TMDB API rate limiting](https://developer.themoviedb.org/docs/rate-limiting).
- Before deployment several resources need to be provisioned in order to enable rate limiting

## Background jobs
ScreenSearch.Api uses 2 background jobs:
<table style="font-family: Arial, sans-serif;">
  <thead>
    <tr style="background-color: #f2f2f2;">
      <th style="border: 1px solid #ddd; padding: 8px; text-align: left;"><a href="https://owasp.org/Top10" target="_blank">Name</a></th>
      <th style="border: 1px solid #ddd; padding: 8px; text-align: left;">Purpose</th>
      <th style="border: 1px solid #ddd; padding: 8px; text-align: left;">Runs</th>
    </tr>
  </thead>
  <tbody>
    <tr>
        <td>TrendingBackgroundJob</td>
        <td>Periodically calls TMDB API in order to cache data of <b>trending movies and TV shows</b>.</td>
        <td>Once every hour.</td>
    </tr>
    <tr>
        <td>SupportedLanguagesJob</td>
        <td>Periodically calls TMDB API in order to cache <b>supported language data</b>.</td>
        <td>Once every hour.</td>
    </tr>
    </tbody>
</table>

## Caching
Redis is used for caching so you'll need to provide a Redis connection string before running the project.

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
    "ConnectionStrings": {
        "RedisConnectionString": "[YOUR_REDIS_CONNECTION_STRING]",
        "ApplicationInsightsConnectionString": "[YOUR_APPLICATION_INSIGHTS_CONNECTION_STRING]"
    },
    "ScreenSearchSettings": {
        "TMDBAPISettings": {
            "AccessToken": "[YOUR_TMDB_ACCESS_TOKEN]"
        }
    }
}
```

**Note:**
> Replace the values in brackets with your own values. 

5. Run the project

## Deployment
In order to set up a fully scalable global Web API, I would provision the following resources: 

1. Several **Azure App Service** instances accross the globe
2. One Azure **API Management** with a global cache rate limiter
3. **Azure Front Door**
4. **Azure Cache for Redis**

## CI/CD
This repository uses Github Actions for CI/CD.

Currently there is only a single workflow defined in the  **.github/workflows** folder:

1. *build-and-test* 
    - builds and tests the application and is triggered **manually**

## Security
<table style="font-family: Arial, sans-serif;">
  <thead>
    <tr style="background-color: #f2f2f2;">
      <th style="border: 1px solid #ddd; padding: 8px; text-align: left;"><a href="https://owasp.org/Top10" target="_blank">OWASP Top 10 Threats 2021 </a></th>
      <th style="border: 1px solid #ddd; padding: 8px; text-align: left;">Mitigation Strategies</th>
      <th style="border: 1px solid #ddd; padding: 8px; text-align: left;">Status</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="border: 1px solid #ddd; padding: 8px;">Broken Access Control</td>
      <td style="border: 1px solid #ddd; padding: 8px;">
        <p>ScreenSearch.Api doesn't store any user data nor does it support any data modification via HTTP verbs like POST, PUT and DELETE, therefore no access control was needed.</p>
      </td>
      <td>✅</td>
    </tr>
    <tr>
      <td style="border: 1px solid #ddd; padding: 8px;">Cryptographic Failures</td>
      <td style="border: 1px solid #ddd; padding: 8px;">
        <p>HTTPS redirection is used.</p>
      </td>
      <td>✅</td>
    </tr>
    <tr>
      <td style="border: 1px solid #ddd; padding: 8px;">Injection</td>
      <td style="border: 1px solid #ddd; padding: 8px;">
        <p>No sensitive data is stored server side. The data that is cached is not open to user manipulation (no parameters are used).</p>
      </td>
      <td>✅</td>
    </tr>
    <tr>
      <td style="border: 1px solid #ddd; padding: 8px;">Insecure Design</td>
      <td style="border: 1px solid #ddd; padding: 8px;">
        <p>No threat modeling is currently implemented.</p>
        <p>The application would need to contain unit and integration tests as well as code coverage.</p>
        <p>The application currently depends heavily on the data sources of outside APIs (for now) which could easily be a threat regarding DoS.</p>
      </td>
      <td>❌</td>
    </tr>
    <tr>
      <td style="border: 1px solid #ddd; padding: 8px;">Security Misconfiguration</td>
      <td style="border: 1px solid #ddd; padding: 8px;">
        <p>The application shouldn't be susceptible to misconfiguration at the moment, but it would require a certain effort in order to fully comply, eg. implementing a reverse proxy, setting up CORS...</p>
      </td>
      <td>✅</td>
    </tr>
    <tr>
      <td style="border: 1px solid #ddd; padding: 8px;">Vulnerable and Outdated Components</td>
      <td style="border: 1px solid #ddd; padding: 8px;">
        <p>All packages are updated to their latest versions, latest version of .NET is used.</p>
        <p>A tool like [Snyk](https://github.com/snyk) could be used to automate dependency checks in order to fully comply.</p>
      </td>
      <td>✅</td>
    </tr>
    <tr>
      <td style="border: 1px solid #ddd; padding: 8px;">Identification and Authentication Failures</td>
      <td style="border: 1px solid #ddd; padding: 8px;">
        <p>ScreenSearch does not implement identification or authentication as it is just an API for public data querying, not modification.</p>
      </td>
      <td>✅</td>
    </tr>
    <tr>
      <td style="border: 1px solid #ddd; padding: 8px;">Software and Data Integrity Failures</td>
      <td style="border: 1px solid #ddd; padding: 8px;">
        <p>A Github Action is set up which builds and tests the project, although for deployment I would add another action which creates an artifact for production.</p>
      </td>
      <td>✅</td>
    </tr>
    <tr>
      <td style="border: 1px solid #ddd; padding: 8px;">Security Logging and Monitoring Failures</td>
      <td style="border: 1px solid #ddd; padding: 8px;">
        <p>Application Insights are used for logging.</p>
      <td>✅</td>
    </tr>
  </tbody>
</table>