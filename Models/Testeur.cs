using System;
using System.ComponentModel;

namespace AGB_AQSI_ExcelTool.Models
{
    public class Testeur : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _name; // to be renamed username
        private string _nom;
        private string _prenom;
        private DateTime _dateCreation;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name // to be renamed username
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        // Property for Last Name (Nom)
        public string Nom
        {
            get => _nom;
            set
            {
                if (_nom != value)
                {
                    _nom = value;
                    OnPropertyChanged(nameof(Nom));
                }
            }
        }

        // Property for First Name (Prenom)
        public string Prenom
        {
            get => _prenom;
            set
            {
                if (_prenom != value)
                {
                    _prenom = value;
                    OnPropertyChanged(nameof(Prenom));
                }
            }
        }

        // Property for DateCreation
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

        public Testeur(int id, string name, string username, string nom, string prenom, DateTime dateCreation)
        {
            Id = id;
            Name = name;    // username
            Nom = nom;
            Prenom = prenom;
            DateCreation = dateCreation;
        }

        public Testeur(string nom, string prenom, DateTime dateCreation, string name)
        {
            Name = name;
            Nom = nom;
            Prenom = prenom;
            DateCreation = dateCreation;
        }

        public Testeur(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
