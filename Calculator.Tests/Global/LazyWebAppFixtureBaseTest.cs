namespace Calculator.Tests.Global;

public abstract class LazyWebAppFixtureBaseTest : WebAppFixtureBaseTest
{
    private static string? _uri;
    private readonly Lazy<HttpResponseMessage> _lazyResponse; // one call for all tests in the same fixture ... 

    protected HttpResponseMessage Response 
        => _lazyResponse.Value; 
    private static HttpResponseMessage GetAndValidateResponse(TestHttpClient client) 
        => client.GetAndValidateResponse(_uri).Result;
    protected async Task<HtmlDocument> GetHtmlDocument()
        => await Response.LoadResponseAsHtmlDoc();

    protected LazyWebAppFixtureBaseTest(WebApplicationFactory<Program> webApp, string uri) : base(webApp)
    {
        _uri = uri;
        _lazyResponse ??= new Lazy<HttpResponseMessage>(() => GetAndValidateResponse(Client));
    }
}
