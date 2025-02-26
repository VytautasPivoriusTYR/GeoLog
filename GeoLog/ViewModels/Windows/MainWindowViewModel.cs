using GeoLog.Models;
using GeoLog.Services;
using GeoLog.Services.Interfaces;
using GeoLog.Views.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace GeoLog.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {

        private readonly IProjectService _projectService;

        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private string _applicationTitle = "Tyrens GeoLog";

        [ObservableProperty]
        private ObservableCollection<Project> _projectList = new();

        [ObservableProperty]
        private Project _selectedProject;



        [ObservableProperty]
        private NavigationViewItem _boreholesMenu;

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new();


        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems;


        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems;




        public MainWindowViewModel(IProjectService projectService, INavigationService navigationService)
        {
            _projectService = projectService;
            _navigationService = navigationService;

            InitializeMenuItems();
            LoadProjects();
        }

        private void InitializeMenuItems()
        {
            _boreholesMenu = new NavigationViewItem
            {
                Content = "Gręžiniai",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(HomePage)
            };

            _menuItems = new ObservableCollection<object>
            {
                new NavigationViewItem()
                {
                    Content = "Pradinis",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                    TargetPageType = typeof(HomePage)
                },
                _boreholesMenu
            };

            _footerMenuItems = new ObservableCollection<object>
            {
                new NavigationViewItem()
                {
                    Content = "Sukurti naują gręžinį",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Add24 },
                    TargetPageType = typeof(CreateNewBorehole)
                },
                new NavigationViewItem()
                {
                    Content = "Nustatymai",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    TargetPageType = typeof(SettingsPage)
                }
            };

            _trayMenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Header = "Pradinis", Tag = "tray_home" }
            };
        }
        private void LoadProjects()
        {
            var existingProjects = _projectService.ReadAllJsonFiles<Project>(ExternalData.ProjectsDirectory);
            _projectList = new ObservableCollection<Project>(existingProjects);

            if (_projectList.Any())
            {
                SelectedProject = _projectList.First();
                ProjectData.CurrentProject = SelectedProject;
            }
        }

        public void AddNewProject(Project project)
        {
            if (project == null) return;

            var projectItem = new NavigationViewItem
            {
                Content = project.Name,
                TargetPageType = typeof(BoreholePage)
            };

            projectItem.Click += (_, _) => ProjectData.CurrentProject = project;

            _projectList.Add(project);
            SelectedProject = project;
        }

        partial void OnSelectedProjectChanged(Project value)
        {
            if (value == null) return;

            ProjectData.CurrentProject = value;
            LoadBoreholes(value);
        }

        public void LoadBoreholes(Project project)
        {
            _boreholesMenu.MenuItems.Clear();

            foreach (var borehole in project.Boreholes)
            {
                var boreholeItem = new NavigationViewItem
                {
                    Content = borehole.Name,
                    TargetPageType = typeof(BoreholePage)
                };
                _boreholesMenu.MenuItems.Add(boreholeItem);

            }
        }

        [RelayCommand]
        private void CreateNewProject()
        {
            _navigationService?.Navigate(typeof(CreateNewProjectPage));
        }


    }
}
