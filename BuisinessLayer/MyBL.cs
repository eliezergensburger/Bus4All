using DALAPI;
using DO;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace BL
{
    internal sealed class MyBL : IBL, IDummyBL
    {
        const  int REFUELLINGTIME = 12;
        const int FULLTANK = 1200;


        IDal dal = FactoryDal.getDal("simple");
      //  private BackgroundWorker metadlek;

        #region singleton parts
        private static IBL instance = new MyBL();
  
        public static IBL getInstance()
        {
            return instance;
        }
        static MyBL() { }
        private MyBL() { }

        #endregion end of singleton parts

        private BusDAO convertDAO(Bus bus)
        {
            BusDAO busDAO= new BusDAO
            {
                License = Int32.Parse(bus.License),
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms,
                Fuel = 0,
                //Status = (bus.Status == true) ? Status.READY : Status.REFUELLING
                Status = (DO.Status)bus.Status
            };
            return busDAO;
        }

        private Bus convertoBO(BusDAO bus)
        {
            return new Bus
            {
                License = bus.License.ToString(),
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms,
                Status = (BO.Status)bus.Status
            };
        }

        //public methods

        List<Bus> IBL.getAllBusses()
        {
            List<Bus> result = new List<Bus>();
            foreach (var item in dal.getBusses())
            {
                result.Add(convertoBO(item));
            }
            return result;
        }

        //implementation of DummyBL
        string IDummyBL.ImriShalom()
        {
            //      return dal.SayHello();
            //     dal.SayHello().Replace('l', 'L');
            string refstr = dal.SayHello();
            refstr.Replace('l', 'L');
            return refstr;

        }

        bool IBL.insertBus(Bus bus)
        {
            List<BusDAO> dalbuses = dal.getBusses();
            BusDAO busDAO = convertDAO(bus);

            if(dalbuses.Exists(mandehu => mandehu.License== busDAO.License))
            {
                throw new BO.BlBusException("kayam bamaarechet");
            }
            bool res = dal.addBus(convertDAO(bus));
            return res;
        }

        public string TomarShalom()
        {
            return dal.SayHello();
        }

        bool IBL.updateBus(Bus bus)
        {
            return dal.update(convertDAO(bus));
        }

        int IBL.refuel(Bus bus)
        {
            List<BusDAO> buses = dal.getBusses();
             BusDAO busDAO = convertDAO(bus);
  
            if (!buses.Any(item => item.License == busDAO.License))
            {
                throw new ArgumentNullException("bus");
            }
            if (busDAO.Status != DO.Status.READY)
            {
                throw new InvalidOperationException("bus not ready");
            }

            //metadlek = new BackgroundWorker();
            //metadlek.DoWork += BackgroundWorker_DoWork;
            //metadlek.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            //metadlek.ProgressChanged += BackgroundWorker_ProgressChanged;

            //metadlek.WorkerReportsProgress = true;

            //metadlek.RunWorkerAsync(busDAO);
            if(busDAO.Fuel == FULLTANK)
            {
                throw new BO.BlBusException("tank full allready");
            }
            busDAO.Status = DO.Status.REFUELLING;
            dal.update(busDAO);

            return REFUELLINGTIME;

            //Thread gamad = new Thread(refuelling);
            //gamad.Start(busDAO);

            // send Thread to refuel the bus
        }

        //private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    BusDAO bus = e.Argument as BusDAO;

        //    bus.Status = DO.Status.REFUELLING;
        //    dal.update(bus);

        //    Thread.Sleep(4000);
        //    metadlek.ReportProgress(30, bus);
        //    Thread.Sleep(4000);
        //    metadlek.ReportProgress(60, bus);
        //    Thread.Sleep(4000);
        //    metadlek.ReportProgress(100, bus);

        //    e.Result = bus;
        //}

        //private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    int percentage = e.ProgressPercentage;
        //    Bus bus = e.UserState as Bus;

        //    // UPDATE UI
        //}

        //private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    BusDAO busDAO = e.Result as BusDAO;

        //    busDAO.Status = DO.Status.READY;
        //    dal.update(busDAO);
        //}

        bool IBL.canRefuel(Bus bus)
        {
            if(bus.Status == BO.Status.DRIVING)
            {
                throw new BO.BusNotReadyException("driving");
            }
            if (bus.Status == BO.Status.REFUELLING)
            {
                throw new BO.BusNotReadyException("refuelling already");
            }
            return true;
        }


        //private void refuelling(object obj)
        //{
        //    BusDAO busDAO = obj as BusDAO;

        //    busDAO.Status = Status.REFUELLING;
        //    dal.update(busDAO);

        //    Thread.Sleep(4000);
        //    //update progess in main thread how ?
        //    Thread.Sleep(4000);
        //    //update progess in main thread how ?
        //    Thread.Sleep(4000);
        //    //update progess in main thread how ?

        //    busDAO.Fuel = 1200;

        //    busDAO.Status = Status.READY;
        //    dal.update(busDAO);
        //}
    }
}
