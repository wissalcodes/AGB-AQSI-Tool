using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGB_AQSI_ExcelTool.Models
{
    public class State : INotifyPropertyChanged
    {

        private bool _isChecked;
        public string Title { get; set; }
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }

        }

        public State() {
                IsChecked = false;
                Title = "";       
          
        }

        public State(string title)
        {
            IsChecked = false;
            Title = title;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
