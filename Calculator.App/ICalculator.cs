namespace Calculator.App;

public interface ICalculator
{
    static int[] Digits { get; }
    decimal ActiveValue { get; }
    decimal ResultValue { get; }
    string ActiveCalculation { get; }
    decimal InputNumber(int input);
    void Clear();
    decimal Equals();
    decimal Plus(decimal? add = default);
    decimal Minus(decimal? subtract);
    void PlusOperator();
}