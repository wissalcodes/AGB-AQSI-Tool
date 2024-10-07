using AGB_AQSI_ExcelTool.ViewModels;
using System.Windows;
using DevExpress.Xpf.Dialogs;
using DevExpress.DataAccess.Excel;                             
using FieldInfo = DevExpress.DataAccess.Excel.FieldInfo;       
using AGB_AQSI_ExcelTool.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using ExcelDataReader;
using System.Linq;
using System;
using DevExpress.Xpf.WindowsUI;
namespace AGB_AQSI_ExcelTool.Views
{

    public partial class Importation : NavigationPage
    {
        private ImportationViewModel VM;


        public Importation()
        {
            InitializeComponent();
             VM = new ImportationViewModel();
            this.DataContext = VM;
        }
        private void ImportFile(object sender, RoutedEventArgs e)     
        {
            // to stock data extracted from the excel file
            ObservableCollection<Demande> demandes = new ObservableCollection<Demande>();
            var source = new ExcelDataSource
            {
                FileName = VM.path
            };

            var worksheetSettings = new ExcelWorksheetSettings("Feuille 1");
            var options = new ExcelSourceOptions(worksheetSettings)
            {
                SkipEmptyRows = true,
                UseFirstRowAsHeader = true
            };
            source.SourceOptions = options;
            var columnMappings = new DevExpress.DataAccess.Excel.FieldInfo[]
            {
                new FieldInfo { Name = "ID", Type = typeof(int) },
                new FieldInfo { Name = "Titre", Type = typeof(string) },
                new FieldInfo { Name = "Initiateur", Type = typeof(string) },
                new FieldInfo { Name = "Responsable realisation", Type = typeof(string) },
                new FieldInfo { Name = "Etat", Type = typeof(string) },
                new FieldInfo { Name = "Type demande", Type = typeof(string) },
            };
            source.Schema.AddRange(columnMappings);
            source.Fill();
            // create a data table from the excel data
            DataTable dt = new DataTable();
            dt = ExcelToDataTableUsingExcelDataReader(VM.path);
            // cast DataTable to ObservableCollection
            ObservableCollection<Demande> filteredDemandes = new ObservableCollection<Demande>();
            // Filter based on selected states
            foreach (DataRow row in dt.Rows)
            {
                var etatValue = (string)row["Etat"];
                // Check if Etat matches any selected state with isChecked true
                 int idDemande = Convert.ToInt32(row["ID"]);
                Testeur testeur = VM.dbService.GetTesteurNomByDemandeId(idDemande);

                if (VM.States.Any(state => state.IsChecked && state.Title == etatValue))
                {
                    filteredDemandes.Add(new Demande(
                        idDemande,
                        (string)row["Titre"],
                        (string)row["Initiateur"],
                        (string)row["Responsable realisation"],
                        (string)row["Etat"],
                        (string)row["Type demande"],
                        testeur != null ? testeur.Name : "Sélectionner", 
                        testeur != null ? testeur.Id : 0 
                    ));
                }
              
            }
            // navigate to ResultatImportation page
            ResultatImportationViewModel vm = new ResultatImportationViewModel(filteredDemandes);
            ResultatImportation resultsWindow = new ResultatImportation(vm);
            resultsWindow.Show(); 
        }

        // method to display file path dialog
        public void SelectFile(object sender, RoutedEventArgs e)
        {
            DXOpenFileDialog fileDialog = new DXOpenFileDialog
            {
                Filter = "Excel Files (*.xlsx, *.xls)|*.xlsx;*.xls|All Files (*.*)|*.*",
                Title = "Sélectionnez le fichier Excel à importer"
            };
            fileDialog.ShowDialog();
            if (string.IsNullOrEmpty(fileDialog.FileName) && string.IsNullOrEmpty(VM.path))
            {
                MessageBox.Show("Veuillez choisir un fichier avant de procéder.");
            }
            else
            {
                VM.path = fileDialog.FileName;
            }
        }

        // method that reads the content of the excel file into a data table
        public DataTable ExcelToDataTableUsingExcelDataReader(string storePath)
        {
            FileStream stream = File.Open(storePath, FileMode.Open, FileAccess.Read);

            string fileExtension = Path.GetExtension(storePath);
            IExcelDataReader excelReader = null;
            if (fileExtension == ".xls")
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (fileExtension == ".xlsx")
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            // configure the reader to treat the first row as a header and skip it
            var config = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            // Read the data into a DataSet
            var dataSet = excelReader.AsDataSet(config);
            excelReader.Close();
            // get the first table from the DataSet (first sheet)
            return dataSet.Tables[0];
        }

        private void BackToMainView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.navigationFrame.Navigate(new Uri("Views/MainView.xaml", UriKind.Relative));
            }
        }
    }
}
