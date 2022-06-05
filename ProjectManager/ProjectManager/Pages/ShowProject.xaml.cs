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
    public sealed partial class ShowProject : Page
    {
        public ShowProject()
        {
            this.InitializeComponent();
        }

        private void OpenKanban_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ShowKanban), showProject.Kanban);
        }

        private void OpenFileTree_Click(object sender, RoutedEventArgs e)
        {

        }

        bool unsaved = false;

        Project showProject;

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.IsEnabled = true;
            DescriptionTextBox.IsEnabled = true;
            CreatedOnPicker.IsEnabled = true;
        }

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            unsaved = true;
        }

        private void OnDateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            unsaved = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!unsaved)
            {
                NameTextBox.IsEnabled = false;
                DescriptionTextBox.IsEnabled = false;
                CreatedOnPicker.IsEnabled = false;
            }

            Project.EditProject(showProject.Id, new Project()
            {
                Id = showProject.Id,
                Name = NameTextBox.Text.Trim(),
                Description = DescriptionTextBox.Text.Trim(),
                Created = showProject.Created,
                LastUpdated = DateTime.Now
            });

            unsaved = false;

            NameTextBox.IsEnabled = false;
            DescriptionTextBox.IsEnabled = false;
            CreatedOnPicker.IsEnabled = false;
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (!unsaved)
            {
                this.Frame.GoBack();
                return;
            }

            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = $"You have not saved! Are you sure you want to exit?";
            dialog.PrimaryButtonText = "Yes";
            dialog.CloseButtonText = "No";
            dialog.DefaultButton = ContentDialogButton.Close;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
                this.Frame.GoBack();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Project project = (Project)e.Parameter;

            NameTextBox.IsEnabled = false;
            NameTextBox.Text = project.Name;

            DescriptionTextBox.IsEnabled = false;
            DescriptionTextBox.Text = project.Description;

            CreatedOnPicker.IsEnabled = false;
            CreatedOnPicker.SelectedDate = new DateTimeOffset(project.Created);

            showProject = project;
        }
    }
}
