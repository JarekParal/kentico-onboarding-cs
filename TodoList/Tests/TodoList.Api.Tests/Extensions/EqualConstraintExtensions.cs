using System;
using NUnit.Framework.Constraints;
using TodoList.Api.Tests.Comparers;

namespace TodoList.Api.Tests.Extensions
{
    public static class EqualConstraintExtensions
    {
        private static readonly Lazy<ItemComparer> s_itemComparerLazy = new Lazy<ItemComparer>();

        public static EqualConstraint UsingItemComparer(this EqualConstraint equalConstraint)
            => equalConstraint.Using(s_itemComparerLazy.Value);
    }
}
