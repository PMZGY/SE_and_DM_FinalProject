using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_HWK.Service
{
    public class BookService : IBookService
    {
        private MVC_HWK.Dao.IBookDao BookDao { get; set; }

        public List<MVC_HWK.Model.BookData> GetBookDataByCondition(MVC_HWK.Model.BookSearchArg bookSearchArg)
        {
            return BookDao.GetBookDataByCondition(bookSearchArg);
        }

        public int InsertBook(MVC_HWK.Model.BookData BookData)
        {
            return BookDao.InsertBook(BookData);
        }

        public int UpdateBook(MVC_HWK.Model.BookData BookData)
        {
            return BookDao.UpdateBook(BookData);
        }

        public void DeleteBookById(string BookId)
        {
            BookDao.DeleteBookById(BookId);
        }
        public List<MVC_HWK.Model.BookData> SearchBookById(string BookId)
        {
            return BookDao.SearchBookById(BookId);
        }
        }
}
