using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BO;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bL = BL.FactoryBL.BlInstance;

        ObservableCollection<Bus> buses = new ObservableCollection<Bus>();

        public MainWindow()
        {
            InitializeComponent();
            List<Bus> busList = bL.getAllBusses();
            foreach (var item in busList)
            {
                buses.Add(item);
            }
            this.lbBuses.ItemsSource = buses;

        }

        private void btnRefuel_Click(object sender, RoutedEventArgs e)
        {
            Button currentButton = sender as Button;
            Bus bus = currentButton.DataContext as Bus;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;

            currentButton.IsEnabled = false;

            //ProgressBar progressBar = (currentButton.Parent as Grid).Children[4] as ProgressBar;
            //using Binding to Trip property of Bus (INotifyProertyChanged) instead -- see XAML

            List<object> args = new List<object> { bus, currentButton };
            worker.RunWorkerAsync(args);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgw = sender as BackgroundWorker;
            int zman = 0;

            List<object> args = e.Argument as List<Object>;

            Bus bus = args[0] as Bus;
            //Button btn = args[1] as Button;

            try
            {
                zman = bL.refuel(bus);
            }
            catch (BO.BlBusException busex)
            {
                e.Result = busex.Message;
                e.Cancel = true;
                return;
            }

            for (int i = 1; i <= zman; i++)
            {
                Thread.Sleep(1000);
                bgw.ReportProgress(i * 100 / zman, bus);
            }

            e.Result = args;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Bus bus = e.UserState as Bus;
            bus.Trip = e.ProgressPercentage;
            //  progressBar.Value = pere.ProgressPercentagecentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<Object> args = e.Result as List<Object>;

            Bus bus = args[0] as Bus;

            if (e.Cancelled == true)
            {
                MessageBox.Show("operation cancelled " + e.Result as String);
            }
            else
            {
                bus.Status = BO.Status.READY;
                bL.updateBus(bus);
            }
            Button btn = args[1] as Button;
            btn.IsEnabled = true;
            bus.Trip = 0;
        }

    }
}
