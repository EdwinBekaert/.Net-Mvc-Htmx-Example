﻿namespace Calculator.App;

public class Calculator
{
    public static int[] Digits => new[] { 7, 8, 9, 4, 5, 6, 1, 2, 3, 0 };
    private decimal _sum;

    public Calculator(decimal? sum = default) 
        => _sum = sum ?? default;

    public decimal Equals() 
        => _sum;

    public decimal Plus(decimal? add = default)
        => _sum = (_sum, add) switch
        {
            (decimal.MaxValue, > 0) => 0, 
            (> 0, decimal.MaxValue) => 0, 
            _ when (decimal.MaxValue - add ?? 0) < _sum => 0,    
            _ => _sum + add ?? 0
        };

    public decimal Minus(decimal? subtract)
        => _sum = (_sum, subtract) switch
        {
            (decimal.MinValue, > 0) => 0,
            (> 0, decimal.MinValue) => 0,
            _ when (decimal.MinValue + subtract ?? 0) > _sum => 0,
            _ => _sum - subtract ?? 0
        };
    
}
