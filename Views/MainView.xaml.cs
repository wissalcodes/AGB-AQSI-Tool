using AGB_AQSI_ExcelTool.ViewModels;
using DevExpress.Xpf.WindowsUI;
using System;
using System.Windows;
using System.Windows.Controls;


namespace AGB_AQSI_ExcelTool.Views
{
    public partial class MainView : NavigationPage
    {
        public MainView()
        {
            InitializeComponent();
            var viewModel = new MainViewModel();
            this.DataContext = viewModel;
            
        }

        private void NavigateToPage(object sender, RoutedEventArgs e)
        {
            string pageName = (sender as Button)?.Tag.ToString();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                switch (pageName)
                {
                    case "Importation":
                        mainWindow.navigationFrame.Navigate(new Uri("Views/Importation.xaml", UriKind.Relative));
                        break;
                    case "ManageMembers":
                        mainWindow.navigationFrame.Navigate(new Uri("Views/ManageMembers.xaml", UriKind.Relative));
                        break;
                    case "History":
                        mainWindow.navigationFrame.Navigate(new Uri("Views/History.xaml", UriKind.Relative));
                        break;
                    case "MainView":
                        mainWindow.navigationFrame.Navigate(new Uri("Views/MainView.xaml", UriKind.Relative));
                        break;
                }
            }
        }
     
    }
}
