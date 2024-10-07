using DevExpress.Xpf.WindowsUI;
using System;
using System.Linq;
using System.Windows;
using AGB_AQSI_ExcelTool.ViewModels;
using AGB_AQSI_ExcelTool.Models;
namespace AGB_AQSI_ExcelTool.Views
{
  
    public partial class ManageMembers : NavigationPage
    {
        private ManageMembersViewModel vm;

        public ManageMembers()
        {
            InitializeComponent();
            vm = new ManageMembersViewModel();
            this.DataContext = vm;
        }
        private void BackToMainView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.navigationFrame.Navigate(new Uri("Views/MainView.xaml", UriKind.Relative));
            }
        }

        private void AjouterTesteur_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextEdit.Text.Trim();
            string nom = NomTextEdit.Text.Trim();
            string prenom = PrenomTextEdit.Text.Trim();
           
            if (username == null)
            {
                MessageBox.Show("Veuillez saisir le nom d'utilisteur");
                return;
            }
            if (nom == null)
            {
                MessageBox.Show("Veuillez saisir le nom");
                return;
            }
            if (prenom == null)
            {
                MessageBox.Show("Veuillez saisir le prenom");
                return;
            }
            try
            {
                vm.AjouterTesteur(username, nom, prenom);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SupprimerTesteur_Click(object sender, RoutedEventArgs e)
        {
            // Check if the grid has any selected items
            var selectedTesteur = TesteursGrid.SelectedItem as Testeur;

            if (selectedTesteur != null)
            {
                // Confirm deletion
                var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce testeur ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // get the selected tester's username to delete
                    var username = selectedTesteur.Name; 

                    vm.SupprimerTesteur(username);
                    MessageBox.Show("Testeur supprimé avec succès");
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un testeur à supprimer");
            }
        }

        public void SupprimerTesteurs_Click(object sender, RoutedEventArgs e)
        {
            // Check if the grid has any selected items
            var selectedTesteurs = TesteursGrid.SelectedItems.Cast<Testeur>().ToList();

            if (selectedTesteurs.Any())
            {
                // Confirm deletion
                var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ces testeurs ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // get the list of selected tester usernames to delete
                    var usernames = selectedTesteurs.Select(t => t.Name);
                    var joinedUsernames = string.Join(",", usernames);
                    vm.SupprimerPlusieursTesteurs(joinedUsernames);

                    MessageBox.Show("Testeurs supprimés avec succès");
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner des testeurs à supprimer");
            }
        }
    }
}
