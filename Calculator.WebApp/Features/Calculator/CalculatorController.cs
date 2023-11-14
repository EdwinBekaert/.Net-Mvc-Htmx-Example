using Calculator.App;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebApp.Features.Calculator;

[Route("Calculator")]
public class CalculatorController : Controller
{
    private readonly ICalculator _calc;

    public CalculatorController(IHttpContextAccessor httpContextAccessor) 
        => _calc = new SessionCalculator(httpContextAccessor);

    public IActionResult Index()
    {
        var viewModel = new CalculatorViewModel(_calc);
        return View(viewModel);
    }

    [Route("InputNumber/{inputValue:int}")]
    [HttpGet]
    public IActionResult InputNumber([FromRoute]int inputValue)
    {
        _calc.InputNumber(inputValue);
        return DisplayCalculatorResultPartial();
    }

    [Route("Clear")]
    [HttpGet]
    public IActionResult Clear()
    {
        _calc.Clear();
        return DisplayCalculatorResultPartial();
    }

    [Route("Plus")]
    [HttpGet]
    public IActionResult Plus()
    {
        _calc.PlusOperator();
        return DisplayCalculatorResultPartial();
    }

    [Route("Minus")]
    [HttpGet]
    public IActionResult Minus()
    {
        _calc.MinusOperator();
        return DisplayCalculatorResultPartial();
    }

    private IActionResult DisplayCalculatorResultPartial()
    {
        var viewModel = new CalculatorViewModel(_calc);
        return PartialView("_CalculatorResult", viewModel);
    }
    
    private IActionResult DisplayCalculatorResultPartial()
    {
        var viewModel = new CalculatorViewModel(_calc);
        return PartialView("_CalculatorResult", viewModel);
    }
}