using AutoMapper;
using BL.DTOs;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Filters;
using BL.DTOs.FullVersions;
using BL.DTOs.Previews;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;

namespace BL.Config
{
    public class MappingProfile
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            // Entities maps

            config.CreateMap<Author, AuthorDTO>().ReverseMap();
            config.CreateMap<BookCollection, BookCollectionDTO>().ReverseMap();
            config.CreateMap<Book, BookDTO>().ReverseMap();
            config.CreateMap<BookInstance, BookInstanceDTO>().ReverseMap();
            config.CreateMap<EBook, EBookDTO>().ReverseMap();
            config.CreateMap<EReader, EReaderDTO>().ReverseMap();
            config.CreateMap<EReaderInstance, EReaderInstanceDTO>().ReverseMap();
            config.CreateMap<Genre, GenreDTO>().ReverseMap();
            config.CreateMap<Reservation, ReservationDTO>().ReverseMap();
            config.CreateMap<Review, ReviewDTO>().ReverseMap();
            config.CreateMap<User, UserDTO>().ReverseMap();


            config.CreateMap<CompositePredicate, CompositePredicateDto>().ReverseMap();
            config.CreateMap<IPredicate, IPredicateDto>().ReverseMap();
            config.CreateMap<SimplePredicate, PredicateDto>().ReverseMap();
            config.CreateMap(typeof(QueryResult<>), typeof(QueryResultDTO<>)).ReverseMap();

            config.CreateMap<Author, AuthorPrevDTO>().ReverseMap();
            config.CreateMap<BookCollection, BookCollectionPrevDTO>().ReverseMap();
            config.CreateMap<Book, BookPrevDTO>().ReverseMap();
            config.CreateMap<BookInstance, BookInstancePrevDTO>().ReverseMap();
            config.CreateMap<EReader, EReaderPrevDTO>().ReverseMap();
            config.CreateMap<Reservation, ReservationPrevDTO>().ReverseMap();
            config.CreateMap<User, UserPrevDTO>().ReverseMap();
            config.CreateMap<EBook, EBookPrevDTO>().ReverseMap();
            config.CreateMap<EReaderInstance, EReaderInstancePrevDTO>();
            config.CreateMap<ReservationDTO, ReservationPrevDTO>();

            // Connection Tables maps

            config.CreateMap<AuthorBook, AuthorBookDTO>().ReverseMap();
            config.CreateMap<BookGenre, BookGenreDTO>().ReverseMap();
            config.CreateMap<BookCollectionBook, BookCollectionBookDTO>().ReverseMap();
            config.CreateMap<ReservationBookInstance, ReservationBookInstanceDTO>().ReverseMap();
            config.CreateMap<EBookEReaderInstance, EBookEReaderInstanceDTO>().ReverseMap();
        }
    }
}
