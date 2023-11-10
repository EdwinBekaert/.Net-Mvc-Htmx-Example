using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebApp.Features.Calculator;

public class CalculatorController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}