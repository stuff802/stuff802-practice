using FluentAssertions;
using Stuff802.Core.Helpers;

namespace Stuff802.Tests.Unit.Helpers;

public class AnchorHelperTests
{
    [Fact]
    public void ToAnchorId_ReturnsEmpty_WhenInputIsEmpty()
        => AnchorHelper.ToAnchorId("").Should().BeEmpty();

    [Fact]
    public void ToAnchorId_ReturnsEmpty_WhenInputIsWhitespace()
        => AnchorHelper.ToAnchorId("   ").Should().BeEmpty();

    [Fact]
    public void ToAnchorId_PreservesSimpleWord()
        => AnchorHelper.ToAnchorId("Accessibility").Should().Be("Accessibility");

    [Fact]
    public void ToAnchorId_ReplacesSpacesWithHyphens()
        => AnchorHelper.ToAnchorId("Active frontage").Should().Be("Active-frontage");

    [Fact]
    public void ToAnchorId_CollapseMultipleSpaces()
        => AnchorHelper.ToAnchorId("Active  frontage").Should().Be("Active-frontage");

    [Fact]
    public void ToAnchorId_RemovesSpecialCharacters()
        => AnchorHelper.ToAnchorId("Design & Access").Should().Be("Design-Access");

    [Fact]
    public void ToAnchorId_TrimsTrailingWhitespace()
        => AnchorHelper.ToAnchorId("Active frontage   ").Should().Be("Active-frontage");

    [Fact]
    public void ToAnchorId_PreservesHyphens()
        => AnchorHelper.ToAnchorId("Pre-application").Should().Be("Pre-application");

    [Fact]
    public void ToAnchorId_PreservesMixedCase()
        => AnchorHelper.ToAnchorId("Active Travel").Should().Be("Active-Travel");

    [Fact]
    public void ToAnchorId_RemovesPunctuation()
        => AnchorHelper.ToAnchorId("Design (guidance)").Should().Be("Design-guidance");
}
