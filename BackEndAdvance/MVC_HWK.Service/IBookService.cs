using System.Collections.Generic;
using MVC_HWK.Model;

namespace MVC_HWK.Service
{
    public interface IBookService
    {
        void DeleteBookById(string BookId);
        List<BookData> GetBookDataByCondition(BookSearchArg bookSearchArg);
        int InsertBook(BookData BookData);
        List<BookData> SearchBookById(string BookId);
        int UpdateBook(BookData BookData);
    }
}