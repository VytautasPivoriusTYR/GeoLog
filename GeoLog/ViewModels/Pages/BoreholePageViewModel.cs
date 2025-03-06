using GeoLog.Models;
using GeoLog.Views.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace GeoLog.ViewModels.Pages
{
    public partial class BoreholePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private NavigationViewItem _layersMenu;

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new();

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new();

        public BoreholePageViewModel()
        {
            InitializeMenuItems();
        }

        private void InitializeMenuItems()
        {
            _footerMenuItems = new ObservableCollection<object>
            {
                new NavigationViewItem()
                {
                    Content = "Pridėti sluoksnį",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Add24 },
                    TargetPageType = typeof(CreateNewLayerPage)
                },
            };
        }

        public void LoadBoreholeLayers()
        {
            _menuItems.Clear();

            if (ProjectData.CurrentBorehole == null || ProjectData.CurrentBorehole.Layers == null)
            {
                return;
            }

            foreach (var layer in ProjectData.CurrentBorehole.Layers)
            {
                var item = new NavigationViewItem
                {
                    Content = layer.Thickness.ToString(),
                    TargetPageType = typeof(CreateNewLayerPage),
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Layer20 },
                };

                item.Click += (_, _) =>
                {
                    ProjectData.CurrentBoreholeLayer = layer;
                };

                _menuItems.Add(item);

            }
        }

    }
}
