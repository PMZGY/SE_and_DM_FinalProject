using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_HWK.Model
{
    public class BookSearchArg
    {
        /// <summary>
        /// 書籍名稱
        /// </summary>
        [DisplayName("書名")]
        public string Book_Name { get; set; }
        /// <summary>
        /// 書籍類別代號
        /// </summary>
        [DisplayName("圖書類別")]
        public string Book_Class_Name { get; set; }

        [DisplayName("圖書類別ID")]
        public string Book_Class_Id { get; set; }
        /// <summary>
        /// 書籍借閱人
        /// </summary>
        [DisplayName("借閱人")]
        public string Book_Keeper { get; set; }

        public string Book_Keeper_Cname { get; set; }
        /// <summary>
        /// 書籍狀態Book_Code.Code_Id
        /// </summary>
        [DisplayName("借閱狀態")]
        public string Book_Status_Name { get; set; }

        [DisplayName("借閱狀態ID")]
        public string Book_Status_Id { get; set; }
    }
}
