using System;
using System.ComponentModel;

namespace AGB_AQSI_ExcelTool.Models
{
    public class Affectation : INotifyPropertyChanged
    {
        private int _idDemande;
        public int IdDemande
        {
            get => _idDemande;
            set
            {
                if (_idDemande != value)
                {
                    _idDemande = value;
                    OnPropertyChanged(nameof(IdDemande));
                }
            }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private DateTime _dateCreation;
        public DateTime DateCreation
        {
            get => _dateCreation;
            set
            {
                if (_dateCreation != value)
                {
                    _dateCreation = value;
                    OnPropertyChanged(nameof(DateCreation));
                }
            }
        }

        // Constructor
        public Affectation(int idDemande, string username, DateTime dateCreation)
        {
            _idDemande = idDemande;
            _username = username;
            _dateCreation = dateCreation;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to trigger the PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
