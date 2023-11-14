// See https://aka.ms/new-console-template for more information


var calc = new Calculator.App.Calculator();

var result = calc.Plus(5);
Console.WriteLine(calc.Equals());
result = calc.Plus(3.125m);
Console.WriteLine(calc.Equals());
result = calc.Plus(7.30m);
Console.WriteLine(calc.Equals());
result = calc.Minus(2.35m);
Console.WriteLine(calc.Equals());