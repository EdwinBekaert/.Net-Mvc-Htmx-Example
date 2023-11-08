

using FluentAssertions;

namespace Calculator.Tests;

public class UnitTests
{
    [Theory]
    [InlineData(5D,5D)]
    [InlineData(5.12,5.12)]
    [InlineData(null,0)]
    public void CanCreateCalculatorWithStartingNumber(double? number, double expected)
    {
        var calc = new App.Calculator(number);
        calc.Equals().Should().Be(expected);
    }

    [Theory]
    [InlineData(5D,5D, 10D)]
    [InlineData(5.12,2.12, 7.24)]
    [InlineData(null,null, 0D)]
    [InlineData(1D,null, 1D)]
    [InlineData(null,5D, 5D)]
    public void PlusShouldAddAmount(double? start, double? add, double expected)
    {
        var calc = new App.Calculator(start);
        calc.Plus(add);
        var result = calc.Equals();
        result.Should().Be(expected);
    }
    
    
}