using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public static Project ReadProject(string path)
        {
            string jsonString = File.ReadAllText(path);
            Project project = JsonSerializer.Deserialize<Project>(jsonString);
            return project;
        }
    }
}
