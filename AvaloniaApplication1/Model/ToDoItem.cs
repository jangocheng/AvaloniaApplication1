using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Model
{
    class ToDoItem
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string ToDoText { get; set; } = String.Empty;

        public override string ToString()
        {
            string text = ToDoText.Length > 17 ? ToDoText.Substring(0, 17) : ToDoText;
            return $"{DateTime} - {text}";
        }
    }
}
