using Calculator.App;
using Calculator.WebApp.Features.Shared;

namespace Calculator.WebApp.Features.Calculator;

public class SessionCalculator : ICalculator
{
    private readonly IStateManager<App.Calculator> _stateManager;
    public const string SessionKey = "SessionCalculatorKey";
    private readonly App.Calculator _calculator;
    
    public SessionCalculator(IStateManager<App.Calculator> stateManager) // session decorator for the calculator
    {
        _stateManager = stateManager;
        _calculator = stateManager.GetState(SessionKey) ?? new App.Calculator();
    }

    private void SetSessionValue()
        => _stateManager.SetState(SessionKey, _calculator);
    
    // original object actions
    public decimal ActiveValue 
        => _calculator.ActiveValue;
    
    public decimal ResultValue 
        => _calculator.ResultValue;

    public string ActiveCalculation
        => _calculator.ActiveCalculation;

    public void Clear()
    {
        _calculator.Clear();
        SetSessionValue();
    }
    
    public decimal Equals()
        => _calculator.Equals()
            .Do(_ => SetSessionValue()); // use do() extension
    
    public decimal Plus(decimal? add = default)
        => _calculator.Plus(add)
            .Do(_ => SetSessionValue()); // use do() extension
    
    public decimal Minus(decimal? subtract)
        => _calculator.Minus(subtract)
            .Do(_ => SetSessionValue()); // use do() extension

    public void PlusOperator()
    {
        _calculator.PlusOperator();
        SetSessionValue();
    }

    public decimal InputNumber(int input) 
        => _calculator.InputNumber(input)
            .Do(_ => SetSessionValue()); // use do() extension
    
    public void MinusOperator()
    {
        _calculator.MinusOperator();
        SetSessionValue();
    }
}