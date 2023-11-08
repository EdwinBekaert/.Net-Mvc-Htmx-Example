namespace Calculator.App;

public class Calculator
{
    private double _sum;

    public Calculator(double? sum = default)
    {
        _sum = sum ?? 0;
    }

    public double Equals()
    {
        return _sum;
    }

    public Calculator Plus(double? value = default)
    {
        _sum += value ?? 0;
        return this;
    }
}