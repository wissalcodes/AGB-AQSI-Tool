using AGB_AQSI_ExcelTool.ViewModels;
using DevExpress.Xpf.WindowsUI;
using System.Windows;
using System;

namespace AGB_AQSI_ExcelTool.Views
{
    public partial class History : NavigationPage
    {
        private HistoryViewModel VM;
        public History()
        {
            InitializeComponent();
            VM = new HistoryViewModel();
            this.DataContext = VM;
        }

        private void BackToMainView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.navigationFrame.Navigate(new Uri("Views/MainView.xaml", UriKind.Relative));
            }
        }
    }
}
