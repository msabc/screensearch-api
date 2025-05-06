using System.Globalization;
using Asp.Versioning;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Scalar.AspNetCore;
using ScreenSearch.Api.Constants;
using ScreenSearch.Api.Conventions;
using ScreenSearch.Api.Filters;
using ScreenSearch.Api.Jobs;
using ScreenSearch.Application.Services.SupportedLanguage;
using ScreenSearch.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
    options.Conventions.Insert(0, new GlobalRoutePrefixConvention(new RouteAttribute("api")));
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddRouting(options =>
{
    options.LowercaseQueryStrings = true;
    options.LowercaseUrls = true;
});

builder.Services.AddOpenApi();

(var settings, var connectiongStrings) = builder.Services.RegisterApplicationDependencies(builder.Configuration);

builder.Services.AddHostedService<TrendingJob>();
builder.Services.AddHostedService<SupportedLanguagesJob>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddFeatureManagement();

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenTelemetry().UseAzureMonitor(options =>
    {
        options.ConnectionString = connectiongStrings.ApplicationInsightsConnectionString;
    });
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var supportedLanguageService = scope.ServiceProvider.GetRequiredService<ISupportedLanguageService>();
    var response = await supportedLanguageService.GetSupportedLanguagesAsync();

    var supportedCultures = response.Languages.Select(code => new CultureInfo(code)).ToList();

    var defaultCulture = new CultureInfo(settings.LanguageSettings.DefaultCulture);

    var localizationOptions = new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(defaultCulture),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures,
        RequestCultureProviders =
        [
            new QueryStringRequestCultureProvider { QueryStringKey = "language" },
            new AcceptLanguageHeaderRequestCultureProvider(),
            new CookieRequestCultureProvider() { CookieName = CookieNames.Culture  }
        ]
    };

    app.UseRequestLocalization(localizationOptions);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Theme = ScalarTheme.Kepler;
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();