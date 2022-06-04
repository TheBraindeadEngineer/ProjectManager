using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

using ProjectManager.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjectManager.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Project> projectsFiltered = new ObservableCollection<Project>();

        List<Project> allProjects;

        public MainPage()
        {
            this.InitializeComponent();

            Reload_Click(this, null);
        }

        private void OnFilterChanged(object sender, TextChangedEventArgs e)
        {
            if (projectsFiltered.Count == 0)
                return;

            var filtered = allProjects.Where(project => Filter(project));
            RemoveNonMatching(filtered);
            AddBackProjects(filtered);
        }

        private bool Filter(Project project)
        {
            return project.Name.Contains(FilterByProjectName.Text, StringComparison.InvariantCultureIgnoreCase);
        }

        private void RemoveNonMatching(IEnumerable<Project> filteredData)
        {
            for (int i = projectsFiltered.Count - 1; i >= 0; i--)
            {
                var item = projectsFiltered[i];
                if (!filteredData.Contains(item))
                {
                    projectsFiltered.Remove(item);
                }
            }
        }

        private void AddBackProjects(IEnumerable<Project> filteredData)
        {
            foreach (var item in filteredData)
            {
                if (!projectsFiltered.Contains(item))
                    projectsFiltered.Add(item);
            }
        }

        private void DeleteProject(Project project)
        {
            allProjects.Remove(project);
            projectsFiltered.Remove(project);
        }

        private async void RemoveProject_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectListView.SelectedIndex == -1)
                return;

            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = $"Do you want to delete {(ProjectListView.Items[ProjectListView.SelectedIndex] as Project).Name}?";
            dialog.PrimaryButtonText = "Yes";
            dialog.CloseButtonText = "No";
            dialog.DefaultButton = ContentDialogButton.Close;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
                DeleteProject(GetSelectedProject());
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectListView.SelectedIndex == -1)
                return;

            this.Frame.Navigate(typeof(ShowProject), GetSelectedProject());
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            allProjects = GetProjects();

            if (allProjects.Count != 0)
            {
                projectsFiltered = new ObservableCollection<Project>(allProjects);

                ProjectListView.ItemsSource = projectsFiltered;
            }
        }

        private List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ProjectManager\projects";

            if (!Directory.Exists(path))
                return projects;

            string[] files = Directory.GetFiles(path);

            foreach(var file in files)
            {
                projects.Add(Project.ReadProject(file));
            }

            return projects;
        }

        private Project GetSelectedProject()
            => ProjectListView.SelectedItem as Project;
    }
}
