using System;
using NUnit.Framework.Constraints;

namespace TodoList.Api.Tests.Extensions
{
    public static class EqualConstraintExtensions
    {
        public static EqualConstraint UsingItemComparer(this EqualConstraint equalConstraint)
        {
            return equalConstraint.Using(new ItemComparer());
        }
    }
}
