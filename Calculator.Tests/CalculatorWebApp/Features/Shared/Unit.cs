using Calculator.WebApp.Features.Shared;

namespace Calculator.Tests.CalculatorWebApp.Features.Shared;

public class Unit
{
    [Theory]
    [InlineData("0e867fca-5ba7-4baa-ac48-e67558d0b010", true)]
    [InlineData("", false)]
    [InlineData("  ", false)]
    [InlineData(null, false)]
    public void ShowRequestIdIsSetBasedOnRequestId(string requestId, bool shouldShowRequestId)
    {
        var vm = new ErrorViewModel
        {
            RequestId = requestId
        };
        vm.ShowRequestId.Should().Be(shouldShowRequestId);
    }
}