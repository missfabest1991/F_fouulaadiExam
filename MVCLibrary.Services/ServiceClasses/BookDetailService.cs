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
using System.Xml.Linq;

namespace MVCLibrary.Services.ServiceClasses
{
    public class BookDetailService : IBookDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private int bookDetailid;

        public BookDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        ///////////////////////////////////////////////////////////// MapToDTO ////////////////////////

        public async Task<BookDetailDTO> MapToDTO(BookDetail bookDetail)
        {



            return new BookDetailDTO()
            {
                Id = bookDetail.Id,
                BookId = bookDetail.BookId,
                CountEdition = bookDetail.CountEdition,
                Description = bookDetail.Description,
                PublishDateTime = bookDetail.PublishDateTime

            };


        }
        ///////////////////////////////////////////////////////////// MapToEntity ////////////////////////

        public async Task<BookDetail> MapToEntity(BookDetailDTO bookDetail)
        {
            return await Task.Run(() => new BookDetail()
            {
                Id = bookDetail.Id,
                BookId = bookDetail.BookId,
                CountEdition = bookDetail.CountEdition,
                Description = bookDetail.Description,
                PublishDateTime = bookDetail.PublishDateTime
            });
        }
        ///////////////////////////////////////////////////////////// Create ////////////////////////

        public async Task<BookDetailDTO> Create(BookDetailDTO bookDetail)
        {
            var data = await MapToEntity(bookDetail);
            await _unitOfWork.BookDetailRepository.Add(data);
            await _unitOfWork.Commit();
            return bookDetail;
        }

        ///////////////////////////////////////////////////////////// Update ////////////////////////

        public async Task<BookDetailDTO> Update(BookDetailDTO bookDetail)
        {
            var bdata = await _unitOfWork.BookDetailRepository.Add(await MapToEntity(bookDetail));
            _unitOfWork.Commit();
            return await MapToDTO(bdata);
        }

        ///////////////////////////////////////////////////////////// Remove ////////////////////////

        public async  Task Remove(int bookDetailid)
        {
            await _unitOfWork.BookDetailRepository.Delete(await _unitOfWork.BookDetailRepository.GetById(bookDetailid));
            await _unitOfWork.Commit();
        }


        ///////////////////////////////////////////////////////////// GetBookDetail ////////////////////////

        public async Task<BookDetailDTO?> GetBookDetail(int bookDetailid)
        {
            var data = await _unitOfWork.BookDetailRepository.GetById(bookDetailid);
            return await MapToDTO(data);
        }
        ///////////////////////////////////////////////////////////// GetBookDetails ////////////////////////

        public async Task<IEnumerable<BookDetailDTO>> GetBookDetails()
        {
            IQueryable<BookDetail> datas = await _unitOfWork.BookDetailRepository.GetAll();
            var ttt= datas != null && datas.Any() ? datas.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<BookDetailDTO>();
            return ttt;
        }

       
    }
}
