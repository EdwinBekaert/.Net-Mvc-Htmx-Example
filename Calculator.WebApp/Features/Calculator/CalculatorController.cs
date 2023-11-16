using Calculator.App;
using Calculator.WebApp.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebApp.Features.Calculator;

[Route("Calculator")]
public class CalculatorController : Controller
{
    private readonly ICalculator _calc;
    
    public CalculatorController(IHttpContextAccessor httpContextAccessor) 
        => _calc = new SessionCalculator(new SessionManager<App.Calculator>(httpContextAccessor));

    public IActionResult Index()
    {
        var viewModel = new CalculatorViewModel(_calc);
        return View(viewModel);
    }

    [Route(Routes.InputNumber + "/{inputValue:int}")]
    [HttpGet]
    public IActionResult InputNumber([FromRoute]int inputValue)
    {
        _calc.InputNumber(inputValue);
        return DisplayCalculatorResultPartial();
    }

    [Route(Routes.Clear)]
    [HttpGet]
    public IActionResult Clear()
    {
        _calc.Clear();
        return DisplayCalculatorResultPartial();
    }

    [Route(Routes.Plus)]
    [HttpGet]
    public IActionResult Plus()
    {
        _calc.PlusOperator();
        return DisplayCalculatorResultPartial();
    }

    [Route(Routes.Minus)]
    [HttpGet]
    public IActionResult Minus()
    {
        _calc.MinusOperator();
        return DisplayCalculatorResultPartial();
    }

    [Route(Routes.Equal)]
    [HttpGet]
    public IActionResult Equals()
    {
        _calc.Equals();
        return DisplayCalculatorResultPartial();
    }
    
    private IActionResult DisplayCalculatorResultPartial()
    {
        var viewModel = new CalculatorViewModel(_calc);
        return PartialView("_CalculatorResult", viewModel);
    }
    
    public static class Routes
    {
        public const string InputNumber = "InputNumber";
        public const string Clear = "Clear";
        public const string Plus = "Plus";
        public const string Minus = "Minus";
        public const string Equal = "Equals"; // Equals is reserved...
    }
     
}