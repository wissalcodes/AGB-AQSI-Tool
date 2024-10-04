using AGB_AQSI_ExcelTool.Models;
using AGB_AQSI_ExcelTool.Services;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AGB_AQSI_ExcelTool.ViewModels
{
    public class ManageMembersViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public DatabaseService dbService;
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Username { get; set; }

        public ObservableCollection<Testeur> _testeurs;
        public ObservableCollection<Testeur> Testeurs
        {
            get => _testeurs;
            set
            {
                _testeurs = value;
                OnPropertyChanged(nameof(Testeurs));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadTesteurs()
        {
            Testeurs = this.dbService.FetchTesteursInfo();
        }
        public ManageMembersViewModel()
        {
            dbService = new DatabaseService();
            Testeurs = dbService.FetchTesteursInfo();
        }

        public void AjouterTesteur(string username, string nom, string prenom)
        {
            this.dbService.CreateTesteur(username, nom, prenom);
            this.LoadTesteurs();
        }

        public void SupprimerTesteur(string username)
        {
            this.dbService.SupprimerTesteur(username);
            this.LoadTesteurs();

        }
            public void SupprimerPlusieursTesteurs(string usernames)
            {
                this.dbService.SupprimerPlusieursTesteurs(usernames);
                this.LoadTesteurs();
            }
        }
}
