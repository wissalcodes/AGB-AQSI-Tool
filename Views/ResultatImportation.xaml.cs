using AGB_AQSI_ExcelTool.Models;
using AGB_AQSI_ExcelTool.ViewModels;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Dialogs;
using System;
using System.Linq;
using System.Windows;
using System.IO;

namespace AGB_AQSI_ExcelTool.Views
{
    public partial class ResultatImportation : ThemedWindow
    {
        public ResultatImportation(ResultatImportationViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
        public ResultatImportation()
        {
            InitializeComponent();
        }

        private void AffectDemandes(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ResultatImportationViewModel viewModel)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir procéder à l'affectation ?",
                    "Confirmer l'Affectation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {
                    viewModel.CreateAffectations();
                    MessageBox.Show("Demandes affectées");
                    viewModel.IsActive = false;
                    viewModel.IsInactive = true;
                }
                else
                {
                    MessageBox.Show("L'affectation a été annulée.");
                }
            }

        }

        private void Exporter(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ResultatImportationViewModel viewModel)
            {
                try
                {
                    // Open the folder browser dialog to select export path
                    DXFolderBrowserDialog fileDialog = new DXFolderBrowserDialog();
                    fileDialog.Description = "Sélectionnez le dossier que vous souhaitez ouvrir :";
                    fileDialog.ShowNewFolderButton = false;
                    fileDialog.ShowDialog();

                    if (string.IsNullOrWhiteSpace(fileDialog.SelectedPath))
                    {
                        MessageBox.Show("Aucun dossier sélectionné.");
                        return;
                    }

                    // Access the GridControl directly by its name
                    var gridControl = this.DemandesNonAfGrid;

                    if (gridControl == null || gridControl.View == null)
                    {
                        MessageBox.Show("Erreur: le GridControl ou sa vue est introuvable.");
                        return;
                    }

                    // Filter the data source to include only rows where NomTesteur != "Sélectionner"
                    var filteredRows = viewModel.DemandesNonAf
                    .Where(demande => demande.NomTesteur != "Sélectionner")            
                    .ToList();

                    if (!filteredRows.Any())
                    {
                        MessageBox.Show("Aucune donnée à exporter.");
                    }

                    // Temporarily set the filtered data source to the grid for export
                    gridControl.ItemsSource = filteredRows;

                    // Perform the export to XLSX
                    string filePath = Path.Combine(fileDialog.SelectedPath, "Affectation.xlsx");
                    gridControl.View.ExportToXlsx(filePath);

                    MessageBox.Show($"Les demandes affectées ont été exportées à {filePath}");

                    gridControl.ItemsSource = viewModel.DemandesNonAf;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'exportation: {ex.Message}");
                }
            }
        }
    }
}
