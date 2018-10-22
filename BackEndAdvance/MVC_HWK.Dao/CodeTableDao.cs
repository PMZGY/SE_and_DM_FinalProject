using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_HWK.Dao
{
    public class CodeTableDao : ICodeTableDao
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDbConnectionString()
        {
            return MVC_HWK.Common.ConfigTool.GetDbConnectionString();
        }

        public List<SelectListItem> GetStatusTable(string type)
        {
            DataTable dt = new DataTable();
            string sql = @"Select CODE_ID as CodeId,CODE_NAME As CodeName
                           From dbo.BOOK_CODE
                           Where CODE_TYPE=@Type";
            using (SqlConnection conn = new SqlConnection(this.GetDbConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Type", type));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt, "CodeId", "CodeName");
        }

        public List<SelectListItem> GetMemberTable()
        {
            DataTable dt = new DataTable();
            string sql = @"select USER_ID as UserId,USER_CNAME as UserCname,USER_ENAME as UserEname
                           From MEMBER_M";
            using (SqlConnection conn = new SqlConnection(this.GetDbConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["UserEname"].ToString() + "(" + row["UserCname"] + ")",
                    Value = row["UserId"].ToString()
                });
            }
            return result;
        }

        public List<SelectListItem> GetBookClassTable()
        {
            DataTable dt = new DataTable();
            string sql = @"select BOOK_CLASS_ID as BookClassId,BOOK_CLASS_NAME as BookClassName
                            From BOOK_CLASS";
            using (SqlConnection conn = new SqlConnection(this.GetDbConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }




            return this.MapCodeData(dt, "BookClassId", "BookClassName");
        }



        private List<SelectListItem> MapCodeData(DataTable dt, string id, string name)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row[name].ToString(),
                    Value = row[id].ToString()
                });
            }
            return result;
        }
    }
}
