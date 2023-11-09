using Calculator.WebApp.Features.Shared;

namespace Calculator.Tests.CalculatorWebApp.Features.Home;

public class Unit
{
    [Fact]
    public void ShowRequestIdIsSet ()
    {
        var errorVm = new ErrorViewModel
        {
            RequestId = "123"
        };
        errorVm.ShowRequestId.Should().BeTrue();
        errorVm.RequestId = string.Empty;
        errorVm.ShowRequestId.Should().BeFalse();
    }
}