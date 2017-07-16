using System;
using System.Collections.Generic;
using Xunit;

namespace Mup.Tests
{
    public class Scanner_Ctor
    {
        private const string _method = (nameof(Scanner<int>) + "(IEnumerable<KeyValuePair<TTokenCode, Func<char, bool>>>): ");

        [Trait("Class", nameof(Scanner<int>))]
        [Fact(DisplayName = (_method + nameof(CannotHaveNullForPredicatesCollection)))]
        public void CannotHaveNullForPredicatesCollection()
        {
            Assert.Throws<ArgumentNullException>(() => new Scanner<int>(null));
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Fact(DisplayName = (_method + nameof(CannotHaveNullPredicates)))]
        public void CannotHaveNullPredicates()
        {
            Assert.Throws<ArgumentException>(() => new Scanner<int>(new[] { new KeyValuePair<int, Func<char, bool>>(0, null) }));
        }
    }
}