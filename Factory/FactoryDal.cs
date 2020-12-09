using DAL;
using DALAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALAPI
{
    public static class FactoryDal
    {
        public static IDal getDal(string modelname)
        {
            switch (modelname)
            {
                case "nosingleton":
                    //no singleton
                    return  new DalLists();
                case "persist":
                    return  DalXML.Instance;
                case "simple":
                    return  DalObject.Instance;
                 default:
                    return null;
            }
        }
    }
}
