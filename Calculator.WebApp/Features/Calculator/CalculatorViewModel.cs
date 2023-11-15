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
    
    public static class Keys
    {
        public const string Results = "divResults";
        public const string ActiveCalculation = "ActiveCalculation";
        public const string ResultValue = "ResultValue";
        public const string NumberDisplayPrefix = "NumberDisplay-";
        public const string ClearButton = "ClearButton";
        public const string PlusButton = "PlusButton";
        public const string MinusButton = "MinusButton";
        public const string EqualsButton = "EqualsButton";
    }
}