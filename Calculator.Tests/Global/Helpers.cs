using System.Diagnostics.CodeAnalysis;

namespace Calculator.Tests.Global;

public static class Helpers
{

    internal static void ShouldNotBeNull<T>([NotNull] this T? value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
    }

    internal static void ValidateSuccessResponse(this HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType?.ToString().Should().Be("text/html; charset=utf-8");
    }
    
    internal static void NodeContainsInnerText(this HtmlDocument doc, string nodeFilter, string expected)
    {
        var node = doc.GetNode(nodeFilter);
        node.ShouldNotBeNull();
        node.InnerText.Should().Be(expected);
    }

    internal static void NodeContainsHtmlClass(this HtmlDocument doc, string nodeFilter, string htmlClass)
    {
        doc
            .GetNode($"//{nodeFilter}[@class='{htmlClass}']")
            .ShouldNotBeNull();
    }
    
    internal static void NodeContainsHtmlClassAndInnerText(this HtmlDocument doc, string nodeFilter, string htmlClass, string expected)
    {
        var node = doc.GetNode($"//{nodeFilter}[@class='{htmlClass}']");
        node.ShouldNotBeNull();
        node.InnerText.Should().Be(expected).ShouldNotBeNull();
    }

    internal static HtmlNode? GetNode(this HtmlDocument doc, string nodeFilter)
    {
        if (!nodeFilter.StartsWith("//"))
        {
            nodeFilter = "//" + nodeFilter;
        }
        return doc.DocumentNode.SelectSingleNode(nodeFilter);
    }
    
    internal static string GetNodeInnerText(this HtmlDocument doc, string nodeFilter) 
        => doc.GetNode(nodeFilter)?.InnerText 
           ?? string.Empty;

    internal static HtmlNodeCollection GetNodes(this HtmlDocument doc, string nodeFilter)
    {
        if (!nodeFilter.StartsWith("//"))
        {
            nodeFilter = "//" + nodeFilter;
        }
        return doc.DocumentNode.SelectNodes(nodeFilter);
    }
    
    internal static HtmlNodeCollection GetNodesHavingHtmlClass(this HtmlDocument doc, string nodeFilter, string htmlClass)
    {
        if (!nodeFilter.StartsWith("//"))
        {
            nodeFilter = "//" + nodeFilter;
        }
        nodeFilter += $"[@class='{htmlClass}']";
        return doc.DocumentNode.SelectNodes(nodeFilter);
    }
}