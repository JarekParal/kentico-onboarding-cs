using System.Collections.Generic;
using TodoList.Api.Models;

namespace TodoList.Api.Tests.Utils
{
    public class ItemCompare : IEqualityComparer<Item>
    {
        public bool Equals(Item i1, Item i2)
        {
            if (i1 == null || i2 == null)
            {
                return i1 == null && i2 == null;
            }
            return i1.Id.Equals(i2.Id) && i1.Text.Equals(i2.Text);
        }

        public int GetHashCode(Item item)
        {
            return (item.Id + item.Text).GetHashCode(); // TODO: check GetHashCode()
        }
    }
}