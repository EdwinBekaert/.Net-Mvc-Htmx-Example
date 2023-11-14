namespace Calculator.Tests.Global;

public abstract class LazyWebAppFixtureBaseTest : WebAppFixtureBaseTest
{
    private readonly string _uri;
    private readonly Lazy<Task<HttpResponseMessage>> _lazyResponse; // one call for all tests...

    protected LazyWebAppFixtureBaseTest(WebApplicationFactory<Program> factory, string uri) : base(factory)
    {
        _uri = uri;
        _lazyResponse ??= new Lazy<Task<HttpResponseMessage>>(GetAndValidateResponse);
    }

    protected async Task<HttpResponseMessage> GetAndValidateResponse() 
        => await Client.GetAndValidateResponse(_uri);

    protected async Task<HtmlDocument> GetHtmlDocument()
    {
        var response = await _lazyResponse.Value;
        var doc = await response.LoadResponseAsHtmlDoc();
        return doc;
    }
}
