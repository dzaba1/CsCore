using FluentAssertions;
using NUnit.Framework;
using System;

namespace Dzaba.Utils.Tests
{
    [TestFixture]
    public class WeakLazyTests
    {
        private int buildCount = 0;

        private class TestClass { }

        [SetUp]
        public void Setup()
        {
            buildCount = 0;
        }

        private WeakLazy<TestClass> CreateSut()
        {
            return new WeakLazy<TestClass>(() => {
                buildCount++;
                return new TestClass();
            });
        }

        [Test]
        public void Value_WhenCalled_ThenItRecreatesTheObject()
        {
            var sut = CreateSut();
            sut.Value.Should().NotBeNull();
            buildCount.Should().Be(1);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            sut.Value.Should().NotBeNull();
            buildCount.Should().Be(2);
        }
    }
}
