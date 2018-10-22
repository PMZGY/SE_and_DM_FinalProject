using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_HWK.Dao
{
    public class BookDao : IBookDao
    {
        /// <summary>
        /// 取則DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDbConnectionString()
        {
            return MVC_HWK.Common.ConfigTool.GetDbConnectionString();
        }

        /// <summary>
        /// 依照條件取得書籍資料
        /// </summary>
        /// <param name="bookSearchArg"></param>
        /// <returns></returns>
        public List<MVC_HWK.Model.BookData> GetBookDataByCondition(MVC_HWK.Model.BookSearchArg bookSearchArg)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT   dbo.BOOK_DATA.BOOK_STATUS, dbo.BOOK_DATA.BOOK_ID, dbo.BOOK_DATA.BOOK_NAME, 
                            dbo.BOOK_DATA.BOOK_CLASS_ID, dbo.BOOK_CLASS.BOOK_CLASS_NAME, dbo.BOOK_DATA.BOOK_AUTHOR, 
                            dbo.BOOK_DATA.BOOK_BOUGHT_DATE,dbo.BOOK_DATA.BOOK_PUBLISHER,dbo.BOOK_DATA.BOOK_NOTE, 
                            dbo.BOOK_DATA.BOOK_KEEPER,
							dbo.BOOK_DATA.CREATE_DATE,dbo.BOOK_DATA.CREATE_USER,
							dbo.BOOK_DATA.MODIFY_DATE,dbo.BOOK_DATA.MODIFY_USER,
							dbo.BOOK_CODE.CODE_TYPE, dbo.BOOK_CODE.CODE_TYPE_DESC, dbo.BOOK_CODE.CODE_NAME,
                            (dbo.MEMBER_M.USER_ENAME + '(' + dbo.MEMBER_M.USER_CNAME + ')')AS USER_CNAME
                            FROM              dbo.BOOK_DATA LEFT JOIN
                            dbo.BOOK_CLASS ON dbo.BOOK_CLASS.BOOK_CLASS_ID = dbo.BOOK_DATA.BOOK_CLASS_ID LEFT JOIN
                            dbo.BOOK_CODE ON dbo.BOOK_DATA.BOOK_STATUS = dbo.BOOK_CODE.CODE_ID LEFT JOIN
                            dbo.MEMBER_M ON dbo.BOOK_DATA.BOOK_KEEPER = dbo.MEMBER_M.USER_ID
							WHERE (( UPPER(dbo.BOOK_DATA.BOOK_NAME) LIKE UPPER('%'+@BookName+'%')) OR @BookName='') AND 
									(dbo.BOOK_DATA.BOOK_CLASS_ID=@BookClassId or @BookClassId='') AND
									(dbo.BOOK_DATA.BOOK_STATUS=@BookStatusId or @BookStatusId='')AND
									(dbo.BOOK_DATA.BOOK_KEEPER=@NameId or @NameId='')
                            ORDER BY BOOK_DATA.BOOK_BOUGHT_DATE DESC
                            ";
            using (SqlConnection conn = new SqlConnection(this.GetDbConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", bookSearchArg.Book_Name == null ? string.Empty : bookSearchArg.Book_Name));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", bookSearchArg.Book_Class_Id == null ? string.Empty : bookSearchArg.Book_Class_Id));
                cmd.Parameters.Add(new SqlParameter("@BookStatusId", bookSearchArg.Book_Status_Id == null ? string.Empty : bookSearchArg.Book_Status_Id));
                cmd.Parameters.Add(new SqlParameter("@NameId", bookSearchArg.Book_Keeper == null ? string.Empty : bookSearchArg.Book_Keeper));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return this.MapBookDataToList(dt);
        }

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="BookData"></param>
        /// <returns></returns>
        public int InsertBook(MVC_HWK.Model.BookData BookData)
        {
            string sql = @"INSERT INTO dbo.BOOK_DATA
						 (
							 BOOK_NAME, BOOK_CLASS_ID, BOOK_AUTHOR, BOOK_BOUGHT_DATE, BOOK_PUBLISHER, 
                             BOOK_NOTE, BOOK_STATUS,BOOK_KEEPER,CREATE_DATE,CREATE_USER,MODIFY_DATE,MODIFY_USER
						 )
						VALUES
						(
							 @BookName, @BookClassId, @BookAuthor, @BookBoughtDate, @Publisher, 
                             @BookNote, @BookStatus,@BookKeeper,@CreateDate,@CreateUser,@ModifyDate,@ModifyUser
						)
						Select SCOPE_IDENTITY()";
            int BookId;
            using (SqlConnection conn = new SqlConnection(this.GetDbConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", BookData.Book_Name));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", BookData.Book_Class_Id));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", BookData.Book_Author));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", BookData.Book_Bought_Date));
                cmd.Parameters.Add(new SqlParameter("@Publisher", BookData.Book_Publisher));
                cmd.Parameters.Add(new SqlParameter("@BookNote", BookData.Book_Note));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", 'A'));//
                cmd.Parameters.Add(new SqlParameter("@BookKeeper", (Object)DBNull.Value));//
                cmd.Parameters.Add(new SqlParameter("@CreateDate", System.DateTime.Now));//
                cmd.Parameters.Add(new SqlParameter("@CreateUser", "Miko"));//
                cmd.Parameters.Add(new SqlParameter("@ModifyDate", System.DateTime.Now));//
                cmd.Parameters.Add(new SqlParameter("@ModifyUser", "Miko"));//

                BookId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return BookId;
        }

        public int UpdateBook(MVC_HWK.Model.BookData BookData)
        {
            string sql = @"UPDATE dbo.BOOK_DATA
            SET BOOK_NAME=@BookName,BOOK_AUTHOR=@BookAuthor,BOOK_PUBLISHER=@Publisher,BOOK_NOTE=@BookNote,
            BOOK_BOUGHT_DATE=@BookBoughtDate,BOOK_CLASS_ID=@BookClassId,BOOK_STATUS=@BookStatus,BOOK_KEEPER=@BookKeeper,
            MODIFY_DATE=@ModifyDate,MODIFY_USER=@ModifyUser
            WHERE BOOK_ID=@BookId
            ";
            using (SqlConnection conn = new SqlConnection(this.GetDbConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", BookData.Book_Name));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", BookData.Book_Author));
                cmd.Parameters.Add(new SqlParameter("@Publisher", BookData.Book_Publisher));
                cmd.Parameters.Add(new SqlParameter("@BookNote", BookData.Book_Note));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", BookData.Book_Bought_Date));
                cmd.Parameters.Add(new SqlParameter("@BookClassId", BookData.Book_Class_Id));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", BookData.Book_Status_Id));
                cmd.Parameters.Add(new SqlParameter("@BookKeeper", BookData.Book_Keeper == null ? string.Empty : BookData.Book_Keeper));//
                cmd.Parameters.Add(new SqlParameter("@ModifyDate", System.DateTime.Now));//
                cmd.Parameters.Add(new SqlParameter("@ModifyUser", "Miko"));//
                cmd.Parameters.Add(new SqlParameter("@BookId", BookData.Book_Id));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return BookData.Book_Id;
        }



        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="BookId"></param>
        public void DeleteBookById(string BookId)
        {
            try
            {
                string sql = "delete From BOOK_DATA Where BOOK_ID=@Book_Id";
                using (SqlConnection conn = new SqlConnection(this.GetDbConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@Book_Id", BookId));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        /// <summary>
        /// 用ID收尋書籍
        /// </summary>
        /// <param name="BookId"></param>
        public List<MVC_HWK.Model.BookData> SearchBookById(string BookId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT   dbo.BOOK_DATA.BOOK_STATUS, dbo.BOOK_DATA.BOOK_ID, dbo.BOOK_DATA.BOOK_NAME, 
                            dbo.BOOK_DATA.BOOK_CLASS_ID, dbo.BOOK_CLASS.BOOK_CLASS_NAME, dbo.BOOK_DATA.BOOK_AUTHOR, 
                            dbo.BOOK_DATA.BOOK_BOUGHT_DATE,dbo.BOOK_DATA.BOOK_PUBLISHER,dbo.BOOK_DATA.BOOK_NOTE, dbo.BOOK_DATA.BOOK_KEEPER,
                            (dbo.MEMBER_M.USER_ENAME + '(' + dbo.MEMBER_M.USER_CNAME + ')')AS USER_CNAME,
							dbo.BOOK_DATA.CREATE_DATE,dbo.BOOK_DATA.CREATE_USER,
							dbo.BOOK_DATA.MODIFY_DATE,dbo.BOOK_DATA.MODIFY_USER,
							dbo.BOOK_CODE.CODE_TYPE, dbo.BOOK_CODE.CODE_TYPE_DESC, dbo.BOOK_CODE.CODE_NAME
                            FROM              dbo.BOOK_DATA LEFT JOIN
                            dbo.BOOK_CLASS ON dbo.BOOK_CLASS.BOOK_CLASS_ID = dbo.BOOK_DATA.BOOK_CLASS_ID LEFT JOIN
                            dbo.BOOK_CODE ON dbo.BOOK_DATA.BOOK_STATUS = dbo.BOOK_CODE.CODE_ID LEFT JOIN
                            dbo.MEMBER_M ON dbo.BOOK_DATA.BOOK_KEEPER = dbo.MEMBER_M.USER_ID
                            Where BOOK_ID=@Book_Id";
            using (SqlConnection conn = new SqlConnection(this.GetDbConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Book_Id", BookId));
                cmd.ExecuteNonQuery();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToList(dt);
        }



        /// <summary>
        /// 把BookData的資料放到List裡面
        /// </summary>
        /// <param name="BookData"></param>
        /// <returns></returns>

        private List<MVC_HWK.Model.BookData> MapBookDataToList(DataTable BookData)
        {
            List<MVC_HWK.Model.BookData> BookResult = new List<MVC_HWK.Model.BookData>();
            foreach (DataRow row in BookData.Rows)
            {
                BookResult.Add(new MVC_HWK.Model.BookData()
                {
                    Book_Id = (int)row["Book_Id"],
                    Book_Name = row["Book_Name"].ToString(),
                    Book_Class_Id = row["Book_Class_Id"].ToString(),
                    Book_Class_Name = row["Book_Class_Name"].ToString(),
                    Book_Author = row["Book_Author"].ToString(),
                    Book_Bought_Date = Convert.ToDateTime(row["Book_Bought_Date"]),
                    Book_Publisher = row["Book_Publisher"].ToString(),
                    Book_Note = row["Book_Note"].ToString(),
                    Book_Status_Id = row["Book_Status"].ToString(),
                    Book_Status_Name = row["Code_Name"].ToString(),
                    Book_Keeper = row["Book_Keeper"].ToString(),
                    Book_Keeper_Cname = row["User_Cname"].ToString(),
                    Create_Date = Convert.ToDateTime(row["Create_Date"]),
                    Cteate_User = row["Create_User"].ToString(),
                    Modify_Date = Convert.ToDateTime(row["Modify_Date"]),
                    Modify_User = row["Modify_User"].ToString()
                });
            }
            return BookResult;
        }
    }
}
