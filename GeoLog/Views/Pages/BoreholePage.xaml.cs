using GeoLog.ViewModels.Pages;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace GeoLog.Views.Pages
{
    /// <summary>
    /// Interaction logic for BoreholePage.xaml
    /// </summary>
    public partial class BoreholePage : Page
    {
        public BoreholePageViewModel ViewModel { get; }


        public BoreholePage(BoreholePageViewModel viewModel, IPageService pageService, INavigationService navigationService)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent(); // Ensure XAML elements are loaded first

            SetPageService(pageService);

            navigationService.SetNavigationControl(LayerNavigation);

        }

        public INavigationView GetNavigation() => LayerNavigation;

        public bool Navigate(Type pageType) => LayerNavigation.Navigate(pageType);

        public void SetPageService(IPageService pageService) => LayerNavigation.SetPageService(pageService);


    }
}
