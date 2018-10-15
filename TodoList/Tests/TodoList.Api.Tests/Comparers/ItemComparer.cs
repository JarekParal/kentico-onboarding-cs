using System;
using System.Collections.Generic;
using TodoList.Api.Models;

namespace TodoList.Api.Tests.Comparers
{
    public class ItemComparer : IEqualityComparer<Item>
    {
        private ItemComparer() { }

        private static readonly Lazy<ItemComparer> s_itemComparer
            = new Lazy<ItemComparer>(() => new ItemComparer());

        public static ItemComparer Comparer => s_itemComparer.Value;

        public bool Equals(Item x, Item y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id.Equals(y.Id) && string.Equals(x.Text, y.Text);
        }

        public int GetHashCode(Item obj)
        {
            unchecked
            {
                return (obj.Id.GetHashCode() * 397) ^ (obj.Text != null ? obj.Text.GetHashCode() : 0);
            }
        }
    }
}