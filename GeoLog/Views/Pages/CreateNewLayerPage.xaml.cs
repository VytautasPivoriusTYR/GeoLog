using GeoLog.Models;
using GeoLog.Services;
using GeoLog.Services.Interfaces;
using GeoLog.ViewModels.Pages;
using GeoLog.ViewModels.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GeoLog.Views.Pages
{
    /// <summary>
    /// Interaction logic for CreateNewLayerPage.xaml
    /// </summary>
    public partial class CreateNewLayerPage : Page
    {
        private readonly BoreholePageViewModel _boreholePageViewModel;
        private readonly ProjectService _projectService = new ProjectService();

        public CreateNewLayerPage()
        {
            InitializeComponent();

        }

        private void buttonCreateLayer_Click(object sender, RoutedEventArgs e)
        {
            var text = GetRichTextBoxText(richTextLayerDescription);
            var thickness = double.Parse(numberBoxLayerThickness.Text);

            var layer = new BoreholeLayer(text, thickness);

            _projectService.AddBoreholeLayerToProject(layer);
            _boreholePageViewModel.LoadBoreholeLayers();

        }

        private string GetRichTextBoxText(RichTextBox richTextBox)
        {
            TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            return textRange.Text.Trim(); // Trim removes extra line breaks
        }
    }
}
