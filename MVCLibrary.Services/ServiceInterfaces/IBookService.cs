using MVCLibrary.Data;
using MVCLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLibrary.Services.ServiceInterfaces
{
    public interface IBookService
    {
        Task<Book> MapToEntity(BookDTO book); 
        Task<BookDTO> MapToDTO(Book book); //map from bookto bookDto
        Task<IEnumerable<BookDTO>> GetBooks(); //list of Books
        Task<BookDTO?> GetBook(int bookid); //just get a book
        Task<BookDTO> Create(BookDTO book); //create a book
        Task<BookDTO> Update(BookDTO book); //update a entity
        Task Remove(int bookId); //remove a book

    }
}
