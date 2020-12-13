using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BL;
using BO;

namespace PresentationLayer
{
    class Program
    {
        static IBL bl = FactoryBL.BlInstance;
 
        static void Main(string[] args)
        {
            printAllbusses();

            Console.WriteLine("Press any key to return.. (wait to backround thread).");
            Console.ReadKey();

            printAllbusses();
            //Console.WriteLine(bl.TomarShalom());

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }

 
        private static void printAllbusses()
        {
            foreach (var item in bl.getAllBusses())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------");
        }
    }
}
