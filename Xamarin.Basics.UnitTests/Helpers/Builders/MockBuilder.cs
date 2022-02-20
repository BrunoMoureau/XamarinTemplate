using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;

namespace Xamarin.Basics.Tests.Helpers.Builders
{
    public class MockBuilder<T> where T : class
    {
        private readonly Mock<T> _mock;

        public MockBuilder()
        {
            _mock = new Mock<T>();
        }

        public MockBuilder(MockBehavior behavior, params object[] args)
        {
            _mock = new Mock<T>(behavior, args);
        }
        
        public MockSetupBuilder<T, TResult> Calling<TResult>(Expression<Func<T, TResult>> expression) where TResult : class
        {
            var setup = _mock.Setup(expression);
            return new MockSetupBuilder<T, TResult>(this, setup);
        }

        public MockAsyncSetupBuilder<T, TResult> Calling<TResult>(Expression<Func<T, Task<TResult>>> expression) where TResult : class
        {
            var setup = _mock.Setup(expression);
            return new MockAsyncSetupBuilder<T, TResult>(this, setup);
        }

        public static implicit operator Mock<T>(MockBuilder<T> builder)
        {
            return builder.Mock();
        }

        private Mock<T> Mock() => _mock;
    }
}