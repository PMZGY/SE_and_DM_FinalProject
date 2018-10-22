using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_HWK.Service
{
    public class CodeTableService : ICodeTableService
    {
        private MVC_HWK.Dao.ICodeTableDao CodeTableDao { get; set; }
        public List<SelectListItem> GetStatusTable(string type)
        {
            return CodeTableDao.GetStatusTable(type);
        }

        public List<SelectListItem> GetMemberTable()
        {
            return CodeTableDao.GetMemberTable();
        }
        public List<SelectListItem> GetBookClassTable()
        {
            return CodeTableDao.GetBookClassTable();
        }
    }
}
