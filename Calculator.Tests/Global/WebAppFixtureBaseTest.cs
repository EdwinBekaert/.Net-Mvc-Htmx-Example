namespace Calculator.Tests.Global;

public abstract class WebAppFixtureBaseTest : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly TestHttpClient Client;
    
    protected WebAppFixtureBaseTest(WebApplicationFactory<Program> webApp) 
        => Client = new TestHttpClient(webApp);
    
}
