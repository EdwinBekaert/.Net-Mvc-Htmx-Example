using Calculator.App;

namespace Calculator.WebApp.Features.Calculator;

public class CalculatorViewModel
{
    public CalculatorViewModel(ICalculator calc)
    {
        Digits = App.Calculator.Digits;
        ResultValue = calc.ResultValue;
        ActiveCalculation = calc.ActiveCalculation;
    }

    public int[] Digits { get; set; }
    public decimal ResultValue { get; set; }
    public string ActiveCalculation { get; set; }
}