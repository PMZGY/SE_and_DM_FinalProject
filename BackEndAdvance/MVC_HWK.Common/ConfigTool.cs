using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_HWK.Common
{
    public static class ConfigTool
    {
        /// <summary>
        /// 取則DB連線字串
        /// </summary>
        /// <returns></returns>
        public static string GetDbConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.
                ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        public static string GetAppsetting(string Key)
        {
            string AppSetting = string.Empty;
            AppSetting = System.Configuration.ConfigurationManager.AppSettings[Key];
            return AppSetting;
        }
    }
}
