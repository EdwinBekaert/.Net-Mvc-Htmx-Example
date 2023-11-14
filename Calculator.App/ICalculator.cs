namespace Calculator.App;

public interface ICalculator
{
    static int[] Digits { get; }
    decimal ActiveValue { get; }
    decimal ResultValue { get; }
    decimal InputNumber(int input);
    void Clear();
    decimal Equals();
    decimal Plus(decimal? add = default);
    decimal Minus(decimal? subtract);
}