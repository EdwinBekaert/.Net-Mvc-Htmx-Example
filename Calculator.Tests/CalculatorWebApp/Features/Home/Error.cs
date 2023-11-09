using Calculator.Tests.Global;

namespace Calculator.Tests.CalculatorWebApp.Features.Home;

public class Error : WebAppFixtureBaseTest
{
    public Error(WebApplicationFactory<Program> webApp) 
        : base(webApp) { }

    [Fact]
    public async Task Should_Display_Title_Headers_Paragraphs()
    {
        var response = await Client.GetAndValidateResponse("/Home/Error");
        var doc = await Client.LoadResponseAsHtmlDoc(response);
        var title = doc.GetNodeInnerText("title");
        title = title.Replace(" - app", ""); // layout adds " - app"
        doc.NodeContainsInnerText("h1", title);
        doc.GetNodes("h1").Count.Should().Be(1);
        doc.GetNodesHavingHtmlClass("h2", "text-danger").Count.Should().Be(1);
        doc.GetNodes("h3").Count.Should().Be(1);
        doc.GetNodes("p").Count.Should().Be(3);
    }
}