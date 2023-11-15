using Calculator.Tests.Global;
using Calculator.WebApp.Features.Calculator;

namespace Calculator.Tests.CalculatorWebApp.Features.Calculator;

public class Calculator : LazyWebAppFixtureBaseTest
{
    private const string Uri = "/Calculator";
    private const string InputNumberUri = $"{Uri}/{CalculatorController.Routes.InputNumber}";
    private const string EqualsUri = $"{Uri}/{CalculatorController.Routes.Equal}";
    private const string PlusUri = $"{Uri}/{CalculatorController.Routes.Plus}";
    private const string MinusUri = $"{Uri}/{CalculatorController.Routes.Minus}";
    private const string ClearUri = $"{Uri}/{CalculatorController.Routes.Clear}";
    public Calculator(WebApplicationFactory<Program> webApp) : base(webApp, Uri) { }
    
    [Fact]
    public async Task Should_Display_All_Digits_And_Buttons()
    {
        var doc = await GetHtmlDocument();
        foreach (var digit in App.Calculator.Digits)
            doc.GetElementbyId($"{CalculatorViewModel.Keys.NumberDisplayPrefix}{digit}")
                .Should().NotBeNull();
        doc.GetElementbyId(CalculatorViewModel.Keys.ClearButton)
            .Should().NotBeNull();
        doc.GetElementbyId(CalculatorViewModel.Keys.PlusButton)
            .Should().NotBeNull();
        doc.GetElementbyId(CalculatorViewModel.Keys.MinusButton)
            .Should().NotBeNull();
        doc.GetElementbyId(CalculatorViewModel.Keys.EqualsButton)
            .Should().NotBeNull();
    }
    
    [Fact]
    public async Task InputNumber_Should_Return_ResultPartial_With_Results()
    {
        await GetAndValidateResponse(); // home calculator
        var result = await Client.GetAndValidateResponse($"{InputNumberUri}/5");
        var doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation)
            .InnerText.Should().Be("5");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue)
            .InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse($"{InputNumberUri}/5");
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("55");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
    }
    
    [Fact]
    public async Task Clear_should_clear_results()
    {
        await GetAndValidateResponse(); // home calculator
        var result = await Client.GetAndValidateResponse($"{InputNumberUri}/5");
        var doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("5");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse($"{InputNumberUri}/9");
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("59");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse(ClearUri);
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
    }
    
    [Fact]
    public async Task PlusOperation_should_do_sum()
    {
        var result = await Client.GetAndValidateResponse($"{InputNumberUri}/5");
        var doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("5");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse($"{InputNumberUri}/9");
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("59");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse(PlusUri);
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("59&#x2B;"); // &#x2B; is plus symbol
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("59,00");
    }
    
    [Fact]
    public async Task MinusOperation_should_do_sum()
    {
        var result = await Client.GetAndValidateResponse($"{InputNumberUri}/5");
        var doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("5");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse($"{InputNumberUri}/9");
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("59");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse(MinusUri);
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("59-");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("59,00");
    }
    
    [Fact]
    public async Task Equals_should_do_sum()
    {
        var result = await Client.GetAndValidateResponse($"{InputNumberUri}/5"); // 5
        result.Should().NotBeNull();
        result = await Client.GetAndValidateResponse($"{InputNumberUri}/9"); // 59
        result.Should().NotBeNull();
        result = await Client.GetAndValidateResponse(PlusUri); // 59+
        result.Should().NotBeNull();
        result = await Client.GetAndValidateResponse($"{InputNumberUri}/9"); //59+9
        result.Should().NotBeNull();
        result = await Client.GetAndValidateResponse(EqualsUri); //59+9=68
        result.Should().NotBeNull();
        var doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be(""); // &#x2B; is plus symbol
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("68,00");
    }
}