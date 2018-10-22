using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC_HWK.Model;

namespace MVC_HWK.Dao
{
    public class BookTestDao : IBookDao
    {
        public void DeleteBookById(string BookId)
        {
            throw new NotImplementedException();
        }

        public List<BookData> GetBookDataByCondition(BookSearchArg bookSearchArg)
        {
            throw new NotImplementedException();
        }

        public int InsertBook(BookData BookData)
        {
            throw new NotImplementedException();
        }

        public List<BookData> SearchBookById(string BookId)
        {
            throw new NotImplementedException();
        }

        public int UpdateBook(BookData BookData)
        {
            throw new NotImplementedException();
        }
    }
}
