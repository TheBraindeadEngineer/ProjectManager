using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Text.Json;

using ProjectManager.Models;

namespace ProjectManager.Services
{
    public class MemberService
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ProjectManager\users.json";

        public static List<Member> GetMembers(int projectId)
        {
            if (!File.Exists(path))
                return null;
            string jsonString = File.ReadAllText(path);
            List<Member> members = JsonSerializer.Deserialize<List<Member>>(jsonString);
            return members.Where(member => member.AssignedProjects.Contains(projectId)).ToList();
        }
    }
}
