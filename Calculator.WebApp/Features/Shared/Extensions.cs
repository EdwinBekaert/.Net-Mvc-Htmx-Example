using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Calculator.WebApp.Features.Shared;

public static class Extensions
{
    public static T Do<T>(this T value, Action<T> action)
    {
        action(value);
        return value;
    }

    public static IServiceCollection AddSessionServices(this IServiceCollection services) // What we need to store the calculator object in the session
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
        services.AddDistributedMemoryCache(); // Add distributed memory cache for session
        services.AddSession(options => // Configure session options
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        services.AddHttpContextAccessor();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        return services;
    }
}