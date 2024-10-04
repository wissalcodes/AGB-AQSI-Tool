using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AGB_AQSI_ExcelTool.Models;
using AGB_AQSI_ExcelTool.Services;
using System.Collections.Generic;
using DevExpress.Xpf.Grid;
using System.Windows;

namespace AGB_AQSI_ExcelTool.ViewModels
{
    public class ResultatImportationViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public DatabaseService dbService;

        public ObservableCollection<string> _testeurs;
        public ObservableCollection<string> Testeurs
        {
            get => _testeurs;
            set
            {
                _testeurs = value;
                OnPropertyChanged(nameof(Testeurs));
            }
        }
        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }
        private bool _isInactive;
        public bool IsInactive
        {
            get => _isInactive;
            set
            {
                _isInactive = value;
                OnPropertyChanged(nameof(IsInactive));
            }
        }
        // demandes affectees
        private ObservableCollection<Demande> _demandesAf;
        public ObservableCollection<Demande> DemandesAf
        {
            get => _demandesAf;
            set
            {
                _demandesAf = value;
                OnPropertyChanged(nameof(DemandesAf));
            }
        }

        // demande pas encore affectees
        private ObservableCollection<Demande> _demandesNonAf;
        public ObservableCollection<Demande> DemandesNonAf
        {
            get => _demandesNonAf;
            set
            {
                _demandesNonAf = value;
                OnPropertyChanged(nameof(DemandesNonAf));
            }
        }


        public ResultatImportationViewModel(ObservableCollection<Demande> demandes)
        {
            // Initialize the collections
            DemandesAf = new ObservableCollection<Demande>();
            DemandesNonAf = new ObservableCollection<Demande>();
            dbService = new DatabaseService();
            IsActive = true;
            IsInactive = false;
            FetchTesteurs();
            // separer les demandes affectees et non-affectees
            foreach (Demande demande in demandes)
            {

                if (demande.IdTesteur != 0 )  // si un testeur est affecte a cette demande
                {
                    DemandesAf.Add(demande);
                }
                else
                {
                    DemandesNonAf.Add(demande);
                }
            }
        }

        public void FetchTesteurs()
        {
             ObservableCollection<string> testeurs = dbService.FetchTesteurs();
            Testeurs = new  ObservableCollection<string>(testeurs);
             OnPropertyChanged(nameof(Testeurs));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CreateAffectations()
        {
            foreach (var demande in this.DemandesNonAf)
            {
                if (!string.IsNullOrEmpty(demande.NomTesteur) && !demande.NomTesteur.Equals("Sélectionner"))
                {

                    this.dbService.AffecterDemande(demande.ID, demande.NomTesteur);
                }
            }
        }

    }
}
