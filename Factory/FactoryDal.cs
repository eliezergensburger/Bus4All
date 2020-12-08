using DAL;
using DALAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public static class FactoryDal
    {
        public static IDal getDal(string modelname)
        {
            switch (modelname)
            {
                case "simple":
                    return DalLists.Instance;
                case "persist":
                    return  DalXML.Instance;
                case "pashut":
                    return  DalObject.Instance;
                default:
                    return null;
            }
        }
    }
}
