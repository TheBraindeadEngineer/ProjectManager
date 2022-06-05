using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public class TodoTask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public Priority TaskPriority { get; set; }
        public enum Priority
        {
            Low,
            Medium,
            High,
            Critical
        }
    }
}
