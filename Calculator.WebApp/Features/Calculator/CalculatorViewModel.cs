using Calculator.App;

namespace Calculator.WebApp.Features.Calculator;

public class CalculatorViewModel
{
    public CalculatorViewModel(ICalculator calc)
    {
        Digits = App.Calculator.Digits;
        ActiveValue = calc.ActiveValue;
        ResultValue = calc.ResultValue;
    }
    public int[] Digits { get; set; }
    public decimal ActiveValue { get; set; }
    public decimal ResultValue { get; set; }
}