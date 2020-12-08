using DO;
using System.Collections.Generic;

namespace BL
{
    public interface IBL
    {
        bool insertBus(Bus bus);
        string TomarShalom();
        bool updateBus(Bus bus);
        List<Bus> getAllBusses();
    }
}