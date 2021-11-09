using System;
using AutoMapper;
using BL.DTOs;
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
            config.CreateMap<EBookInstance, EBookInstanceDTO>().ReverseMap();
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
            config.CreateMap<EReader, EReaderPrevDTO>().ReverseMap();
            config.CreateMap<Reservation, ReservationPrevDTO>().ReverseMap();
            config.CreateMap<User, UserPrevDTO>().ReverseMap();

            // Connection Tables maps

            config.CreateMap<Author_Book, Author_BookDTO>().ReverseMap();
            config.CreateMap<Book_Genre, Book_GenreDTO>().ReverseMap();
            config.CreateMap<BookCollection_Book, BookCollection_BookDTO>().ReverseMap();
            config.CreateMap<Reservation_BookInstance, Reservation_BookInstanceDTO>().ReverseMap();
        }
    }
}
