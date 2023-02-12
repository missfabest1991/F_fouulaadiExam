using MVCLibrary.Data;
using MVCLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLibrary.Services.ServiceInterfaces
{
    public interface IBookDetailService
    {
        Task<BookDetail> MapToEntity(BookDetailDTO bookDetail);
        Task<BookDetailDTO> MapToDTO(BookDetail bookDetail); //map from BookDetail to BookDetailDTO
        Task<IEnumerable<BookDetailDTO>> GetBookDetails(); //list of BookDetails
        Task<BookDetailDTO?> GetBookDetail(int bookDetailid); //just get a BookDetail
        Task<BookDetailDTO> Create(BookDetailDTO bookDetail); //create a BookDetail
        Task<BookDetailDTO> Update(BookDetailDTO bookDetail); //update a entity
        Task Remove(int bookDetailid); //remove a BookDetail
    }
}
