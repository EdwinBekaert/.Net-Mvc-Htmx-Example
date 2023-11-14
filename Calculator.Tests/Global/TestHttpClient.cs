using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Calculator.Tests.Global;

public class TestHttpClient
{
    private HttpClient Client { get; }

    internal TestHttpClient(WebApplicationFactory<Program> webApp)
    {
        var factory = webApp.WithWebHostBuilder(GetCustomBuilder());
        //var t = factory.Services.CreateScope().ServiceProvider;
        Client = factory.CreateClient();
        // add headers
        Client.DefaultRequestHeaders.Clear();
        //Get the used db
        // _context = _serviceProvider.GetService<ApplicationDbContext>() ?? throw new ArgumentNullException(nameof(_context));
        // Drivers.DbContextInitializer.InitTestDb(_context);
    }

    private static Action<IWebHostBuilder> GetCustomBuilder()
    {
        return builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                // replace the database context
            });
        };
    }
    
    protected internal async Task<HttpResponseMessage> GetAndValidateResponse(string? uri)
    {
        var response = await Client.GetAsync(uri);
        response.ValidateSuccessResponse();
        return response;
    }
}