using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public static class DataSource
    {
        private  static string helloStr = "Hello World";
        private static List<BusDAO> busses = new List<BusDAO>();
        public static List<BusDAO> Buses { get => busses; }

        public static string Hello
        {
            get { return helloStr; }
            set { helloStr = value; }
        }

    }
}
