using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace Dzaba.Utils.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        private string CreateTestText()
        {
            return @"Test1

Test2

";
        }

        [Test]
        public void SplitToLines_WhenCalled_ItSplitsToLinesWithEmptyEntries()
        {
            var split = CreateTestText()
                .SplitToLines()
                .ToArray();

            split.Length.Should().Be(4);
            split[1].Should().BeEmpty();
            split[3].Should().BeEmpty();
        }

        [Test]
        public void SplitToLines_WhenCalled_ItSplitsWithoutEmptyEntries()
        {
            var split = CreateTestText()
                .SplitToLines(true)
                .ToArray();

            split.Length.Should().Be(2);
            split[0].Should().Be("Test1");
            split[1].Should().Be("Test2");
        }

        [Test]
        public void Swap_WhenCalled_ThenItSwapsElements()
        {
            var text = "AB";
            var result = text.Swap(0, 1);
            result.Should().Be("BA");
        }

        [Test]
        public void Swap_WhenProvidedTheSameIndex_ThenItDoesNothing()
        {
            var text = "AB";
            var result = text.Swap(0, 0);
            result.Should().BeSameAs(text);
        }
    }
}
