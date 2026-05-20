using FluentAssertions;

namespace Stuff802.Tests.E2E;

[Collection("Browser")]
public class SearchTests(BrowserFixture fixture)
{
    [Fact]
    public async Task SearchPage_Loads_WithoutQuery()
    {
        var page = await fixture.Browser.NewPageAsync();
        var response = await page.GotoAsync($"{BrowserFixture.BaseUrl}/search/");

        response!.Status.Should().Be(200);
        await page.CloseAsync();
    }

    [Fact]
    public async Task SearchPage_ReturnsResults_ForKnownTerm()
    {
        var page = await fixture.Browser.NewPageAsync();
        await page.GotoAsync($"{BrowserFixture.BaseUrl}/search/?query=design");

        var body = await page.InnerTextAsync("body");
        body.Should().NotBeNullOrEmpty();
        // Page should not be a 500 error
        (await page.TitleAsync()).Should().NotContain("500");
        await page.CloseAsync();
    }

    [Fact]
    public async Task GlossaryApi_ReturnsEntries()
    {
        var page = await fixture.Browser.NewPageAsync();
        var response = await page.GotoAsync($"{BrowserFixture.BaseUrl}/umbraco/api/glossary/entries");

        response!.Status.Should().Be(200);
        var body = await response.TextAsync();
        body.Should().StartWith("[");
        await page.CloseAsync();
    }
}
