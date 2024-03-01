using ApiBiblioteca.Application.ViewModel;
using ApiBiblioteca.Domain.DTOs;
using ApiBiblioteca.Domain.Models;
using AutoMapper;

namespace ApiBiblioteca.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            // ViewModel to Model
            CreateMap<AddressViewModel, Address>();
            CreateMap<UserViewModel, User>();
            CreateMap<BookViewModel, Book>();
            
            // Model to ModelDTO
            CreateMap<Address, AddressDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Book, BookDTO>();
            CreateMap<Loan, LoanDTO>()
                .ForMember(dest => dest.User, m => m.MapFrom(orig => orig.User.Name))
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.BookLendings.Select(bl => bl.Book).ToList()));
        }
    }
}
