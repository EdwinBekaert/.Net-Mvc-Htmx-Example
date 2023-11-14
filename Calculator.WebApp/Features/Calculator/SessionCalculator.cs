using System.Text;
using Calculator.App;
using Calculator.WebApp.Features.Shared;
using Newtonsoft.Json;

namespace Calculator.WebApp.Features.Calculator;

public class SessionCalculator : ICalculator
{
    public const string SessionKey = "SessionCalculatorKey";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICalculator _calculator;
    
    public SessionCalculator(IHttpContextAccessor httpContextAccessor) // session decorator for the calculator
    {
        _httpContextAccessor = httpContextAccessor;
        if(httpContextAccessor.HttpContext is null)
            throw new ArgumentNullException(nameof(httpContextAccessor));
        var sessionValue = GetSessionValue(httpContextAccessor);
        _calculator = sessionValue != null
            ? DeserializeCalculator(Encoding.UTF8.GetString(sessionValue))
            : CreateCalc();
    }

    private static byte[]? GetSessionValue(IHttpContextAccessor httpContextAccessor) 
        => httpContextAccessor.HttpContext?.Session.Get(SessionKey);
    private void SetSessionValue()
        => _httpContextAccessor.HttpContext?.Session.Set(SessionKey, Encoding.UTF8.GetBytes(SerializeCalculator()));
    private static App.Calculator CreateCalc() 
        => new();
    private static App.Calculator DeserializeCalculator(string sessionValue) 
        => JsonConvert.DeserializeObject<App.Calculator>(sessionValue) 
           ?? CreateCalc();
    private string SerializeCalculator() 
        => JsonConvert.SerializeObject(_calculator);

    public decimal ActiveValue 
        => _calculator.ActiveValue;
    public decimal ResultValue 
        => _calculator.ResultValue;

    public decimal Equals()
        => _calculator.Equals();
    public decimal Plus(decimal? add = default)
        => _calculator.Plus(add)
            .Do(_ => SetSessionValue()); // use do() extension
    public decimal Minus(decimal? subtract)
        => _calculator.Minus(subtract)
            .Do(_ => SetSessionValue()); // use do() extension
    public decimal InputNumber(int input) 
        => _calculator.InputNumber(input)
            .Do(_ => SetSessionValue()); // use do() extension 
}