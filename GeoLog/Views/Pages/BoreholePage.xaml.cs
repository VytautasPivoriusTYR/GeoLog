using GeoLog.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        public BoreholePageViewModel ViewModel;


        private readonly INavigationService _navigationService;

        public BoreholePage(BoreholePageViewModel viewModel, INavigationService navigationService)
        {
            InitializeComponent(); // Ensure XAML elements are loaded first

            ViewModel = viewModel;
            DataContext = ViewModel;

            _navigationService = navigationService;
        }




    }
}
