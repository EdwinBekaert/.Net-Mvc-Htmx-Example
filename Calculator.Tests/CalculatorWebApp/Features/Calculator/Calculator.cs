using Calculator.Tests.Global;

namespace Calculator.Tests.CalculatorWebApp.Features.Calculator;

public class Calculator : WebAppFixtureBaseTest
{
    public Calculator(WebApplicationFactory<Program> webApp)
        : base(webApp)
    { }
    
    [Fact]
    public async Task Should_Display_All_Digits()
    {
        var response = await Client.GetAndValidateResponse("/Calculator");
        var doc = await response.LoadResponseAsHtmlDoc();
        foreach (var digit in App.Calculator.Digits)
            doc.GetElementbyId($"numberDisplay-{digit}")
                .Should().NotBeNull();
    }
}