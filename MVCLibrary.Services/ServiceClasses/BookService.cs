using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MVCLibrary.Data;
using MVCLibrary.DTO;
using MVCLibrary.Infrastructure;
using MVCLibrary.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCLibrary.Services.ServiceClasses
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        ///////////////////////////////////////////////////MapToDTO/////////////////////////////////////////////////

        public async Task<BookDTO> MapToDTO(Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Name = book.Name,
                AuthorName = book.AuthorName,
                Price = book.Price,
                LibraryId = book.LibraryId,
                CategoryId = book.CategoryId

            };
        }
        ///////////////////////////////////////////////////MapToEntity/////////////////////////////////////////////////

        public async Task<Book> MapToEntity(BookDTO book)
        {
            return await Task.Run(() => new Book()
               {
                Id = book.Id,
                Name = book.Name,
                AuthorName = book.AuthorName,
                Price = book.Price,
                LibraryId = book.LibraryId,
                CategoryId = book.CategoryId
               });
        }

        ///////////////////////////////////////////////////CREATE/////////////////////////////////////////////////

   
        public async Task<BookDTO> Create(BookDTO book)
        {
            var data=await MapToEntity(book);
            await _unitOfWork.BookRepository.Add(data);
            await _unitOfWork.Commit();
            return book;
        }

        ///////////////////////////////////////////////////Update/////////////////////////////////////////////////


        public async Task<BookDTO> Update(BookDTO book)
        {
            var bdata = await _unitOfWork.BookRepository.Add(await MapToEntity(book));
            _unitOfWork.Commit();
            return await MapToDTO(bdata);
        }
        ///////////////////////////////////////////////////Remove/////////////////////////////////////////////////


        public async Task Remove(int bookId)
        {
            await _unitOfWork.BookRepository.Delete(await _unitOfWork.BookRepository.GetById(bookId));
            await _unitOfWork.Commit();
        }

        ///////////////////////////////////////////////////GetBook/////////////////////////////////////////////////


        public async Task<BookDTO?> GetBook(int bookid)
        {
            var data = await _unitOfWork.BookRepository.GetById(bookid);
            return await MapToDTO(data);
        }

        ///////////////////////////////////////////////////GetBooks/////////////////////////////////////////////////

        public async Task<IEnumerable<BookDTO>> GetBooks()
        {
            IEnumerable<Book> datas = await _unitOfWork.BookRepository.GetAll();
          var ttt= datas != null && datas.Any() ? datas.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<BookDTO>();
            return ttt;
        }


       


        }
}
