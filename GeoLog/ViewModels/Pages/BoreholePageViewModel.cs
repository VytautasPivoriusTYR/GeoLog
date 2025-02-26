using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace GeoLog.ViewModels.Pages
{
    public partial class BoreholePageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Layer 1",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Layer24 },
                TargetPageType = typeof(Views.Pages.HomePage)
            },
            new NavigationViewItem()
            {
                Content = "Layer 2",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Layer24 },
                TargetPageType = typeof(Views.Pages.HomePage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Add New Layer",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Add24 },
                TargetPageType = typeof(Views.Pages.HomePage)
            }
        };
        public BoreholePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void NavigateTo(Type pageType)
        {
            _navigationService.Navigate(pageType);
        }

    }
}
