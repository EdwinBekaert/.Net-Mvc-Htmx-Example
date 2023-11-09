using Calculator.Tests.Global;

namespace Calculator.Tests.CalculatorWebApp.Features.Shared;

public class Layout : WebAppFixtureBaseTest
{
    private static Lazy<HttpResponseMessage>? _response; // one call for all tests...
    private static HttpResponseMessage GetAndValidateResponse(TestHttpClient client)
    {
        return client.GetAndValidateResponse("/").Result;
    }
    public Layout(WebApplicationFactory<Program> factory) : base(factory)
    {
        _response ??= new Lazy<HttpResponseMessage>(() => GetAndValidateResponse(Client));
        
    }
    
    private static async Task<HtmlDocument> GetHtmlDocument()
    {
        var response = _response!.Value;
        var doc = await response.LoadResponseAsHtmlDoc();
        return doc;
    }

    [Fact]
    public async Task Should_Have_CSS()
    {
        var doc = await GetHtmlDocument();
        doc.NodeContainsAttributeWithValue("link","href","/css/site.plus.css");
    }
    
    [Fact]
    public async Task Should_Have_JS()
    {
        var doc = await GetHtmlDocument();
        doc.NodeContainsAttributeWithValue("script","src","/js/site.js");
    }

    [Fact]
    public async Task Should_Have_Home_Link()
    {
        var doc = await GetHtmlDocument();
        doc.NodeContainsInnerText("a","Home");
    }
    
    [Fact]
    public async Task Should_Have_Privacy_Link()
    {
        var doc = await GetHtmlDocument();
        doc.NodeContainsInnerText("a","Privacy");
    }
}