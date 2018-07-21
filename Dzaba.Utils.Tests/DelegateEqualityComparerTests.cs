using FluentAssertions;
using NUnit.Framework;

namespace Dzaba.Utils.Tests
{
    [TestFixture]
    public class DelegateEqualityComparerTests
    {
        [Test]
        public void Equals_WhenADelegateProvided_ThenItsInvoked()
        {
            var invoked = false;
            var sut = new DelegateEqualityComparer<int>((x, y) =>
            {
                invoked = true;
                return true;
            }, x => 1);

            var result = sut.Equals(1, 1);
            result.Should().BeTrue();
            invoked.Should().BeTrue();
        }

        [Test]
        public void GetHashCode_WhenADelegateProvided_ThenItsInvoked()
        {
            var invoked = false;
            var sut = new DelegateEqualityComparer<int>((x, y) => true, x =>
            {
                invoked = true;
                return x;
            });

            var result = sut.GetHashCode(1);
            result.Should().Be(1);
            invoked.Should().BeTrue();
        }
    }
}
