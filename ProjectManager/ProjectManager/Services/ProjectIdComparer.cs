using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectManager.Models;

namespace ProjectManager.Services
{
    public class ProjectIdComparer : IComparer<Project>
    {
        public int Compare(Project x, Project y)
        {
            if(x.Id < y.Id)
                return -1;
            if(x.Id > y.Id)
                return 1;
            return 0;
        }
    }
}
