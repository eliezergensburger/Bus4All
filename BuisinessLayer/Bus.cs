using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Bus
    {
        public String License { get; set; }
        public DateTime StartOfWork { get; set; }
        public int TotalKms { get; set; }

        public override string ToString()
        {
            return $"License is: {License}, Start of Work: {StartOfWork.ToShortDateString()}, Total KM: {TotalKms}";
        }
    }
}
