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
        calc.ResultValue.Should().Be(expected);
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
        var func = () => calc.Plus(1);
        func.Should().NotThrow();
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
        calc.ResultValue.Should().Be(expected);
    }
    
    [Fact]
    public void MinusUnderMinValueReturnsZero()
    {
        var calc = new App.Calculator(decimal.MinValue);
        calc.Equals().Should().Be(decimal.MinValue);
        // now add one
        var func = () => calc.Minus(1);
        func.Should().NotThrow();
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
    
    [Fact]
    public void InputNumberShouldConcatActiveNumber()
    {
        var calc = new App.Calculator();
        calc.ActiveValue.Should().Be(0);
        calc.InputNumber(5);
        calc.ActiveValue.Should().Be(5);
        calc.InputNumber(1);
        calc.ActiveValue.Should().Be(51);
        calc.InputNumber(1).Should().Be(511);
    }
    
    [Fact]
    public void InputNumberMayBeZero()
    {
        var calc = new App.Calculator();
        calc.ActiveValue.Should().Be(0);
        calc.InputNumber(0);
        calc.ActiveValue.Should().Be(0);
        calc.InputNumber(0);
        calc.ActiveValue.Should().Be(0);
    }
    
    [Fact]
    public void InputNumberHigherThen9ThrowsOutOfRangeException()
    {
        var calc = new App.Calculator();
        calc.ActiveValue.Should().Be(0);
        calc.InputNumber(1);
        var func = () => calc.InputNumber(int.MaxValue);
        func.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Only use numbers 0->9 (Parameter 'input')\nActual value was 2147483647.");
        calc.ActiveValue.Should().Be(1);
        
    }
    
    [Fact]
    public void InputNumberExceedsMaxValueShouldReturnLastValue()
    {
        var calc = new App.Calculator();
        calc.ActiveValue.Should().Be(0);
        for (var i = 0; i < 30; i++)
        {
            calc.InputNumber(9);
        }
        calc.ActiveValue.Should().Be(decimal.MaxValue);
        
    }
    
    [Fact]
    public void ClearShouldSetActiveAndResultToZero()
    {
        var calc = new App.Calculator(10);
        calc.ResultValue.Should().Be(10);
        calc.ActiveValue.Should().Be(0);
        calc.InputNumber(5);
        calc.ActiveValue.Should().Be(5);
        calc.ResultValue.Should().Be(10);
        calc.Clear();
        calc.ActiveValue.Should().Be(0);
        calc.ResultValue.Should().Be(0);
    }
}