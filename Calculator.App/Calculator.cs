namespace Calculator.App;

public class Calculator : ICalculator
{
    public static int[] Digits => new[] { 7, 8, 9, 4, 5, 6, 1, 2, 3, 0 };
    public decimal ActiveValue { get; set; }
    public decimal ResultValue { get; set; }

    public Calculator(decimal? sum = default)
        => (ResultValue, ActiveValue) = (sum ?? default, 0);
    
    public decimal InputNumber(int input)
    {
        if((decimal.MaxValue - input) / 10m < ActiveValue)
            return ActiveValue = decimal.MaxValue;
        return ActiveValue = input switch
        {
            < 10 => 10m * ActiveValue + input,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, "Only use numbers 0->9")
        };
    }

    public void Clear() 
        => (ActiveValue, ResultValue) = (0, 0);
    
    public decimal Equals() 
        => ResultValue;
    
    public decimal Plus(decimal? add = default)
        => ResultValue = (_sum: ResultValue, add) switch
        {
            (decimal.MaxValue, > 0) => 0, 
            (> 0, decimal.MaxValue) => 0, 
            _ when (decimal.MaxValue - add ?? 0) < ResultValue => 0,    
            _ => ResultValue + add ?? 0
        };

    public decimal Minus(decimal? subtract)
        => ResultValue = (_sum: ResultValue, subtract) switch
        {
            (decimal.MinValue, > 0) => 0,
            (> 0, decimal.MinValue) => 0,
            _ when (decimal.MinValue + subtract ?? 0) > ResultValue => 0,
            _ => ResultValue - subtract ?? 0
        };

    
}
