using System;

namespace TodoList.Contracts.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
            => $"{nameof(Id)}: {Id}, {nameof(Text)}: {Text}";
    }
}