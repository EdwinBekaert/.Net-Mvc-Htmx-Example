namespace Calculator.Tests.CalculatorApp;

public class CalculatorAppTests
{
    [Theory]
    [InlineData(5,5)]
    [InlineData(5.12,5.12)]
    [InlineData(null,0)]
    public void CanCreateCalculatorWithStartingNumber(decimal number, decimal expected)
    {
        var calc = new App.Calculator(number);
        calc.Equals().Should().Be(expected);
    }
    
    [Theory]
    [InlineData(5, 5, 10)]
    [InlineData(5.12, 2.12, 7.24)]
    [InlineData(null, null, 0)]
    [InlineData(1, null, 1)]
    [InlineData(null, 5, 5)]
    //[InlineData(decimal.MaxValue, 1, 0)]
    public void PlusShouldAddAmount(decimal start, decimal add, decimal expected)
    {
        var calc = new App.Calculator(start);
        calc.Plus(add);
        var result = calc.Equals();
        result.Should().Be(expected);
    }
    
    [Fact] // edge case
    public void AddAboveMaxValueReturnsZero()
    {
        var calc = new App.Calculator(decimal.MaxValue);
        calc.Equals().Should().Be(decimal.MaxValue);
        // now add one
        var exception = Record.Exception(() => calc.Plus(1));
        exception.Should().BeNull();
        calc.Equals().Should().Be(0);
    }
    
    [Theory]
    [InlineData(5,5, 0)]
    [InlineData(5,2.12, 2.88)]
    [InlineData(null,null, 0)]
    [InlineData(1,null, 1)]
    [InlineData(null,5, -5)]
    public void MinusShouldSubtractAmount(decimal start, decimal subtract, decimal expected) // do NOT make nullable decimals as Xunit still has issues with them !
    {
        var calc = new App.Calculator(start);
        calc.Minus(subtract);
        var result = calc.Equals();
        result.Should().Be(expected);
    }
    
    [Fact]
    public void MinusUnderMinValueReturnsZero()
    {
        var calc = new App.Calculator(decimal.MinValue);
        calc.Equals().Should().Be(decimal.MinValue);
        // now add one
        var exception = Record.Exception(() => calc.Minus(1));
        exception.Should().BeNull();
        calc.Equals().Should().Be(0);
    }
    
    [Fact]
    public void DigitsInOrderOfNumPad()
    {
        var digits = App.Calculator.Digits;
        digits.Should().NotBeNull();
        digits.Should().NotBeEmpty();
        digits.Should().BeEquivalentTo(new[] { 7, 8, 9, 4, 5, 6, 1, 2, 3, 0 });
    }
}