using System;
using System.Threading.Tasks;
using Moq;
using Moq.Language.Flow;

namespace Xamarin.Basics.Tests.Helpers.Builders
{
    public class MockSetupBuilder<T, TResult> 
        where T : class
        where TResult : class
    {
        private readonly MockBuilder<T> _mockBuilder;
        private readonly ISetup<T, TResult> _setup;

        public MockSetupBuilder(MockBuilder<T> mockBuilder, ISetup<T, TResult> setup)
        {
            _mockBuilder = mockBuilder;
            _setup = setup;
        }

        public MockBuilder<T> Returns(TResult result)
        {
            _setup.Returns(result);
            return _mockBuilder;
        }

        public MockBuilder<T> Returns(Mock<TResult> result)
        {
            _setup.Returns(result.Object);
            return _mockBuilder;
        }
        
        public MockBuilder<T> Throws<TException>(TException exception) where TException : Exception
        {
            _setup.Throws(exception);
            return _mockBuilder;
        }
    }
    
    public class MockAsyncSetupBuilder<T, TResult> 
        where T : class
        where TResult : class
    {
        private readonly MockBuilder<T> _mockBuilder;
        private readonly ISetup<T, Task<TResult>> _setup;

        public MockAsyncSetupBuilder(MockBuilder<T> mockBuilder, ISetup<T, Task<TResult>> setup)
        {
            _mockBuilder = mockBuilder;
            _setup = setup;
        }

        public MockBuilder<T> Returns(TResult result)
        {
            _setup.ReturnsAsync(result);
            return _mockBuilder;
        }

        public MockBuilder<T> Returns(Mock<TResult> result)
        {
            _setup.ReturnsAsync(result.Object);
            return _mockBuilder;
        }

        public MockBuilder<T> Throws<TException>(TException exception) where TException : Exception
        {
            _setup.ThrowsAsync(exception);
            return _mockBuilder;
        }
    }
}