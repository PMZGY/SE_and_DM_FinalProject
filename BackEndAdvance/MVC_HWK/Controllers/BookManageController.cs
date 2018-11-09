using MVC_HWK.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVC_HWK.Controllers
{
    public class BookManageController : Controller
    {
        private ICodeTableService GetTableService { get; set; }
        private IBookService BookService { get; set; }

        // GET: BookManage
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                MVC_HWK.Common.Logger.Write(MVC_HWK.Common.Logger.LogCategory.Error, ex.ToString());
                Console.WriteLine(ex);
                throw;
            }

        }


        [HttpPost()]
        public JsonResult Index(MVC_HWK.Model.BookSearchArg BookSearchArg)
        {

            var result=BookService.GetBookDataByCondition(BookSearchArg);

            return this.Json(result);
        }
        
       
        public JsonResult DropDownListBookClass()
        {
            return this.Json(GetTableService.GetBookClassTable(),JsonRequestBehavior.AllowGet);
        }
        public JsonResult DropDownListBookKeeper()
        {
            return this.Json(GetTableService.GetMemberTable(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DropDownListStatus()
        {
            return this.Json(GetTableService.GetStatusTable("BOOK_STATUS"), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult InsertBook()
        {          
            return View();
        }

        [HttpPost()]
        public JsonResult InsertBook(MVC_HWK.Model.BookData BookData)
        {
            try
            {
                BookService.InsertBook(BookData);
                return this.Json(true);
            }
            catch (Exception ex)
            {
                throw new Exception("InsertFail");
            }
        }

        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteBook(string BookId)
        {
            List<MVC_HWK.Model.BookData> BookStatus = BookService.SearchBookById(BookId);
            if (BookStatus[0].Book_Keeper != "") {
                return Json(new { message = "fail" });
            }
            
            try
            {
                BookService.DeleteBookById(BookId);
                return this.Json(true);
                // 3
            }
            catch (Exception ex)
            {
                throw new Exception("deletefail");
            }
        }


        /// <summary>
        /// 修改書籍畫面
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult UpdateBook(string BookId)
        {
            return View();
        }

        [HttpPost()]
        public ActionResult UpdateBook(MVC_HWK.Model.BookData BookData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                        BookService.UpdateBook(BookData);                                     
                }
                return View(BookData);

            }
            catch (Exception ex)
            {
                throw new Exception("updatefail");
            }
           
        }

        //用BookId取得書籍資料傳回UpdateBook
        public JsonResult LoadBookData(string BookId)
        {
            return this.Json(BookService.SearchBookById(BookId).FirstOrDefault<MVC_HWK.Model.BookData>(),JsonRequestBehavior.AllowGet);
        }

    }
}