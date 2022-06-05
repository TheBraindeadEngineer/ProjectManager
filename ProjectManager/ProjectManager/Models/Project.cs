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
        public int Id { get; set; }
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

        static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ProjectManager\projects";

        public static string ProjectFolderPath { get { return path; } }

        public static List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();

            if (!Directory.Exists(path))
                return projects;

            string[] files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                if (file.EndsWith(".bak"))
                    continue;
                projects.Add(Project.ReadProject(file));
            }

            return projects;
        }

        public static void SaveProject(Project project)
        {
            if (!Directory.Exists(path))
                return;

            string fileName = project.Name.Replace(" ", "") + ".json";

            if(File.Exists(path + "\\" + fileName))
                File.Move(path + "\\" + fileName, path + "\\" + fileName + ".bak");

            File.WriteAllTextAsync(path + "\\" + fileName, JsonSerializer.Serialize(project, new JsonSerializerOptions() { WriteIndented = true}));
        }

        private static Dictionary<int, string> GetProjectPathDictionary()
        {
            Dictionary<int, string> projects = new Dictionary<int, string>();

            if (!Directory.Exists(path))
                return null;

            string[] files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                if (file.EndsWith(".bak"))
                    continue;
                projects.Add(Project.ReadProject(file).Id, file);
            }

            return projects;
        }

        public static void DeleteProject(Project project)
        {
            Dictionary<int, string> projects = GetProjectPathDictionary();

            if (projects == null)
                return;

            if (!projects.ContainsKey(project.Id))
                return;

            File.Delete(projects[project.Id]);
        }

        public static void EditProject(int projectId, Project newData)
        {
            string filePath = GetProjectPathDictionary()[projectId];

            if (!File.Exists(filePath))
                return;

            File.Delete(filePath);
            SaveProject(newData);
        }

        public static int GetFirstValidId()
        {
            List<Project> projects = GetProjects();

            if (projects.Count == 0)
                return 0;

            projects.Sort(new Services.ProjectIdComparer());
            int prevId = projects.First().Id;

            for(int i = 1; i < projects.Count; i++)
            {
                if (projects[i].Id - prevId > 1)
                    return prevId + 1;
                else
                    prevId = projects[i].Id;
            }
            
            return prevId + 1;
        }
    }
}
