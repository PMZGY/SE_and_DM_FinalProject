using MVC_HWK.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_HWK.Service
{
    public class BookFactory
    {
        public IBookDao GetBookDao()
        {
            IBookDao result;

            switch (Common.ConfigTool.GetAppsetting("DaoInTest"))
            {
<<<<<<< HEAD
               // case "PONPON"
               // case "Y":
=======
               // test embedded
>>>>>>> c846764775ba572e03931029ffab48ea7c7796e6
                case "Y":
                    result = new MVC_HWK.Dao.BookTestDao();
                    break;
                case "N":
                    result = new MVC_HWK.Dao.BookDao();
                    break;
                default:
                    result = new MVC_HWK.Dao.BookTestDao();
                    break;
            }
            return result;
        }


    }
}
