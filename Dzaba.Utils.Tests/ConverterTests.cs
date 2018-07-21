using FluentAssertions;
using NUnit.Framework;
using System;

namespace Dzaba.Utils.Tests
{
    [TestFixture]
    public class ConverterTests
    {
        private struct InvalidStruct
        {

        }

        [Test]
        public void FromString_WhenAStringProvided_ThenItCanBeConvertedToInt()
        {
            var result = Converter.FromString<int>("1");
            result.Should().Be(1);
        }

        [Test]
        public void FromString_WhenAStructIsInvalid_ThenThereIsAnException()
        {
            this.Invoking(s => Converter.FromString<InvalidStruct>("test"))
                .Should().Throw<InvalidOperationException>();
        }
    }
}
