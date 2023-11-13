using Calculator.Tests.Global;

namespace Calculator.Tests.CalculatorWebApp.Features.Shared;

public class Layout : LazyWebAppFixtureBaseTest
{
    public Layout(WebApplicationFactory<Program> factory) : base(factory, "/") {}
    
    private async Task<HtmlDocument> GetHtmlDocument()
    {
        var response = Response;
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
    
    [Fact]
    public async Task Should_Have_Calculator_Link()
    {
        var doc = await GetHtmlDocument();
        doc.NodeContainsInnerText("a","Calculator");
    }
}