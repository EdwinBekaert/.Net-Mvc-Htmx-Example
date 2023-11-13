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
}