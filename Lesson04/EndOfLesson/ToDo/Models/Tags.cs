using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class Tags
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<ToDo> ToDos { get; set; }
    }
}
