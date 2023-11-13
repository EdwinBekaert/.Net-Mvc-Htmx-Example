namespace Calculator.Tests.Global;

public abstract class LazyWebAppFixtureBaseTest : WebAppFixtureBaseTest
{
    private static string? _uri;
    private readonly Lazy<HttpResponseMessage> _lazyResponse 
        = new(); // one call for all tests...
    protected HttpResponseMessage Response 
        => _lazyResponse.Value; 
    private static HttpResponseMessage GetAndValidateResponse(TestHttpClient client) 
        => client.GetAndValidateResponse(_uri).Result;

    protected LazyWebAppFixtureBaseTest(WebApplicationFactory<Program> webApp, string uri) : base(webApp)
    {
        _uri = uri;
        _lazyResponse ??= new Lazy<HttpResponseMessage>(() => GetAndValidateResponse(Client));
    }
}
