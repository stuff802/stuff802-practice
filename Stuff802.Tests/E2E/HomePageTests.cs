using FluentAssertions;
using Microsoft.Playwright;

namespace Stuff802.Tests.E2E;

[Collection("Browser")]
public class HomePageTests(BrowserFixture fixture)
{
    [Fact]
    public async Task HomePage_Loads_Successfully()
    {
        var page = await fixture.Browser.NewPageAsync();
        var response = await page.GotoAsync(BrowserFixture.BaseUrl);

        response!.Status.Should().Be(200);
        await page.CloseAsync();
    }

    [Fact]
    public async Task HomePage_HasExpectedTitle()
    {
        var page = await fixture.Browser.NewPageAsync();
        await page.GotoAsync(BrowserFixture.BaseUrl);

        var title = await page.TitleAsync();
        title.Should().NotBeNullOrEmpty();
        await page.CloseAsync();
    }

    [Fact]
    public async Task HomePage_ContainsNavigation()
    {
        var page = await fixture.Browser.NewPageAsync();
        await page.GotoAsync(BrowserFixture.BaseUrl);

        var nav = page.Locator("nav");
        await nav.First.WaitForAsync();
        (await nav.CountAsync()).Should().BeGreaterThan(0);
        await page.CloseAsync();
    }
}
