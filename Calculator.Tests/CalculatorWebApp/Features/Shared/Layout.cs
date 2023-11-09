using Calculator.Tests.Global;

namespace Calculator.Tests.CalculatorWebApp.Features.Shared;

public class Layout : WebAppFixtureBaseTest
{

    public Layout(WebApplicationFactory<Program> factory) 
        : base(factory) { }
    
    [Fact]
    public async Task Should_Have_CSS()
    {
        // var response = await GetAndValidateResponse("/");
        // var doc = await LoadResponseAsHtmlDoc(response);
        // doc.NodeContainsInnerText("h1", "Welcome");
        // doc.NodeContainsHtmlClass("div", "text-center");
    }
    
    [Fact]
    public async Task Should_Have_CSS2()
    {
        // var response = await GetAndValidateResponse("/");
        // var doc = await LoadResponseAsHtmlDoc(response);
        // doc.NodeContainsInnerText("h1", "Welcome");
        // doc.NodeContainsHtmlClass("div", "text-center");
    }
    
    // <link rel="stylesheet" href="~/css/site.css" asp-append-version="true"/>
    // <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
    // <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
    // <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
    // <script src="~/js/site.js" asp-append-version="true"></script>
}