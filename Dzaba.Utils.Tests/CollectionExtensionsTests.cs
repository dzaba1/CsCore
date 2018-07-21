using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Dzaba.Utils.Tests
{
    [TestFixture]
    public class CollectionExtensionsTests
    {
        [Test]
        public void Swap_WhenIndexIsTheSame_ThenElementsAreNotSwapped()
        {
            var array = new[] { 1, 2 };
            array.Swap(0, 0);
            array[0].Should().Be(1);
            array[1].Should().Be(2);
        }

        [Test]
        public void Swap_WhenIndexesProvided_ThenElementsAreSwapped()
        {
            var array = new[] { 1, 2 };
            array.Swap(0, 1);
            array[0].Should().Be(2);
            array[1].Should().Be(1);
        }

        [Test]
        public void AddRange_WhenCollectionIsNull_ThenNothingHappens()
        {
            var col = new HashSet<int>();
            col.AddRange(null);
            col.Count.Should().Be(0);
        }

        [Test]
        public void AddRange_WhenCollectionProvided_ThenElementsAreAdded()
        {
            var col = new HashSet<int>();
            col.AddRange(new[] { 1, 2 });
            col.Count.Should().Be(2);
            col.ElementAt(0).Should().Be(1);
            col.ElementAt(1).Should().Be(2);
        }
    }
}
