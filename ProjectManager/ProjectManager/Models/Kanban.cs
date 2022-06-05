using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public class Kanban
    {
        public List<TodoTask> ToDo { get; set; }
        public List<TodoTask> InProgress { get; set; }
        public List<TodoTask> Done { get; set; }
        public Kanban()
        {
            ToDo = new List<TodoTask>();
            InProgress = new List<TodoTask>();
            Done = new List<TodoTask>();
        }
    }
}
