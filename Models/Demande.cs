using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGB_AQSI_ExcelTool.Models
{
    public class Demande : INotifyPropertyChanged
    {
        public int ID { get; set; }

        private string _titre;
        public string Titre
        {
            get => _titre;
            set
            {
                if (_titre != value)
                {
                    _titre = value;
                    OnPropertyChanged(nameof(Titre));
                }
            }
        }
        public string Init { get; set; }
        public string RespReal { get; set; }
        public string Etat { get; set; }
        public string Type { get; set; }

        private string _nomTesteur;
        public string NomTesteur
        {
            get => _nomTesteur;
            set
            {
                if (_nomTesteur != value)
                {
                    _nomTesteur = value;
                    OnPropertyChanged(nameof(NomTesteur));
                }
            }
        }
        public int IdTesteur { get; set; }

        public Demande(int col0 , string col1, string col2, string col3, string col4, string col5 , string nom, int testeurId)
        {
            ID = col0;
            Titre = col1;
            Init = col2;
            RespReal = col3;
            Etat = col4;
            Type = col5;
            NomTesteur = nom;
            IdTesteur = testeurId;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
