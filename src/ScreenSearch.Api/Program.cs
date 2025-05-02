using System.Threading.RateLimiting;
using Microsoft.FeatureManagement;
using Scalar.AspNetCore;
using ScreenSearch.Api.Constants;
using ScreenSearch.Api.Filters;
using ScreenSearch.Api.Jobs;
using ScreenSearch.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddOpenApi();

var settings = builder.Services.RegisterApplicationDependencies(builder.Configuration);

builder.Services.AddHostedService<TrendingJob>();

builder.Services.AddFeatureManagement();

builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy(RateLimitPolicies.TMDBPolicy, context =>
    {
        context.Items["RateLimiterPolicyName"] = "RemoteIpPolicy";

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Theme = ScalarTheme.DeepSpace;
    });
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.MapControllers();

app.Run();