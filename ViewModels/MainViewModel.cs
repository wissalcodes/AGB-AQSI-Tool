using DevExpress.Mvvm;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
namespace AGB_AQSI_ExcelTool.ViewModels
{
    public class MainViewModel : ViewModelBase, IDisposable, INotifyPropertyChanged
    {
        private DateTime _currentDateTime;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public MainViewModel()
        {

            // initialize the current date/time
            _currentDateTime = DateTime.Now;

            // Start the background task to update the date and time
            _cancellationTokenSource = new CancellationTokenSource();
            StartClockUpdateTask(_cancellationTokenSource.Token);


            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (sender, e) =>
            {
                _currentDateTime = DateTime.Now;
                OnPropertyChanged(nameof(CurrentDay));
                OnPropertyChanged(nameof(CurrentTime));
                OnPropertyChanged(nameof(CurrentDate));
            };
            timer.Start();
        }

        public string CurrentDay => _currentDateTime.ToString("dddd", new CultureInfo("fr-FR")); 

        public string CurrentTime => _currentDateTime.ToString("hh:mm tt"); 

        public string CurrentDate => _currentDateTime.ToString("dd MMMM",new CultureInfo("fr-FR")); 

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Asynchronous task that continuously updates the current date and time
        private async void StartClockUpdateTask(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
               var currentTime = DateTime.Now;

                // Marshal updates to the UI thread (because only UI thread can update the IU)
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _currentDateTime = currentTime;
                    OnPropertyChanged(nameof(CurrentDay));
                    OnPropertyChanged(nameof(CurrentTime));
                    OnPropertyChanged(nameof(CurrentDate));

                });
                await Task.Delay(1000, cancellationToken);

                // Wait for 1 second before the next update
            }
        }

        public void StopClockUpdateTask()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void Dispose()
        {
            StopClockUpdateTask();
            _cancellationTokenSource.Dispose();
        }
    }
}
