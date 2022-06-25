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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using ProjectManager.Models;
using Microsoft.UI.Xaml.Media.Animation;

namespace ProjectManager.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowKanban : Page
    {
        public ShowKanban()
        {
            this.InitializeComponent();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewTodoTask),null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void MoveLeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (InProgressListView.SelectedIndex != -1)
            {
                object task = InProgressListView.SelectedItem;
                ToDoListView.Items.Add(task);
                ToDoListView.SelectedItem = task;
                InProgressListView.Items.Remove(task);
            }
            else if (DoneListView.SelectedIndex != -1)
            {
                object task = DoneListView.SelectedItem;
                InProgressListView.Items.Add(task);
                InProgressListView.SelectedItem = task;
                DoneListView.Items.Remove(task);
            }
        }

        private void MoveRightButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToDoListView.SelectedIndex != -1)
            {
                object task = ToDoListView.SelectedItem;
                InProgressListView.Items.Add(task);
                InProgressListView.SelectedItem = task;
                ToDoListView.Items.Remove(task);
            }
            else if (InProgressListView.SelectedIndex != -1)
            {
                object task = InProgressListView.SelectedItem;
                DoneListView.Items.Add(task);
                DoneListView.SelectedItem = task;
                InProgressListView.Items.Remove(task);
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            TodoTask task;

            if (ToDoListView.SelectedIndex != -1)
                task = (TodoTask)ToDoListView.SelectedItem;
            else if (InProgressListView.SelectedIndex != -1)
                task = (TodoTask)InProgressListView.SelectedItem;
            else if (DoneListView.SelectedIndex != -1)
                task = (TodoTask)DoneListView.SelectedItem;
            else
                return;

            this.Frame.Navigate(typeof(OpenTask), task, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToDoListView.SelectedIndex != -1)
                ToDoListView.Items.Remove(ToDoListView.SelectedItem);
            else if (InProgressListView.SelectedIndex != -1)
                InProgressListView.Items.Remove(InProgressListView.SelectedItem);
            else if (DoneListView.SelectedIndex != -1)
                DoneListView.Items.Remove(DoneListView.SelectedItem);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack(new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Kanban kanban = (Kanban)e.Parameter;

            ToDoListView.ItemsSource = kanban.ToDo;
            InProgressListView.ItemsSource = kanban.InProgress;
            DoneListView.ItemsSource = kanban.Done;
        }

        private void DoneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InProgressListView.SelectedIndex = -1;
            ToDoListView.SelectedIndex = -1;
        }

        private void InProgressListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoneListView.SelectedIndex = -1;
            ToDoListView.SelectedIndex = -1;
        }

        private void ToDoListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoneListView.SelectedIndex = -1;
            InProgressListView.SelectedIndex = -1;
        }
    }
}
