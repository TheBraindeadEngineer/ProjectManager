using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Roles Role { get; set; }
        public List<int> AssignedProjects { get; set; }
        public enum Roles
        {
            Developer,
            Manager,
            Marketing
        }
    }
}
