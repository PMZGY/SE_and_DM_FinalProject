using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC_HWK.Dao
{
    public interface ICodeTableDao
    {
        List<SelectListItem> GetBookClassTable();
        List<SelectListItem> GetMemberTable();
        List<SelectListItem> GetStatusTable(string type);
    }
}