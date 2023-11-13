using Calculator.Tests.Global;

namespace Calculator.Tests.CalculatorWebApp.Features.Calculator;

public class Calculator : LazyWebAppFixtureBaseTest
{
    private const string WebUri = "/Calculator";
    public Calculator(WebApplicationFactory<Program> webApp)
        : base(webApp, WebUri)
    { }
    
    [Fact]
    public async Task Should_Display_All_Digits()
    {
        var doc = await Response.LoadResponseAsHtmlDoc();
        foreach (var digit in App.Calculator.Digits)
            doc.GetElementbyId($"numberDisplay-{digit}")
                .Should().NotBeNull();
    }
}