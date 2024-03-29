﻿using AutoMapper;
using BL.DTOs;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Entities.EReader;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.Genre;
using BL.DTOs.Entities.Reservation;
using BL.DTOs.Entities.Review;
using BL.DTOs.Entities.User;
using BL.DTOs.Enums;
using BL.DTOs.Filters;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using DAL.Enums;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using System.Linq;

namespace BL.Config
{
    public class MappingProfile
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            // Entities maps

            config.CreateMap<Author, AuthorDTO>().ReverseMap();
            config.CreateMap<BookCollection, BookCollectionDTO>().ReverseMap();
            config.CreateMap<BookCollection, BookCollectionCreateDTO>().ReverseMap();
            config.CreateMap<BookCollectionDTO, BookCollectionCreateDTO>().ReverseMap();
            config.CreateMap<Book, BookDTO>().ReverseMap();
            config.CreateMap<BookInstance, BookInstanceDTO>().ReverseMap();
            config.CreateMap<BookInstanceDTO, BookInstanceCreateDTO>().ReverseMap();
            config.CreateMap<EBook, EBookDTO>().ReverseMap();
            config.CreateMap<EReader, EReaderDTO>().ReverseMap();
            config.CreateMap<EReaderInstance, EReaderInstanceDTO>().ReverseMap();
            config.CreateMap<Genre, GenreDTO>().ReverseMap();
            config.CreateMap<Reservation, ReservationDTO>().ReverseMap();
            config.CreateMap<Review, ReviewDTO>().ReverseMap();
            config.CreateMap<User, UserDTO>().ReverseMap();
            config.CreateMap<User, UserLoginDTO>().ReverseMap();
            config.CreateMap<User, UserCreateDTO>().ReverseMap();
            config.CreateMap<UserDTO, UserCreateDTO>().ReverseMap();
            config.CreateMap<UserDTO, UserEditDTO>().ReverseMap();
            config.CreateMap<User, UserShowDTO>().ReverseMap();
            config.CreateMap<User, UserEditDTO>().ReverseMap();

            config.CreateMap(typeof(QueryResult<>), typeof(QueryResultDTO<>)).ReverseMap();

            config.CreateMap<Author, AuthorPrevDTO>().ReverseMap();
            config.CreateMap<BookCollection, BookCollectionPrevDTO>().ReverseMap();
            config.CreateMap<Book, BookPrevDTO>().ReverseMap();
            config.CreateMap<BookInstance, BookInstancePrevDTO>().ReverseMap();
            config.CreateMap<EReader, EReaderPrevDTO>().ReverseMap();
            config.CreateMap<Reservation, ReservationPrevDTO>().ReverseMap();
            config.CreateMap<User, UserPrevDTO>().ReverseMap();
            config.CreateMap<UserDTO, UserPrevDTO>().ReverseMap();
            config.CreateMap<EBook, EBookPrevDTO>().ReverseMap();
            config.CreateMap<EReaderInstance, EReaderInstancePrevDTO>().ReverseMap();
            config.CreateMap<EReaderInstance, EReaderInstanceCreateDTO>().ReverseMap();
            config.CreateMap<EReaderInstanceDTO, EReaderInstanceCreateDTO>().ReverseMap();
            config.CreateMap<ReservationDTO, ReservationPrevDTO>().ReverseMap();

            // Enums
            config.CreateMap<BookInstanceConditionDTO, BookInstanceCondition>().ReverseMap();
            config.CreateMap<EBookFormatDTO, EBookFormat>().ReverseMap();
            config.CreateMap<GenreTypeDTO, GenreType>().ReverseMap();
            config.CreateMap<LanguageDTO, Language>().ReverseMap();

            // Connection Tables maps
            config.CreateMap<AuthorBook, AuthorBookDTO>().ReverseMap();
            config.CreateMap<BookGenre, BookGenreDTO>().ReverseMap();
            config.CreateMap<BookCollectionBook, BookCollectionBookDTO>().ReverseMap();
            config.CreateMap<BookCollectionBook, AddBookInCollectionDTO>().ReverseMap();
            config.CreateMap<EBookEReaderInstance, AddEBookInEReaderInstanceDTO>().ReverseMap();
            config.CreateMap<ReservationBookInstance, ReservationBookInstanceDTO>().ReverseMap();
            config.CreateMap<EBookEReaderInstance, EBookEReaderInstanceDTO>().ReverseMap();
        }

        public static IPredicate ConvertPredicate(IPredicateDto predDto)
        {
            if (predDto is PredicateDto)
            {
                var pred = predDto as PredicateDto;
                return new SimplePredicate(pred.TargetPropertyName, pred.ComparedValue, pred.ValueComparingOperator);
            }
            else
            {
                var pred = predDto as CompositePredicateDto;
                return new CompositePredicate(pred.Predicates.Select(x => ConvertPredicate(x)), pred.Operator);
            }
        }
    }
}
