# .Net-Mvc-Htmx-Example
Example App for .Net MVC + HTMX + Bulma
***
### TDD: Fluent Validations
``` csharp
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
```
***
### TDD: HTML Agility Pack
``` csharp
    internal static HtmlNodeCollection GetNodesHavingHtmlClass(this HtmlDocument doc, string nodeFilter, string htmlClass) 
        => doc.DocumentNode
            .SelectNodes($"{nodeFilter}[@class='{htmlClass}']".FixXpathFilter());

    internal static void NodeContainsInnerText(this HtmlDocument doc, string nodeFilter, string expected) 
        => doc
            .GetNode($"{nodeFilter}[contains(text(), '{expected}')]".FixXpathFilter())
            .Should().NotBeNull();

    internal static void NodeContainsHtmlClass(this HtmlDocument doc, string nodeFilter, string htmlClass)
        => doc
            .GetNode($"{nodeFilter}[contains(@class,'{htmlClass}')]".FixXpathFilter())
            .Should().NotBeNull();
    
    internal static void NodeContainsAttributeWithValue(this HtmlDocument doc, string nodeFilter, string attribute,
        string value)
        => doc
            .GetNode($"{nodeFilter}[starts-with(@{attribute},'{value}')]".FixXpathFilter())
            .Should().NotBeNull();
```
***
### TDD: WebApplication Test Fixture
``` csharp
    public abstract class WebAppFixtureBaseTest : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly TestHttpClient Client;
        
        protected WebAppFixtureBaseTest(WebApplicationFactory<Program> webApp) 
            => Client = new TestHttpClient(webApp);
        
    }
```
***
### TDD: Integration Tests
``` csharp
    [Fact]
    public async Task Clear_should_clear_results()
    {
        await GetAndValidateResponse(); // home calculator
        var result = await Client.GetAndValidateResponse($"{Uri}/InputNumber/5");
        var doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId("ActiveCalculation").InnerText.Should().Be("5");
        doc.GetElementbyId("ResultValue").InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse($"{Uri}/InputNumber/9");
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId("ActiveCalculation").InnerText.Should().Be("59");
        doc.GetElementbyId("ResultValue").InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse($"{Uri}/Clear");
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId("ActiveCalculation").InnerText.Should().Be("");
        doc.GetElementbyId("ResultValue").InnerText.Should().Be("0,00");
    }
```
***
### feature slices
``` csharp
    builder.Services.Configure<RazorViewEngineOptions>(options =>
    {
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add("~/Features");
        options.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("~/Features/Shared/{0}.cshtml");
    });
```
***
### Htmx
No javascript framework for swapping div content, but rather rich hx-* attributes. 
``` html
    @foreach (var number in Model.Digits)
    {
        <div id="numberDisplay-@number" class="column is-one-third">
            <p class="button number-button"
               hx-get="@Url.Action("InputNumber", "Calculator")/@number"
               hx-trigger="click, keyup[key=='@number'] from:body"
               hx-swap="outerHTML"
               hx-target="#divResults">
                &nbsp;&nbsp;&nbsp;@number&nbsp;&nbsp;&nbsp;
            </p>
        </div>
    }
    <div id="clearButton" class="column is-one-third">
        <p class="button clear-button"
           hx-get="@Url.Action("Clear", "Calculator")"
           hx-trigger="click, keyup[key=='Backspace'||key=='Delete'] from:body"
           hx-swap="outerHTML"
           hx-target="#divResults">
            &nbsp;&nbsp;&nbsp;<i class="fa-solid fa-eraser"></i>&nbsp;&nbsp;&nbsp;
        </p>
    </div>
    <div id="plusButton" class="column is-one-third">
        <p class="button plus-button menu-button"
           hx-get="@Url.Action("Plus", "Calculator")"
           hx-trigger="click, keyup[key=='+'] from:body"
           hx-swap="outerHTML"
           hx-target="#divResults">
            &nbsp;&nbsp;&nbsp;<i class="fa-solid fa-plus"></i>&nbsp;&nbsp;&nbsp;
        </p>
    </div>
    <div id="minusButton" class="column is-one-third">
        <p class="button minus-button menu-button"
           hx-get="@Url.Action("Minus", "Calculator")"
           hx-trigger="click, keyup[key=='-'] from:body"
           hx-swap="outerHTML"
           hx-target="#divResults">
            &nbsp;&nbsp;&nbsp;<i class="fa-solid fa-minus"></i>&nbsp;&nbsp;&nbsp;
        </p>
    </div>
    <div id="equalsButton" class="column is-one-third">
        <p class="button equals-button menu-button"
           hx-get="@Url.Action("Equals", "Calculator")"
           hx-trigger="click, keyup[key=='='||key=='Enter'] from:body"
           hx-swap="outerHTML"
           hx-target="#divResults">
            &nbsp;&nbsp;&nbsp;<i class="fa-solid fa-equals"></i>&nbsp;&nbsp;&nbsp;
        </p>
    </div>
```
***
### Extensions / generics
``` csharp
    public static T Do<T>(this T value, Action<T> action)
    {
        action(value);
        return value;
    }
```
``` csharp
    public decimal Plus(decimal? add = default)
        => _calculator.Plus(add)
            .Do(_ => SetSessionValue()); // use do() extension
```
***
### Decorator Pattern
Calculator object was decorated to enable it to live in the session 
``` csharp
    public class SessionCalculator : ICalculator
    {
        public const string SessionKey = "SessionCalculatorKey";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICalculator _calculator;
        
        public SessionCalculator(IHttpContextAccessor httpContextAccessor) // session decorator for the calculator
        {
            _httpContextAccessor = httpContextAccessor;
            if(httpContextAccessor.HttpContext is null)
                throw new ArgumentNullException(nameof(httpContextAccessor));
            var sessionValue = GetSessionValue(httpContextAccessor);
            _calculator = sessionValue != null
                ? DeserializeCalculator(Encoding.UTF8.GetString(sessionValue))
                : CreateCalc();
        }
    
        private static byte[]? GetSessionValue(IHttpContextAccessor httpContextAccessor) 
            => httpContextAccessor.HttpContext?.Session.Get(SessionKey);
        
        private void SetSessionValue()
            => _httpContextAccessor.HttpContext?.Session.Set(SessionKey, Encoding.UTF8.GetBytes(SerializeCalculator()));
        
        private static App.Calculator CreateCalc() 
            => new();
        
        private static App.Calculator DeserializeCalculator(string sessionValue) 
            => JsonConvert.DeserializeObject<App.Calculator>(sessionValue) 
               ?? CreateCalc();
        
        private string SerializeCalculator() 
            => JsonConvert.SerializeObject(_calculator);
    
        // original object actions
        public decimal ActiveValue 
            => _calculator.ActiveValue;
        
        public decimal ResultValue 
            => _calculator.ResultValue;
    
        public string ActiveCalculation
            => _calculator.ActiveCalculation;
    
        public void Clear()
        {
            _calculator.Clear();
            SetSessionValue();
        }
        
        public decimal Equals()
            => _calculator.Equals()
                .Do(_ => SetSessionValue()); // use do() extension
        
        public decimal Plus(decimal? add = default)
            => _calculator.Plus(add)
                .Do(_ => SetSessionValue()); // use do() extension
        
        public decimal Minus(decimal? subtract)
            => _calculator.Minus(subtract)
                .Do(_ => SetSessionValue()); // use do() extension
    
        public void PlusOperator()
        {
            _calculator.PlusOperator();
            SetSessionValue();
        }
    
        public decimal InputNumber(int input) 
            => _calculator.InputNumber(input)
                .Do(_ => SetSessionValue()); // use do() extension
        
        public void MinusOperator()
        {
            _calculator.MinusOperator();
            SetSessionValue();
        }
    }
```
