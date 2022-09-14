using System;
using System.ComponentModel;
using System.Windows;

using MKLWrapper;

namespace MKLBenchmarkApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewData viewData { get; set; }

        public MainWindow()
        {
            viewData = new();
            DataContext = viewData;
            InitializeComponent();
        }

        public void OnLoaded(object sender, RoutedEventArgs e)
        {
            functionComboBox.ItemsSource = Enum.GetValues(typeof(MKLWrapper.VMf));
        }

        public void OnClosing(object sender, CancelEventArgs e)
        {
            if (!ProceedWithBenchmarkReplacement())
            {
                e.Cancel = true;
            }
        }

        private void OnCreateNewBenchmark(object sender, RoutedEventArgs e)
        {
            if (ProceedWithBenchmarkReplacement())
            {
                viewData.Benchmark = new();
            }
        }

        private void OnSaveBenchmark(object sender, RoutedEventArgs e)
        {
            SaveBenchmarkData();
        }

        private void OnOpenBenchmark(object sender, RoutedEventArgs e)
        {
            if (ProceedWithBenchmarkReplacement())
            {
                LoadBenchmarkData();
            }
        }

        private void OnAddVMTime(object sender, RoutedEventArgs e)
        {
            // Dummy code, improve later
            VMGrid grid = new VMGrid(viewData.NewNodesNumber, viewData.NewLeftBorder, viewData.NewRightBorder);
            VMf funcType = VMf.Sin;
            viewData.AddVMTime(funcType, grid);
        }

        private void OnAddVMAccuracy(object sender, RoutedEventArgs e)
        {
            // Dummy code, improve later
            VMGrid grid = new VMGrid(viewData.NewNodesNumber, viewData.NewLeftBorder, viewData.NewRightBorder);
            VMf funcType = VMf.Cos;
            viewData.AddVMAccuracy(funcType, grid);
        }

        private bool SaveBenchmarkData()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.Filter = "VMBenchmark binary file (*vmbenchmark)|*.vmbenchmark|All (*.*)|*.*";
            dlg.FilterIndex = 0;
            dlg.OverwritePrompt = true;

            if (dlg.ShowDialog() == true)
            {
                if (viewData.Save(dlg.FileName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool LoadBenchmarkData()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Filter = "VMBenchmark binary file (*vmbenchmark)|*.vmbenchmark|All (*.*)|*.*";
            dlg.FilterIndex = 0;
            dlg.CheckFileExists = true;

            if (dlg.ShowDialog() == true)
            {
                return viewData.Load(dlg.FileName);
            }
            return false;
        }

        private bool ProceedWithBenchmarkReplacement()
        {
            if (viewData.ChangesNotSaved)
            {
                MessageBoxResult result = MessageBox.Show(
                    messageBoxText: "Do you want to save the changes in current benchmark data? Your changes will be lost if you don't save them.",
                    caption: "MKL Benchmark App",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning,
                    MessageBoxResult.Yes);

                if (result == MessageBoxResult.Cancel)
                {
                    return false;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    return SaveBenchmarkData();
                }
            }
            return true;
        }
    }
}
