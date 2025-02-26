using GeoLog.Models;
using GeoLog.Services;
using GeoLog.Services.Interfaces;
using GeoLog.ViewModels.Pages;
using GeoLog.ViewModels.Windows;
using System.Windows.Controls;

namespace GeoLog.Views.Pages
{
    /// <summary>
    /// Interaction logic for CreateNewBorehole.xaml
    /// </summary>
    public partial class CreateNewBorehole : Page
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IProjectService _projectService;


        public CreateNewBorehole(IProjectService projectService,MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _projectService = projectService;
            _mainWindowViewModel = mainWindowViewModel;
        }

        private void buttonCreateBorehole_Click(object sender, RoutedEventArgs e)
        {
            var BoreholeNumber = textBoxBoreholeName.Text;
            _projectService.AddBoreholeToProject(BoreholeNumber);

            _mainWindowViewModel.LoadBoreholes(ProjectData.CurrentProject);

        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the CreateNewBorehole page
        }
    }
}
