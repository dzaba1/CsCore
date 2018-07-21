using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;

namespace Dzaba.Utils.Tests
{
    [TestFixture]
    public class ReflectionExtensionsTests
    {
        [Serializable]
        private class TestClass : IDisposable
        {
            public void Dispose()
            {
                
            }
        }

        private class TestClass2
        {
            public TestClass2(int value)
            {

            }
        }

        [Test]
        public void IsNullable_WhenItsNullable_ThenItReturnsTrue()
        {
            var type = typeof(int?);
            type.IsNullable().Should().BeTrue();
        }

        [Test]
        public void IsNullable_WhenItsNotNullable_ThenItReturnsFalse()
        {
            object obj = "aaa";
            obj.GetType().IsNullable().Should().BeFalse();
        }

        [Test]
        public void HasCustomAttribute_WhenAMemeberHasAnAttribute_ThenItReturnsTrue()
        {
            var obj = new TestClass();
            obj.GetType().HasCustomAttribute<SerializableAttribute>().Should().BeTrue();
        }

        [Test]
        public void HasCustomAttribute_WhenAMemeberDoesntHaveAnAttribute_ThenItReturnsFalse()
        {
            var obj = new TestClass();
            obj.GetType().HasCustomAttribute<TestFixtureAttribute>().Should().BeFalse();
        }

        [Test]
        public void ImplementsInterface_WhenATypeImplementsAnInterface_ThenItReturnsTrue()
        {
            var obj = new TestClass();
            obj.GetType().ImplementsInterface<IDisposable>().Should().BeTrue();
        }

        [Test]
        public void ImplementsInterface_WhenATypeDoesntImplementAnInterface_ThenItReturnsFalse()
        {
            var obj = new TestClass();
            obj.GetType().ImplementsInterface<ICloneable>().Should().BeFalse();
        }

        [Test]
        public void HasParameterlessConstructor_WhenATypeHasAParameterlessConstructor_ThenItReturnsTrue()
        {
            var obj = new TestClass();
            obj.GetType().HasParameterlessConstructor().Should().BeTrue();
        }

        [Test]
        public void HasParameterlessConstructor_WhenATypeDoesntHaveAParameterlessConstructor_ThenItReturnsFalse()
        {
            var obj = new TestClass2(1);
            obj.GetType().HasParameterlessConstructor().Should().BeFalse();
        }

        [Test]
        public void GetResourceStream_WhenAResourceExists_ThenItReturnsAStream()
        {
            using (var stream = GetType().Assembly.GetResourceStream(@"Resources\Test.txt"))
            {
                stream.Should().NotBeNull();
            }
        }

        [Test]
        public void GetResourceStream_WhenAResourceDoesntExist_ThenThereIsAnException()
        {
            this.Invoking(s => GetType().Assembly.GetResourceStream("Invalid.txt"))
                .Should().Throw<FileNotFoundException>();
        }

        [Test]
        public void GetResourceText_WhenAResourceExists_ThenItReturnsAText()
        {
            var result = GetType().Assembly.GetResourceText(@"Resources\Test.txt");
            result.Should().Be("Test");
        }
    }
}
