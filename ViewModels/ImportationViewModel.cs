using System.Collections.ObjectModel;
using System.ComponentModel;
using AGB_AQSI_ExcelTool.Models;
using AGB_AQSI_ExcelTool.Services;
using System.Linq;

namespace AGB_AQSI_ExcelTool.ViewModels
{
    public class ImportationViewModel : INotifyPropertyChanged
    {
        public DatabaseService dbService;

        private ObservableCollection<State> _states;
        public ObservableCollection<State> States
        {
            get => _states;
            set
            {
                if (_states != value)
                {
                    if (_states != null)
                    {
                        UnsubscribeFromStatePropertyChanges();
                    }

                    _states = value;
                    OnPropertyChanged(nameof(States));
                    SubscribeToStatePropertyChanges();
                    UpdateIsStateSelected(); 
                }
            }
        }

        private string _path;
        public string path
        {
            get { return _path; }
            set
            {
                if (_path != value)
                {

                    _path = value;
                    OnPropertyChanged(nameof(path));
                    UpdateIsPathSelected();
                }
            }
        }

        private bool _isPathSelected;
        public bool IsPathSelected
        {
            get => _isPathSelected;
            set
            {
                _isPathSelected = value;
                OnPropertyChanged(nameof(IsPathSelected));
            }
        }

        private bool _isStateSelected;
        public bool IsStateSelected
        {
            get => _isStateSelected;
            private set 
            {
                if (_isStateSelected != value)
                {
                    _isStateSelected = value;
                    OnPropertyChanged(nameof(IsStateSelected));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ImportationViewModel()
        {
            // initialize the database service
            dbService = new DatabaseService();

            // populate the states array
            States = new ObservableCollection<State>
            {
                new State("Archivage des PS sur préprod"),
                new State("CR en production sans validation AQSI"),
                new State("Demande annulée"),
                new State("Demande initiée"),
                new State("Demande mise en production"),
                new State("En attente d'annulation"),
                new State("En attente de complément d'information"),
                new State("En attente de planification"),
                new State("En attente de planification DEV"),
                new State("En attente de planification test tech/UAT"),
                new State("En attente de validation AQSI"),
                new State("En attente de validation CP"),
                new State("En attente de validation des PS"),
                new State("En attente déploiement package test"),
                new State("En attente planification MEP"),
                new State("En attente planification UAT"),
                new State("En attente validation CAB"),
                new State("En attente validation UAT"),
                new State("En cours de cadrage"),
                new State("En cours de correction"),
                new State("En cours de planification cadrage"),
                new State("En cours de réalisation"),
                new State("Pending : Attente retour prestataire"),
                new State("Planification cadrage"),
                new State("Planification des correctifs"),
                new State("Préparation MEP")
            };

            path = "Aucun chemin selectionné";
            UpdateIsPathSelected();
        }

        private void UnsubscribeFromStatePropertyChanges()
        {
            foreach (var state in States)
            {
                state.PropertyChanged -= OnStatePropertyChanged;
            }
        }
        // Method to update IsPathSelected based on the current path
        private void UpdateIsPathSelected()
        {
            IsPathSelected = !string.IsNullOrEmpty(_path) && _path != "Aucun chemin selectionné";
        }

        // Method to update IsStateSelected based on whether any state is checked
        private void UpdateIsStateSelected()
        {
            IsStateSelected = States.Any(state => state.IsChecked);
        }

        // Event handler for individual state property changes
        private void OnStatePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(State.IsChecked))
            {
                UpdateIsStateSelected();  
            }
        }
        private void SubscribeToStatePropertyChanges()
        {
            foreach (var state in States)
            {
                state.PropertyChanged += OnStatePropertyChanged;
            }
        }
    }
}
