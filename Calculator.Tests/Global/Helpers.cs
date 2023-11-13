namespace Calculator.Tests.Global;

public static class Helpers
{
    internal static void ValidateSuccessResponse(this HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType?.ToString().Should().Be("text/html; charset=utf-8");
    }
    
    internal static async Task<HtmlDocument> LoadResponseAsHtmlDoc(this HttpResponseMessage response)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(await response.Content.ReadAsStringAsync());
        return htmlDoc;
    }

    private static string FixXpathFilter(this string nodeFilter)
        => nodeFilter.StartsWith("//") switch
        {
            false => "//" + nodeFilter,
            _ => nodeFilter
        };
    
    internal static HtmlNode? GetNode(this HtmlDocument doc, string nodeFilter)
        => doc.DocumentNode.SelectSingleNode(nodeFilter.FixXpathFilter());
    
    internal static string GetNodeInnerText(this HtmlDocument doc, string nodeFilter) 
        => doc.GetNode(nodeFilter)?.InnerText 
           ?? string.Empty;

    internal static HtmlNodeCollection GetNodes(this HtmlDocument doc, string nodeFilter)
        => doc.DocumentNode.SelectNodes(nodeFilter.FixXpathFilter());
    
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
    
}