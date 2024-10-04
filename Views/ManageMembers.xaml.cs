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
                // Get the selected tester's username to delete
                var username = selectedTesteur.Name; // Assuming the Testeur class has a Username property

                // Call the ViewModel method to delete the selected tester
                vm.SupprimerTesteur(username);

                MessageBox.Show("Testeur supprimé avec succès");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un testeur à supprimer");
            }
        }

        public void SupprimerTesteurs_Click(object sender, RoutedEventArgs e)
        {
            // check if the grid has any selected items
            var selectedTesteurs = TesteursGrid.SelectedItems.Cast<Testeur>().ToList();

            if (selectedTesteurs.Any())
            {
                // Get the list of selected tester usernames to delete
                var usernames = selectedTesteurs.Select(t => t.Name);
                var joinedUsernames = string.Join(",", usernames);
                vm.SupprimerPlusieursTesteurs(joinedUsernames);            }
            else
            {
                MessageBox.Show("Veuillez sélectionner des testeurs à supprimer");
            }
        }
    }
}
