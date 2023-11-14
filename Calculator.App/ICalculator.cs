namespace Calculator.App;

public interface ICalculator
{
    static int[] Digits { get; } = Array.Empty<int>();
    decimal ActiveValue { get; }
    decimal ResultValue { get; }
    decimal Equals();
    decimal Plus(decimal? add = default);
    decimal Minus(decimal? subtract);
    decimal InputNumber(int input);
}