using DALAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    internal sealed class MyBL : IBL,DummyBL
    {
       IDal dal = Factory.FactoryDal.getDal("simple");

        private static IBL instance = new MyBL();
        public static IBL getInstance()
        {
            return instance;
        }
        static MyBL() {}
        private MyBL() {}
        private BusDAO convertDAO(Bus bus)
        {
            return new BusDAO
            {
                License = Int32.Parse(bus.License),
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms
            };
        }

        private Bus convertoBO(BusDAO bus)
        {
            return new Bus
            {
                License = bus.License.ToString(),
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms
            };
        }

        List<Bus> IBL.getAllBusses()
        {
            List<Bus> result = new List<Bus>();
            foreach (var item in dal.getBusses())
            {
                result.Add(convertoBO(item));
            }
            return result;
        }

        string DummyBL.ImriShalom()
        {
            //      return dal.SayHello();
            //     dal.SayHello().Replace('l', 'L');
            string refstr = dal.SayHello();
            refstr.Replace('l', 'L');
            return refstr;

        }

        bool IBL.insertBus(Bus bus)
        {
            bool res = dal.addBus(convertDAO(bus));
            return res;
        }

        string IBL.TomarShalom()
        {
             return dal.SayHello();
        }

        bool IBL.updateBus(Bus bus)
        {
            return dal.update(convertDAO(bus));
        }
    }
}
