using AGB_AQSI_ExcelTool.Models;
using AGB_AQSI_ExcelTool.Services;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System;
namespace AGB_AQSI_ExcelTool.ViewModels
{
    public class HistoryViewModel : ViewModelBase
    {

        public ObservableCollection<Affectation> Affectations { get; set; }

        private readonly DatabaseService dbService;
        public HistoryViewModel() {   
         dbService = new DatabaseService();
            // Fetch all affectations from the database
          Affectations   = dbService.FetchAffectations();
        }
    }
}
