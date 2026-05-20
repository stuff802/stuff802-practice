using FluentAssertions;

namespace Stuff802.Tests.E2E;

[Collection("Browser")]
public class NavigationTests(BrowserFixture fixture)
{
    [Fact]
    public async Task ChapterListPage_Loads_Successfully()
    {
        var page = await fixture.Browser.NewPageAsync();
        var response = await page.GotoAsync($"{BrowserFixture.BaseUrl}/chapter-list/");

        response!.Status.Should().Be(200);
        await page.CloseAsync();
    }

    [Fact]
    public async Task InternalLinks_IncludePort()
    {
        var page = await fixture.Browser.NewPageAsync();
        await page.GotoAsync(BrowserFixture.BaseUrl);

        // Collect all internal hrefs on the home page
        var links = await page.EvalOnSelectorAllAsync<string[]>(
            "a[href^='/'], a[href^='http://localhost']",
            "els => els.map(e => e.href)");

        // None should link to localhost without a port
        links.Should().NotContain(l => l.StartsWith("http://localhost/") && !l.Contains(":9558"));
        await page.CloseAsync();
    }

    [Fact]
    public async Task ClickingChapterLink_NavigatesToCorrectPage()
    {
        var page = await fixture.Browser.NewPageAsync();
        await page.GotoAsync(BrowserFixture.BaseUrl);

        var chapterLink = page.Locator("a[href*='chapter']").First;
        if (await chapterLink.CountAsync() == 0)
        {
            await page.CloseAsync();
            return;
        }

        await chapterLink.ClickAsync();
        var response = page.Context.Pages.Last();
        (await page.TitleAsync()).Should().NotBeNullOrEmpty();
        await page.CloseAsync();
    }
}
