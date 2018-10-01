using System;
using System.Collections.Generic;

namespace TodoList.Api.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
            => $"{nameof(Id)}: {Id}, {nameof(Text)}: {Text}";
    }
}