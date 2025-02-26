using GeoLog.Models;
using GeoLog.Services;
using GeoLog.ViewModels.Windows;
using System.Windows.Controls;

namespace GeoLog.Views.Pages
{
    /// <summary>
    /// Interaction logic for CreateNewProjectPage.xaml
    /// </summary>
    public partial class CreateNewProjectPage : Page
    {
        private readonly FileService fileService = new FileService();

        private readonly MainWindowViewModel _mainWindowViewModel;

        public CreateNewProjectPage(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
        }

        private void buttonCreateProject_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxProjectName.Text == null || textBoxProjectName.Text.Length < 3)
            {
                MessageBox.Show("Projekto pavadinimas neįvestas arba per trumpas");
                return;
            }

            if (fileService.IsfileExist(@$"{ExternalData.ProjectsDirectory}\{textBoxProjectName.Text}"))
            {
                MessageBox.Show("Projektas tokiu pavadinimu jau egzistuoja");
                return;
            }
            var newProject = new Project(textBoxProjectName.Text, textBoxProjectDescription.Text);
            fileService.CreateJsonFile(ExternalData.ProjectsDirectory, textBoxProjectName.Text, newProject);

            _mainWindowViewModel.AddNewProject(newProject);
        }
    }
}
