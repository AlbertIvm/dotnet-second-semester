using System.Windows;

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
            DataContext = viewData;
            InitializeComponent();
        }
    }
}
