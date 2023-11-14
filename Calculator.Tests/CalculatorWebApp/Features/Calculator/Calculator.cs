using Calculator.Tests.Global;

namespace Calculator.Tests.CalculatorWebApp.Features.Calculator;

public class Calculator : LazyWebAppFixtureBaseTest
{
    private const string Uri = "/Calculator";
    public Calculator(WebApplicationFactory<Program> webApp) : base(webApp, Uri) { }
    
    [Fact]
    public async Task Should_Display_All_Digits()
    {
        var doc = await GetHtmlDocument();
        foreach (var digit in App.Calculator.Digits)
            doc.GetElementbyId($"numberDisplay-{digit}")
                .Should().NotBeNull();
    }
    
    [Fact]
    public async Task InputNumber_Should_Return_ResultPartial_With_Results()
    {
        await GetAndValidateResponse(); // home calculator
        var result = await Client.GetAndValidateResponse($"{Uri}/InputNumber/5");
        var doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId("ActiveValue").InnerText.Should().Be("5,00");
        doc.GetElementbyId("ResultValue").InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse($"{Uri}/InputNumber/5");
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId("ActiveValue").InnerText.Should().Be("55,00");
        doc.GetElementbyId("ResultValue").InnerText.Should().Be("0,00");
    }
}