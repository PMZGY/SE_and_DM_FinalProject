using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_HWK.Models
{
    public class BookData
    {
        /// <summary>
        /// 書籍PK流水號
        /// </summary>
        public int Book_Id { get; set; }

        /// <summary>
        /// 書籍名稱
        /// </summary>
        [Required(ErrorMessage = "此欄位必填")]
        [DisplayName("書名")]
        public string Book_Name { get; set; }

        /// <summary>
        /// 書籍類別
        /// </summary>
        [Required(ErrorMessage = "此欄位必填")]
        [DisplayName("圖書類別")]
        public string Book_Class_Id { get; set; }

        [DisplayName("圖書類別名稱")]
        public string Book_Class_Name{ get; set; }
        /// <summary>
        /// 書籍作者
        /// </summary>
        [Required(ErrorMessage = "此欄位必填")]
        [DisplayName("作者")]
        public string Book_Author { get; set; }

        /// <summary>
        /// 書籍購買日期
        /// </summary>
        [Required(ErrorMessage = "此欄位必填")]
        [DisplayName("購書日期")]
        public DateTime Book_Bought_Date { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [Required(ErrorMessage = "此欄位必填")]
        [DisplayName("出版商")]
        public string Book_Publisher { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        [Required(ErrorMessage = "此欄位必填")]
        [DisplayName("內容簡介")]
        public string Book_Note { get; set; }

        /// <summary>
        /// 書籍狀態Book_Code.Code_Id
        /// </summary>
        [Required(ErrorMessage = "此欄位必填")]
        [DisplayName("借閱狀態")]
        public string Book_Status_Id { get; set; }

        [DisplayName("借閱狀態")]
        public string Book_Status_Name { get; set; }
        /// <summary>
        /// 書籍借閱人
        /// </summary>
        [DisplayName("借閱人")]
        public string Book_Keeper { get; set; }

        public string Book_Keeper_Cname { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime Create_Date { get; set; }

        /// <summary>
        /// 建立使用者
        /// </summary>
        public string Cteate_User { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        public DateTime Modify_Date { get; set; }

        /// <summary>
        /// 修改使用者
        /// </summary>
        public string Modify_User { get; set; }

    }
}