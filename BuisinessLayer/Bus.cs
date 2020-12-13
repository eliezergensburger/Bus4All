using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus
    {
        public String License { get; set; }
        public DateTime StartOfWork { get; set; }
        public int TotalKms { get; set; }
        public Status Status { get; set; }
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
