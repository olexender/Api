using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AmazingCo.Api.Tests
{
    /// <summary>
    /// This serves as a wrapper for class variables that exist in parallelized fixtures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TestLocal<T> : ITestLocal<T>
    {
        private readonly Dictionary<string, T> _dict = new Dictionary<string, T>();
        private readonly Func<T> _function;
        private readonly object _lock = new object();

        public T Value
        {
            get
            {
                lock (_lock)
                {
                    if (!_dict.ContainsKey(GetTestId()) && _function != null)
                        _dict[GetTestId()] = _function();
                    return _dict.ContainsKey(GetTestId()) ? _dict[GetTestId()] : default(T);
                }
            }
            set
            {
                lock (_lock)
                {
                    _dict[GetTestId()] = value;
                }
            }
        }

        private string GetTestId()
        {
            return TestContext.CurrentContext.Test.FullName;
        }

        public TestLocal(Func<T> function = null)
        {
            _function = function;
        }

        public static implicit operator T(TestLocal<T> local)
        {
            return local.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is T)
            {
                return Value.Equals(obj);
            }

            return base.Equals(obj);
        }
    }

    public interface ITestLocal<out T>
    {
        T Value { get; }
    }
}
