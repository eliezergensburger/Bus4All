using BO;
using System.Collections.Generic;

namespace BL
{
    public interface IBL
    {
        bool insertBus(Bus bus);
        bool updateBus(Bus bus);
        List<Bus> getAllBusses();
        void refuel(Bus bus);
    }
}