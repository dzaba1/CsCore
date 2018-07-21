using FluentAssertions;
using NUnit.Framework;

namespace Dzaba.Utils.Tests
{
    [TestFixture]
    public class ExpressionExtensionsTests
    {
        private class TestClass
        {
            public int Value { get; set; }
        }

        [Test]
        public void GetMemberName_WhenAPropertyProvided_ThenItReturnsItsName()
        {
            var obj = new TestClass();
            var name = ExpressionExtensions.GetMemberName(() => obj.Value);
            name.Should().Be(nameof(TestClass.Value));
        }
    }
}
