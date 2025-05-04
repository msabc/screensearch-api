using System.Globalization;
using System.Threading.RateLimiting;
using Asp.Versioning;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Scalar.AspNetCore;
using ScreenSearch.Api.Constants;
using ScreenSearch.Api.Conventions;
using ScreenSearch.Api.Filters;
using ScreenSearch.Api.Jobs;
using ScreenSearch.IoC;
using ScreenSearch.Application.Services.SupportedLanguage;

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
    options.LowercaseUrls = true;
});

builder.Services.AddOpenApi();

(var settings, var connectiongStrings) = builder.Services.RegisterApplicationDependencies(builder.Configuration);

builder.Services.AddHostedService<TrendingJob>();
builder.Services.AddHostedService<SupportedLanguagesJob>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddFeatureManagement();

builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy(RateLimitPolicies.TMDBPolicy, context =>
    {
        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: "tmdb",
            factory: key => new FixedWindowRateLimiterOptions
            {
                PermitLimit = settings.RateLimitSettings.TMDB.NumberOfRequestsPermitted,
                Window = TimeSpan.FromSeconds(settings.RateLimitSettings.TMDB.WindowSizeInSeconds),
                QueueLimit = 0,
                AutoReplenishment = true
            }
        );
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

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
            new RouteDataRequestCultureProvider { RouteDataStringKey = "language" },
            new AcceptLanguageHeaderRequestCultureProvider(),
            new CookieRequestCultureProvider()
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

app.UseRateLimiter();

app.MapControllers();

app.Run();