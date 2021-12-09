using Autofac.Extras.Moq;
using AutoMapper;
using BL.Config;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Entities.EReader;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.Genre;
using BL.DTOs.Entities.User;
using BL.DTOs.Enums;
using BL.Facades;
using BL.QueryObjects;
using BL.Services;
using BL.Services.Implementations;
using DAL;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MoqTest
{   
    public class BookFacadeTests
    {
        private IMapper _mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

        private BookFacade Setup(AutoMock mock, Func<Tuple<Book, string[], string[]>> dataFunc)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            mock.Mock<IRepository<Book>>()
                .Setup(x => x.GetByID(1, dataFunc().Item2, dataFunc().Item3).Result)
                .Returns(dataFunc().Item1);

            var bookRepo = mock.Create<IRepository<Book>>();

            var userRepo = mock.Mock<IRepository<User>>().Object;

            mock.Mock<IQuery<ReservationBookInstance>>()
                .Setup(x => x.Execute().Result)
                .Returns(new QueryResult<ReservationBookInstance>() { Items = new List<ReservationBookInstance>()});

            var reservationBookInstanceQuery = mock.Create<IQuery<ReservationBookInstance>>();
            var reservationBookInstanceQueryObject = 
                new QueryObject<ReservationBookInstanceDTO, ReservationBookInstance>(_mapper, reservationBookInstanceQuery);

            var userService = new CRUDService<UserDTO, User>(userRepo, _mapper, null);
            var bookService = new CRUDService<BookDTO, Book>(bookRepo, _mapper, null);
            var reservBookInstanceService =
                new CRUDService<ReservationBookInstanceDTO, ReservationBookInstance>(null, _mapper, reservationBookInstanceQueryObject);

            var bookFacade = new BookFacade(uow, bookService, null, null, userService, reservBookInstanceService, null, null, null);

            return bookFacade;
        }

        [Fact]
        public async Task GetBookPreviewsComplex()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookFacade = Setup(mock,GetEntries);

                var data = GetEntries();

                var result = await bookFacade.Get(data.Item1.Id, data.Item2, data.Item3);
                
            }
        }

        public Tuple<Book,string[],string[]> GetEntries()
        {
            var bookInst1 = new BookInstance() 
            { 
                Id = 1, 
                BookOwnerId = 1 
            };
            var bookInst2 = new BookInstance() 
            { 
                Id = 2, 
                BookOwnerId = 2 
            };

            var review1 = new Review()
            {
                UserID = 1
            };
            var review2 = new Review()
            {
                UserID = 2
            };

            var book = new Book()
            {
                Id = 1,
                BookInstances = new List<BookInstance>()
                {
                    bookInst1, bookInst2
                },
                Reviews = new List<Review>
                {
                    review1, review2
                }
            };

            var colToLoad = new string[]
            {
                nameof(BookDTO.Reviews),
                nameof(BookDTO.BookInstances)
            };

            var refsToLoad = new string[0];

            return new Tuple<Book, string[], string[]>(book, refsToLoad, colToLoad);
        }
    }
}
