<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
using System;
=======
﻿using System;
using System.ComponentModel.DataAnnotations;
>>>>>>> ccc475f52a86799efa405dbc73d0b57abff61c5a

namespace ToDoApp.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [UIHint("Status")]
        public Status Status { get; set; }
<<<<<<< HEAD
        public DateTime Created { get; set; } 
=======
        public DateTime Created { get; set; }
>>>>>>> ccc475f52a86799efa405dbc73d0b57abff61c5a
    }
}
