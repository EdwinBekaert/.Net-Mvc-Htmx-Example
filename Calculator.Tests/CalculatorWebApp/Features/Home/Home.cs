using Calculator.Tests.Global;

namespace Calculator.Tests.CalculatorWebApp.Features.Home;

public class Home : WebAppFixtureBaseTest
{
    public Home(WebApplicationFactory<Program> webApp) : base(webApp)
    {
        
    }
    
    [Theory]
    [InlineData("/", "Home Page - app")]
    [InlineData("/Home", "Home Page - app")]
    [InlineData("/Home/Privacy", "Privacy Policy - app")]
    [InlineData("/Home/Error", "Error - app")]
    public async Task Feature_Endpoints(string url, string title)
    {
        var response = await Client.GetAndValidateResponse(url);
        var doc = await response.LoadResponseAsHtmlDoc();
        doc.NodeContainsInnerText("title", title);
    }
    [Fact]
    public async Task Should_Display_Centered_Welcome_Header()
    {
        var response = await Client.GetAndValidateResponse("/");
        var doc = await response.LoadResponseAsHtmlDoc();
        doc.NodeContainsInnerText("h1", "Welcome");
        doc.NodeContainsHtmlClass("div", "text-center");
    }
}