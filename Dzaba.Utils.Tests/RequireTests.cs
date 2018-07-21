using FluentAssertions;
using NUnit.Framework;
using System;

namespace Dzaba.Utils.Tests
{
    [TestFixture]
    public class RequireTests
    {
        [Test]
        public void NotNull_WhenObjectIsNull_ThenThereIsAnException()
        {
            this.Invoking(s => Require.NotNull(null, "test"))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void NotNull_WhenObjectIsNotNull_ThenNothingHappens()
        {
            Require.NotNull(new object(), "test");
        }

        [TestCase(null)]
        [TestCase("")]
        public void NotEmpty_WhenStringIsNullOrEmpty_ThenThereIsAnException(string str)
        {
            this.Invoking(s => Require.NotEmpty(str, nameof(str)))
                .Should().Throw<ArgumentNullException>();
        }

        [TestCase("test")]
        [TestCase(" ")]
        public void NotEmpty_WhenStringIsNotNullOrEmpty_ThenNothingHappens(string str)
        {
            Require.NotEmpty(str, nameof(str));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void NotWhiteSpace_WhenStringIsNullOrWhiteSpace_ThenThereIsAnException(string str)
        {
            this.Invoking(s => Require.NotWhiteSpace(str, nameof(str)))
                .Should().Throw<ArgumentNullException>();
        }

        [TestCase("test")]
        public void NotWhiteSpace_WhenStringIsNotNullOrWhiteSpace_ThenNothingHappens(string str)
        {
            Require.NotWhiteSpace(str, nameof(str));
        }

        [TestCase(-1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        public void IndexInRange_WhenIndexIsNotInRange_ThenThereIsAnException(int current, int length)
        {
            this.Invoking(s => Require.IndexInRange(current, length, nameof(current)))
                .Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestCase(0, 2)]
        [TestCase(1, 2)]
        public void IndexInRange_WhenIndexIsInRange_ThenNothingHappens(int current, int length)
        {
            Require.IndexInRange(current, length, nameof(current));
        }

        [TestCase(-1, 0, 2)]
        [TestCase(3, 0, 2)]
        public void InRange_WhenIsNotInRange_ThenThereIsAnException(double current, double min, double max)
        {
            this.Invoking(s => Require.InRange(current, min, max, nameof(current)))
                .Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestCase(0, 0, 2)]
        [TestCase(1, 0, 2)]
        [TestCase(2, 0, 2)]
        public void InRange_WhenIsInRange_ThenNothingHappens(double current, double min, double max)
        {
            Require.InRange(current, min, max, nameof(current));
        }
    }
}
