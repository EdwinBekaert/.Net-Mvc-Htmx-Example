using Calculator.Tests.Global;

namespace Calculator.Tests.CalculatorWebApp.Features.Home;

public class Privacy : WebAppFixtureBaseTest
{
    public Privacy(WebApplicationFactory<Program> factory) 
        : base(factory) { }

    [Fact]
    public async Task Should_Display_Title_As_H1_And_One_Paragraph()
    {
        var response = await Client.GetAndValidateResponse("/Home/Privacy");
        var doc = await response.LoadResponseAsHtmlDoc();
        var title = doc.GetNodeInnerText("title");
        title = title.Replace(" - app", ""); // layout adds " - app"
        doc.NodeContainsInnerText("h1", title);
        doc.GetNodes("p").Count.Should().Be(3);
    }
}