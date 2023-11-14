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

    [HttpGet]
    [Route("InputNumber/{inputValue:int}")]
    public IActionResult InputNumber([FromRoute]int inputValue)
    {
        _calc.InputNumber(inputValue);
        var viewModel = new CalculatorViewModel(_calc);
        return PartialView("_CalculatorResult", viewModel);
    }

    [Route("Clear")]
    public IActionResult Clear()
    {
        _calc.Clear();
        var viewModel = new CalculatorViewModel(_calc);
        return PartialView("_CalculatorResult", viewModel);
    }

    [Route("Plus")]
    public IActionResult Plus()
    {
        _calc.PlusOperator();
        var viewModel = new CalculatorViewModel(_calc);
        return PartialView("_CalculatorResult", viewModel);
    }

    [Route("Minus")]
    public IActionResult Minus()
    {
        _calc.MinusOperator();
        var viewModel = new CalculatorViewModel(_calc);
        return PartialView("_CalculatorResult", viewModel);
    }
}