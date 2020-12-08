using DO;
using System.Collections.Generic;

namespace DALAPI
{
    public interface IDal
    {
        string SayHello();
        void SetHello(string message);
        bool addBus(BusDAO bus);
        bool update(BusDAO bus);
        List<BusDAO> getBusses();
    }
}