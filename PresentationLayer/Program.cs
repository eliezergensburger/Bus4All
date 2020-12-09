﻿using System;
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
            Bus bus = new Bus
            {
                License = "1234567",
                StartOfWork = DateTime.Today.AddYears(-2),
                TotalKms = 5000,
            };
            bl.insertBus(bus);
            bl.refuel(bus);
            printAllbusses();
           Thread.Sleep(20000);
 
            bl.insertBus(new Bus
            {
                License = "33333",
                StartOfWork = DateTime.Today.AddYears(-20),
                TotalKms = 999989
            });

            bl.insertBus(new Bus
            {
                License = "55555",
                StartOfWork = DateTime.Today.AddMonths(-2),
                TotalKms = 50
            });


            bl.insertBus(new Bus
            {
                License = "1234567",
                StartOfWork = DateTime.Today,
                TotalKms = 100
            });
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
