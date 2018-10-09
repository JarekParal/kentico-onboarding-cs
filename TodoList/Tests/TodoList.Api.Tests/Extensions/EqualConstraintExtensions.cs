using System;
using NUnit.Framework.Constraints;
using TodoList.Api.Tests.Comparers;

namespace TodoList.Api.Tests.Extensions
{
    public static class EqualConstraintExtensions
    {
        public static EqualConstraint UsingItemComparer(this EqualConstraint equalConstraint)
            => equalConstraint.Using(ItemComparer.Comparer);
    }
}
