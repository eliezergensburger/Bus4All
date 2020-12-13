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

        Button currentButton = null;
        ProgressBar progressBar = null;

        public MainWindow()
        {
            InitializeComponent();
            foreach (var item in bL.getAllBusses())
            {
                buses.Add(item);
            }
            this.lbBuses.ItemsSource = buses;

        }

        public static T FindAncestorOrSelf<T>(DependencyObject obj)
          where T : DependencyObject
        {
            while (obj != null)
            {
                T objTest = obj as T;

                if (objTest != null)
                    return objTest;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        private void btnRefuel_Click(object sender, RoutedEventArgs e)
        {
            currentButton = sender as Button;
            Bus bus = currentButton.DataContext as Bus;

            if (bL.canRefuel(bus))
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

                worker.WorkerReportsProgress = true;

                currentButton.IsEnabled = false;
                progressBar = (currentButton.Parent as Grid).Children[4] as ProgressBar;
  
                worker.RunWorkerAsync(bus);
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgw = sender as BackgroundWorker;
   
            Bus bus = e.Argument as Bus;
           int zman = bL.refuel(bus);
            for (int i = 1; i <= zman; i++)
            {
                Thread.Sleep(1000);
                bgw.ReportProgress(i*100/zman, bus);
            }

            e.Result =bus;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int percentage = e.ProgressPercentage;
            Bus bus = e.UserState as Bus;
            progressBar.Value = percentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bus bus = e.Result as Bus;
            bus.Status = BO.Status.READY;
            bL.updateBus(bus);

            currentButton.IsEnabled = true;
            progressBar.Value = 0;

            progressBar = null;
            currentButton = null;
         }
    }
}
