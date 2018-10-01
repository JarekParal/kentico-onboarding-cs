using NUnit.Framework.Constraints;

namespace TodoList.Api.Tests.Utils
{
    public static class ExtensionItemCompare
    {
        public static EqualConstraint UsingItemCompare(this EqualConstraint equalConstraint)
        {
            return equalConstraint.Using(new ItemCompare());
        }
    }
}
