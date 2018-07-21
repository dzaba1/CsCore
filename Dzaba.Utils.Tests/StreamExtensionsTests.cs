using FluentAssertions;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace Dzaba.Utils.Tests
{
    [TestFixture]
    public class StreamExtensionsTests
    {
        private TextReader CreateTextReader()
        {
            return new StringReader(@"Test1
Test2");
        }

        [Test]
        public void ToEnemurable_WhenCalled_ThenItReadsAllLinesAsEnumerable()
        {
            using (var reader = CreateTextReader())
            {
                var col = reader.ToEnumerable().ToArray();
                col.Length.Should().Be(2);
            }
        }
    }
}
