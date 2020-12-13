using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus:INotifyPropertyChanged
    {
        private int trip;

        public int Trip
        {
            get { return trip; }
            set {
                trip = value;
                OnPropertyChange(this, new PropertyChangedEventArgs("Trip"));
            }
        }

        private void OnPropertyChange(Bus bus, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(bus, args);
        }

        public String License { get; set; }
        public DateTime StartOfWork { get; set; }
        public int TotalKms { get; set; }
        public Status Status { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return
                $"License is: {License}, \n" +
                $"Start of Work: {StartOfWork.ToShortDateString()}, \n" +
                $"Total KM: {TotalKms}, \n" +
                $"Ready to drive: {Status} \n";
        }
        //public override string ToString()
        //{
        //    return this.ToStringProperty();
        //}
    }
}
