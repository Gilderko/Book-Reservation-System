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
        public async Task GetBook1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookFacade = Setup(mock, GetEntries);

                var data = GetEntries();
                await Evaluate(mock, bookFacade, data);
            }
        }

        [Fact]
        public async Task GetBook2()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookFacade = Setup(mock, GetEntries2);

                var data = GetEntries2();
                await Evaluate(mock, bookFacade, data);
            }
        }

        [Fact]
        public async Task GetBook3()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookFacade = Setup(mock, GetEntries3);

                var data = GetEntries3();
                await Evaluate(mock, bookFacade, data);
            }
        }

        private static async Task Evaluate(AutoMock mock, BookFacade bookFacade, Tuple<Book, string[], string[]> data)
        {
            var result = await bookFacade.Get(data.Item1.Id, data.Item2, data.Item3);

            Assert.True(mock.Mock<IRepository<Book>>().Invocations.Count() == 1);

            foreach (var review in data.Item1.Reviews)
            {
                UserRepoInvocationsInclude(mock, review.UserID);
            }

            foreach (var bookInstance in data.Item1.BookInstances)
            {
                UserRepoInvocationsInclude(mock, bookInstance.BookOwnerId);
                ReservBookInstInvocationsInclude(mock, bookInstance.Id);
            }

            Assert.True(mock.Mock<IQuery<ReservationBookInstance>>()
                .Invocations.Where(invo => invo.Method.Name == nameof(IQuery<ReservationBookInstance>.Execute)).Count() == data.Item1.BookInstances.Count());
        }

        private static void UserRepoInvocationsInclude(AutoMock mock, object argument)
        {
            var predicates = mock.Mock<IRepository<User>>().Invocations
                                .Where(invo => invo.Method.Name == nameof(IRepository<User>.GetByID))
                                .Select(invo => (int) invo.Arguments[0]);

            Assert.Contains(predicates, pred => pred.Equals(argument));
        }

        private static void ReservBookInstInvocationsInclude(AutoMock mock, object argument)
        {
            var predicates = mock.Mock<IQuery<ReservationBookInstance>>().Invocations
                                .Where(invo => invo.Method.Name == nameof(IQuery<ReservationBookInstance>.Where))
                                .Select(invo => (SimplePredicate)invo.Arguments[0]).ToList();

            Assert.Contains(predicates, pred => pred.ComparedValue.Equals(argument));
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

        public Tuple<Book, string[], string[]> GetEntries2()
        {
            var bookInst1 = new BookInstance()
            {
                Id = 1,
                BookOwnerId = 1
            };

            var review1 = new Review()
            {
                UserID = 1
            };

            var book = new Book()
            {
                Id = 1,
                BookInstances = new List<BookInstance>()
                {
                    bookInst1
                },
                Reviews = new List<Review>
                {
                    review1
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

        public Tuple<Book, string[], string[]> GetEntries3()
        {
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
                Reviews = new List<Review>
                {
                    review1, review2
                },
                BookInstances = new List<BookInstance>()                
            };

            var colToLoad = new string[]
            {
                nameof(BookDTO.Reviews),
            };

            var refsToLoad = new string[0];

            return new Tuple<Book, string[], string[]>(book, refsToLoad, colToLoad);
        }
    }
}
