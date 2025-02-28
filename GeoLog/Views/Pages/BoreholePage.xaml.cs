using GeoLog.Models;
using GeoLog.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<string> Layers { get; set; } = new ObservableCollection<string>();

        public BoreholePage(BoreholePageViewModel viewModel, INavigationService navigationService)
        {
            InitializeComponent(); // Ensure XAML elements are loaded first

            ViewModel = viewModel;
            DataContext = ViewModel;

            _navigationService = navigationService;
            BoreholeList.ItemsSource = Layers;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BoreholeList.ItemsSource = Layers;
        }



       

        private void BoreholeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void buttonCreate(object sender, RoutedEventArgs e)
        {
           
        }

        private void buttonDelete(object sender, RoutedEventArgs e)
        {
            if (BoreholeList.SelectedItem is string selectedItem)
            {
                Layers.Remove(selectedItem);
            }
        }
    }
}
