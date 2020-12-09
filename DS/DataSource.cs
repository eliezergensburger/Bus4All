using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    /// <summary>
    /// TO DO
    /// </summary>
    public static class DataSource
    {
        private  static string helloStr = "Hello World";
        private static List<BusDAO> busses = new List<BusDAO>();
        private static List<BusInTravelDAO> busestravel = new List<BusInTravelDAO>();

        public static List<BusDAO> Buses { get => busses; }
        public static List<BusInTravelDAO> BusesTravel { get => busestravel; }

        public static string Hello
        {
            get { return helloStr; }
            set { helloStr = value; }
        }

    }
}
