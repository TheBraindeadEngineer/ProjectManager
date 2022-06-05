using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
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
    public sealed partial class AddProject : Page
    {
        public AddProject()
        {
            this.InitializeComponent();
            CreatedOnPicker.SelectedDate = new DateTimeOffset(DateTime.Now);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim() == String.Empty ||
                DescriptionTextBox.Text.Trim() == String.Empty)
                return;

            Project newProject = new Project()
            {
                Id = Project.GetFirstValidId(),
                Name = NameTextBox.Text.Trim(),
                Description = DescriptionTextBox.Text.Trim(),
                Created = CreatedOnPicker.SelectedDate.Value.Date,
                LastUpdated = CreatedOnPicker.SelectedDate.Value.Date
            };

            Project.SaveProject(newProject);

            this.Frame.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
