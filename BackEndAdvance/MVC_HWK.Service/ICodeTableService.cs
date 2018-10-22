using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC_HWK.Service
{
    public interface ICodeTableService
    {
        List<SelectListItem> GetBookClassTable();
        List<SelectListItem> GetMemberTable();
        List<SelectListItem> GetStatusTable(string type);
    }
}