# .Net-Mvc-Htmx-Example
Example App for .Net MVC + HTMX + Bulma
***
### Testing

1. **Unit Testing Principles:**
   - **Description:** The tests follow the principles of unit testing, where individual components (methods or functions) are tested in isolation to ensure they behave as expected.
2. **Fluent Assertion Pattern:**
   - **Code Example:** The use of FluentAssertions methods like `Should().Be(...)`
   - **Description:** The test assertions are written in a fluent style, making them more readable and expressive.
3. **Edge Case Testing:**
   - **Code Example:** Tests for scenarios involving the minimum and maximum values of `decimal`.
   - **Description:** Edge case testing ensures that the application behaves correctly in extreme or boundary conditions, such as when values approach the limits of the data types.
4. **Test Fixture Pattern:**
   - **Description:** The test class contains multiple test methods sharing the same context (instance of the `WebApplicationFactory<Program>` class). This promotes code reuse and reduces redundancy in setup code.
   - **Code Example:** The test class inherits from `LazyWebAppFixtureBaseTest` and has common setup constants.
   - **Description:** The use of a test fixture class (`LazyWebAppFixtureBaseTest`) provides common setup logic and constants for the test class, promoting code reuse and maintainability.
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
``` csharp
    public abstract class WebAppFixtureBaseTest : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly TestHttpClient Client;
        
        protected WebAppFixtureBaseTest(WebApplicationFactory<Program> webApp) 
            => Client = new TestHttpClient(webApp);
        
    }
```
#### HTML Agility Pack
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
5. **Arrange-Act-Assert (AAA) Pattern:**
   - **Description:** The structure of each test method follows the AAA pattern. The Arrange section sets up the test scenario, the Act section performs the action being tested, and the Assert section verifies the expected outcome.
6. **Behavior-Driven Development (BDD) Style:**
   - **Description:** The test method names are written in a descriptive manner that reflects the behavior being tested. This aligns with the principles of Behavior-Driven Development.
7. **Page Object Pattern (sort of):**
   - **Code Example:** `CalculatorViewModel.Keys`, `doc.DocumentNode.SelectNodes()` as element retrieval using HTML Agility Pack
   - **Description:** The test methods use keys from `CalculatorViewModel.Keys` to interact with and assert against elements on the HTML page. This is similar to the Page Object pattern, where UI elements and their interactions are encapsulated in a separate class or structure.
``` csharp
    [Fact]
    public async Task Clear_should_clear_results()
    {
        await GetAndValidateResponse(); // home calculator
        var result = await Client.GetAndValidateResponse($"{InputNumberUri}/5");
        var doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("5");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse($"{InputNumberUri}/9");
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("59");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
        result = await Client.GetAndValidateResponse(ClearUri);
        doc = await result.LoadResponseAsHtmlDoc();
        doc.GetElementbyId(CalculatorViewModel.Keys.ActiveCalculation).InnerText.Should().Be("");
        doc.GetElementbyId(CalculatorViewModel.Keys.ResultValue).InnerText.Should().Be("0,00");
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
     <div id="@CalculatorViewModel.Keys.NumberDisplayPrefix@number" class="column is-one-third">
         <p class="button number-button"
            hx-get="@Url.Action(CalculatorController.Routes.InputNumber, "Calculator")/@number"
            hx-trigger="click, keyup[key=='@number'] from:body"
            hx-swap="outerHTML"
            hx-target="#@CalculatorViewModel.Keys.Results">
             &nbsp;&nbsp;&nbsp;@number&nbsp;&nbsp;&nbsp;
         </p>
     </div>
   }
   <div id="@CalculatorViewModel.Keys.ClearButton" class="column is-one-third">
     <p class="button clear-button"
        hx-get="@Url.Action(CalculatorController.Routes.Clear, "Calculator")"
        hx-trigger="click, keyup[key=='Backspace'||key=='Delete'] from:body"
        hx-swap="outerHTML"
        hx-target="#@CalculatorViewModel.Keys.Results">
         &nbsp;&nbsp;&nbsp;<i class="fa-solid fa-eraser"></i>&nbsp;&nbsp;&nbsp;
     </p>
   </div>
   <div id="@CalculatorViewModel.Keys.PlusButton" class="column is-one-third">
       <p class="button plus-button menu-button"
          hx-get="@Url.Action(CalculatorController.Routes.Plus, "Calculator")"
          hx-trigger="click, keyup[key=='+'] from:body"
          hx-swap="outerHTML"
          hx-target="#@CalculatorViewModel.Keys.Results">
           &nbsp;&nbsp;&nbsp;<i class="fa-solid fa-plus"></i>&nbsp;&nbsp;&nbsp;
       </p>
   </div>
   <div id="@CalculatorViewModel.Keys.MinusButton" class="column is-one-third">
       <p class="button minus-button menu-button"
          hx-get="@Url.Action(CalculatorController.Routes.Minus, "Calculator")"
          hx-trigger="click, keyup[key=='-'] from:body"
          hx-swap="outerHTML"
          hx-target="#@CalculatorViewModel.Keys.Results">
           &nbsp;&nbsp;&nbsp;<i class="fa-solid fa-minus"></i>&nbsp;&nbsp;&nbsp;
       </p>
   </div>
   <div id="@CalculatorViewModel.Keys.EqualsButton" class="column is-one-third">
       <p class="button equals-button menu-button"
          hx-get="@Url.Action(CalculatorController.Routes.Equal, "Calculator")"
          hx-trigger="click, keyup[key=='='||key=='Enter'] from:body"
          hx-swap="outerHTML"
          hx-target="#@CalculatorViewModel.Keys.Results">
           &nbsp;&nbsp;&nbsp;<i class="fa-solid fa-equals"></i>&nbsp;&nbsp;&nbsp;
       </p>
   </div>
```
***
### SOLID principles used
1. **Single Responsibility Principle (SRP):**
   - The `SessionManager` class has a single responsibility: managing the session for a generic type `T` and handling serialization/deserialization. It implements the `IStateManager<T>` and `ISerialization<T>` interfaces, each focused on a specific aspect of functionality.
      ````csharp
      public class SessionManager<T> : IStateManager<T>, ISerialization<T> where T : class {}
      ````
2. **Open/Closed Principle (OCP):**
   - The `Extensions` class demonstrates the Open/Closed Principle by providing extension methods (`Do` method) without modifying existing classes. This allows for adding new functionality without changing the existing code.
      ````csharp
        public static T Do<T>(this T value, Action<T> action)
        {
            action(value);
            return value;
        }
      ````
3. **Liskov Substitution Principle (LSP):**
   - The `SessionCalculator` class, implementing the `ICalculator` interface, can be used wherever an `ICalculator` is expected, following the Liskov Substitution Principle.
      ````csharp
      public class SessionCalculator : ICalculator {}
      ````

4. **Interface Segregation Principle (ISP):**
   - The `IStateManager<T>` and `ISerialization<T>` interfaces are narrowly focused on specific responsibilities related to state management and serialization. This adheres to the Interface Segregation Principle by avoiding the creation of overly broad interfaces.
      ````csharp
      public interface IStateManager<T> where T:class
      {
            T? GetState(string key);
            void SetState(string key, T state);
      }
      public interface ISerialization<T> where T:class
      {
            string Serialize(T value);
            T? Deserialize(string sessionValue);
      }
      ````
5. **Dependency Inversion Principle (DIP):**
   - The `SessionManager<T>` and `SessionCalculator` classes depend on abstractions (`IStateManager<T>` and `ISerialization<T>`), not on concrete implementations. 
      ````csharp
      public class SessionManager<T> : IStateManager<T>, ISerialization<T> where T : class {}
      ````

***
### Design Patterns and Practices
1. **Decorator Pattern:**
    - The `SessionCalculator` class acts as a decorator for the `ICalculator` interface. It enhances the behavior of the original calculator by adding session persistence to it.

2. **Dependency Injection:**
    - The `SessionCalculator` class takes an `IHttpContextAccessor` as a constructor parameter, following the Dependency Injection pattern.
    - The `CalculatorViewModel` takes an `ICalculator` instance as a constructor parameter, following the Dependency Injection pattern.
    - This allows the class to be more easily testable and promotes the use of interfaces
      Yes, in addition to the SOLID principles and design patterns mentioned earlier, the code adheres to several other good design practices:

3. **Composition over Inheritance:**
   - The `SessionCalculator` class does not rely on inheritance but uses composition to include the functionality of session management. This adheres to the "favor composition over inheritance" principle, making the code more modular and flexible.

4. **Guard Clauses:**
   - The constructor of the `SessionManager` class and the `AddSessionServices` method in the `Extensions` class include guard clauses to check for null arguments (`ArgumentNullException`). This helps prevent null reference exceptions and ensures that the code fails fast when invalid arguments are provided.

5. **Separation of Concerns:**
   - The code is organized into separate classes and interfaces, each responsible for a specific concern. For example, the `SessionManager` class handles session management and serialization, while the `SessionCalculator` class focuses on implementing the `ICalculator` interface with session-related behavior.

6. **Method Chaining / Fluent Interface:**
    - The `.Do(_ => SetSessionValue())` part in several methods can be seen as a form of method chaining, where the result of a method is immediately used to invoke another method. In this case, it's used to perform additional actions after the original calculator action is executed.
      The provided code exhibits the use of several design patterns:

7. **Strategy Pattern (sort of / indirectly):**
    - The use of the `ICalculator` interface allows for different calculator implementations. The session-based calculator can be swapped with another implementation that adheres to the `ICalculator` interface.
    - The `SessionCalculator` class indirectly employs a strategy pattern. The strategy pattern involves defining a family of algorithms, encapsulating each one, and making them interchangeable. In this case, the strategy for session management is encapsulated within the `SessionCalculator` class, and it can be easily replaced with another strategy (e.g., a different session management implementation) without modifying the client code.

      ```` csharp
      public class SessionCalculator : ICalculator
      {
         private readonly IStateManager<App.Calculator> _stateManager;
         public const string SessionKey = "SessionCalculatorKey";
         private readonly App.Calculator _calculator;
         
         public SessionCalculator(IStateManager<App.Calculator> stateManager) // session decorator for the calculator
         {
            _stateManager = stateManager;
            _calculator = stateManager.GetState(SessionKey) ?? new App.Calculator();
         }
         
          private void SetSessionValue()
             => _stateManager.SetState(SessionKey, _calculator);
          
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
      ````
8. **Constants Class Pattern:**
   - The `CalculatorViewModel.Keys` class follows the Constants Class pattern. It contains constant string fields that serve as keys or identifiers, providing a centralized location for managing constant values related to the view.

9. **Initialization through Constructor:**
   - The `CalculatorViewModel` initializes its properties in the constructor based on the provided `ICalculator` instance, a common practice for initializing view models.

10. **Data Transfer Object (DTO) Pattern:**
    - The `CalculatorViewModel` can be considered a form of a Data Transfer Object as it encapsulates data related to the view.

      ```` csharp
      public class CalculatorViewModel
      {
          public CalculatorViewModel(ICalculator calc)
          {
              Digits = App.Calculator.Digits;
              ResultValue = calc.ResultValue;
              ActiveCalculation = calc.ActiveCalculation;
          }
      
          public int[] Digits { get; set; }
          public decimal ResultValue { get; set; }
          public string ActiveCalculation { get; set; }
          
          public static class Keys
          {
              public const string Results = "divResults";
              public const string ActiveCalculation = "ActiveCalculation";
              public const string ResultValue = "ResultValue";
              public const string NumberDisplayPrefix = "NumberDisplay-";
              public const string ClearButton = "ClearButton";
              public const string PlusButton = "PlusButton";
              public const string MinusButton = "MinusButton";
              public const string EqualsButton = "EqualsButton";
          }
      }
      ````