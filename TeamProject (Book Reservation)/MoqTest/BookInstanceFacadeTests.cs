using Autofac.Extras.Moq;
using AutoMapper;
using BL.Config;
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
    public class BookInstanceFacadeTests
    {
        private IMapper _mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

        #region GetAllBooks

        private BookInstanceFacade SetupGetAllBooks
           (AutoMock mock, Func<Tuple<List<BookInstance>, List<AuthorBook>>> dataFunc)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            var data = dataFunc();

            mock.Mock<IQuery<BookInstance>>()
                .Setup(x => x.Execute().Result)
                .Returns(new QueryResult<BookInstance>() { Items = data.Item1 });
            var bookInstanceQuery = mock.Create<IQuery<BookInstance>>();

            var authorRepo = mock.Mock<IRepository<Author>>().Object;
            mock.Mock<IQuery<AuthorBook>>()
                .Setup(x => x.Execute().Result)
                .Returns(new QueryResult<AuthorBook>() { Items = data.Item2 });
            var authorBookQuery = mock.Create<IQuery<AuthorBook>>();

            var bookInstanceQueryObject =
                new QueryObject<BookInstancePrevDTO, BookInstance>(_mapper,bookInstanceQuery);
               
            var authorBookQueryObject =
                 new QueryObject<AuthorBookDTO, AuthorBook>(_mapper, authorBookQuery);

            var bookInstancePrevService = new BookInstancePreviewService
                (null, _mapper, bookInstanceQueryObject, null);
            var authorService = new AuthorService(authorRepo,_mapper,null,authorBookQueryObject);

            var bookInstanceFacadee = new BookInstanceFacade(uow, null, null, bookInstancePrevService, null, authorService, null);

            return bookInstanceFacadee;
        }

        [Fact]
        public async Task GetAllBooks1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookInstanceFacade = SetupGetAllBooks(mock, GetEntriesGetAllBooks);

                var data = GetEntriesGetAllBooks();
                var result = await bookInstanceFacade.GetAllBookInstances();
                EvaluateGetAllBooks(mock, data, result);
            }
        }

        [Fact]
        public async Task GetAllBooks2()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookInstanceFacade = SetupGetAllBooks(mock, GetEntriesGetAllBooks2);

                var data = GetEntriesGetAllBooks2();
                var result = await bookInstanceFacade.GetAllBookInstances();
                EvaluateGetAllBooks(mock, data, result);
            }
        }

        [Fact]
        public async Task GetAllBooks3()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookInstanceFacade = SetupGetAllBooks(mock, GetEntriesGetAllBooks3);

                var data = GetEntriesGetAllBooks3();
                var result = await bookInstanceFacade.GetAllBookInstances();
                EvaluateGetAllBooks(mock, data, result);
            }
        }

        private static void EvaluateGetAllBooks(AutoMock mock, Tuple<List<BookInstance>, List<AuthorBook>> data, IEnumerable<BookInstancePrevDTO> result)
        {
            foreach (var entry in data.Item1)
            {
                Assert.Contains(mock.Mock<IQuery<AuthorBook>>().Invocations.Where(invo => invo.Method.Name == nameof(IQuery<AuthorBook>.Where))
                    ,invo => (int)(invo.Arguments[0] as SimplePredicate).ComparedValue == entry.BookTemplateID);
            }

            Assert.True(result.Count() == data.Item1.Count());

            Assert.True(mock.Mock<IQuery<AuthorBook>>().
                Invocations.Where(invo => invo.Method.Name == nameof(IQuery<AuthorBook>.Execute)).Count() == data.Item1.Count());

            Assert.True(mock.Mock<IRepository<Author>>().Invocations.Count == data.Item1.Count * data.Item2.Count);
        }

        public Tuple<List<BookInstance>, List<AuthorBook>> GetEntriesGetAllBooks()
        {
            var bookInstances = new List<BookInstance>()
            {
                new BookInstance()
                {
                    Id = 1,
                    BookTemplateID = 1,
                    FromBookTemplate = new Book()
                    {
                        Id = 1
                    }
                },
                new BookInstance()
                {
                    Id = 2,
                    BookTemplateID = 2,
                    FromBookTemplate = new Book()
                    {
                        Id = 2
                    }
                }
            };

            var randomAuthors = new List<AuthorBook>()
            {
                new AuthorBook()
                {
                    AuthorID = 1,                    
                },
                new AuthorBook()
                {
                    AuthorID = 2,
                },
                new AuthorBook()
                {
                    AuthorID = 3,
                }
            };

            return new Tuple<List<BookInstance>, List<AuthorBook>>(bookInstances, randomAuthors);
        }

        public Tuple<List<BookInstance>, List<AuthorBook>> GetEntriesGetAllBooks2()
        {
            var bookInstances = new List<BookInstance>()
            {
                
            };

            var randomAuthors = new List<AuthorBook>()
            {
                
            };

            return new Tuple<List<BookInstance>, List<AuthorBook>>(bookInstances, randomAuthors);
        }

        public Tuple<List<BookInstance>, List<AuthorBook>> GetEntriesGetAllBooks3()
        {
            var bookInstances = new List<BookInstance>()
            {
                new BookInstance()
                {
                    Id = 1,
                    BookTemplateID = 1,
                    FromBookTemplate = new Book()
                    {
                        Id = 1
                    }
                },
                new BookInstance()
                {
                    Id = 2,
                    BookTemplateID = 2,
                    FromBookTemplate = new Book()
                    {
                        Id = 2
                    }
                },
                new BookInstance()
                {
                    Id = 3,
                    BookTemplateID = 3,
                    FromBookTemplate = new Book()
                    {
                        Id = 3
                    }
                }

            };

            var randomAuthors = new List<AuthorBook>()
            {
                
            };

            return new Tuple<List<BookInstance>, List<AuthorBook>>(bookInstances, randomAuthors);
        }

        #endregion

        #region GetBookReservationPrevsByBookInstanceAndDate

        private BookInstanceFacade SetupGetBookReservationPrevsByBookInstanceAndDate
           (AutoMock mock, Func<Tuple<int, DateTime?, DateTime?, List<ReservationBookInstance>>> dataFunc)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            var data = dataFunc();

            mock.Mock<IQuery<ReservationBookInstance>>()
                .Setup(x => x.Execute().Result)
                .Returns(new QueryResult<ReservationBookInstance>() { Items = data.Item4 });
            var reservationBookInstanceQuery = mock.Create<IQuery<ReservationBookInstance>>();

            var userRepo = mock.Mock<IRepository<User>>().Object;

            var reservationBookInstanceQueryObject = new QueryObject<ReservationBookInstanceDTO, ReservationBookInstance>(_mapper, reservationBookInstanceQuery);

            var userService = new CRUDService<UserPrevDTO, User>(userRepo, _mapper, null);
            var reservationServivce = new ReservationService(null, _mapper, null, null, reservationBookInstanceQueryObject);

            var bookInstanceFacadee = new BookInstanceFacade(uow, null, reservationServivce, null, userService, null,null);

            return bookInstanceFacadee;
        }

        [Fact]
        public async Task GetBookReservationPrevsByBookInstanceAndDate1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookInstanceFacade = SetupGetBookReservationPrevsByBookInstanceAndDate(mock, GetGetBookReservationPrevsByBookInstanceAndDate);

                var data = GetGetBookReservationPrevsByBookInstanceAndDate();
                var result = await bookInstanceFacade.GetBookReservationPrevsByBookInstanceAndDate(data.Item1, data.Item2, data.Item3);
                EvaluateGetBookReservationPrevsByBookInstanceAndDate(mock, data);
            }
        }

        [Fact]
        public async Task GetBookReservationPrevsByBookInstanceAndDate2()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookInstanceFacade = SetupGetBookReservationPrevsByBookInstanceAndDate(mock, GetGetBookReservationPrevsByBookInstanceAndDate2);

                var data = GetGetBookReservationPrevsByBookInstanceAndDate2();
                var result = await bookInstanceFacade.GetBookReservationPrevsByBookInstanceAndDate(data.Item1, data.Item2, data.Item3);
                EvaluateGetBookReservationPrevsByBookInstanceAndDate(mock, data);
            }
        }

        [Fact]
        public async Task GetBookReservationPrevsByBookInstanceAndDate3()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookInstanceFacade = SetupGetBookReservationPrevsByBookInstanceAndDate(mock, GetGetBookReservationPrevsByBookInstanceAndDate3);

                var data = GetGetBookReservationPrevsByBookInstanceAndDate3();
                var result = await bookInstanceFacade.GetBookReservationPrevsByBookInstanceAndDate(data.Item1, data.Item2, data.Item3);
                EvaluateGetBookReservationPrevsByBookInstanceAndDate(mock, data);
            }
        }

        private static void EvaluateGetBookReservationPrevsByBookInstanceAndDate(AutoMock mock, Tuple<int, DateTime?, DateTime?, List<ReservationBookInstance>> data)
        {
            Assert.True(mock.Mock<IQuery<ReservationBookInstance>>().
                                Invocations.Where(invo => invo.Method.Name == nameof(IQuery<AuthorBook>.Execute)).Count() == 1);

            IEnumerable<ReservationBookInstance> filteredResult;
            if (data.Item2 == null && data.Item3 == null)
            {
                filteredResult = data.Item4;
            }
            else
            {
                filteredResult = data.Item4.Where(x => x.Reservation.DateFrom >= data.Item2 && x.Reservation.DateTill <= data.Item3);
            }
            

            Assert.True(mock.Mock<IRepository<User>>().
                Invocations.Where(invo => invo.Method.Name == nameof(IRepository<User>.GetByID)).Count() == filteredResult.Count());

            foreach (var entry in filteredResult)
            {
                Assert.Contains(mock.Mock<IRepository<User>>().Invocations.Where(invo => invo.Method.Name == nameof(IRepository<User>.GetByID))
                    , invo => (int)invo.Arguments[0] == entry.Reservation.UserID);
            }
        }

        public Tuple<int, DateTime?, DateTime?, List<ReservationBookInstance>> GetGetBookReservationPrevsByBookInstanceAndDate()
        {
            var bookInstance = new BookInstance()
            {
                Id = 1
            };

            var reservationBookInstanceList = new List<ReservationBookInstance>()
            {
                new ReservationBookInstance()
                {                    
                    Reservation = new Reservation()
                    {
                        Id = 1,
                        UserID = 2,
                        DateFrom = new DateTime(1999,1,1),
                        DateTill = new DateTime(2000,1,1)
                    }, 
                },
                new ReservationBookInstance()
                {
                    Reservation = new Reservation()
                    {
                        Id = 2,
                        UserID = 3,
                        DateFrom = new DateTime(1980,1,1),
                        DateTill = new DateTime(1985,1,1)
                    },
                },
                new ReservationBookInstance()
                {
                    Reservation = new Reservation()
                    {
                        Id = 3,
                        UserID = 4,
                        DateFrom = new DateTime(2001,1,1),
                        DateTill = new DateTime(2004,1,1)
                    },
                }
            };

            return new Tuple<int, DateTime?, DateTime?, List<ReservationBookInstance>>(bookInstance.Id, new DateTime(1998,1,1), new DateTime(2002,1,1), reservationBookInstanceList);
        }

        public Tuple<int, DateTime?, DateTime?, List<ReservationBookInstance>> GetGetBookReservationPrevsByBookInstanceAndDate2()
        {
            var bookInstance = new BookInstance()
            {
                Id = 1
            };

            var reservationBookInstanceList = new List<ReservationBookInstance>()
            {
                new ReservationBookInstance()
                {
                    Reservation = new Reservation()
                    {
                        Id = 1,
                        UserID = 2,
                        DateFrom = new DateTime(1999,1,1),
                        DateTill = new DateTime(2000,1,1)
                    },
                },                
            };

            return new Tuple<int, DateTime?, DateTime?, List<ReservationBookInstance>>(bookInstance.Id, null, null, reservationBookInstanceList);
        }

        public Tuple<int, DateTime?, DateTime?, List<ReservationBookInstance>> GetGetBookReservationPrevsByBookInstanceAndDate3()
        {
            var bookInstance = new BookInstance()
            {
                Id = 1
            };

            var reservationBookInstanceList = new List<ReservationBookInstance>()
            {
                
            };

            return new Tuple<int, DateTime?, DateTime?, List<ReservationBookInstance>>(bookInstance.Id, null, null, reservationBookInstanceList);
        }

        #endregion

        #region GetBookInstancePrevsByUser

        private BookInstanceFacade SetupGetBookInstancePrevsByUser
           (AutoMock mock, Func<Tuple<int, List<BookInstance>, List<AuthorBook>>> dataFunc)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            var data = dataFunc();

            mock.Mock<IQuery<BookInstance>>()
                .Setup(x => x.Execute().Result)
                .Returns(new QueryResult<BookInstance>() { Items = data.Item2 });
            var bookInstanceQuery = mock.Create<IQuery<BookInstance>>();

            var authorRepo = mock.Mock<IRepository<Author>>().Object;
            mock.Mock<IQuery<AuthorBook>>()
                .Setup(x => x.Execute().Result)
                .Returns(new QueryResult<AuthorBook>() { Items = data.Item3 });
            var authorBookQuery = mock.Create<IQuery<AuthorBook>>();

            var bookInstanceQueryObject =
                new QueryObject<BookInstancePrevDTO, BookInstance>(_mapper, bookInstanceQuery);

            var authorBookQueryObject =
                 new QueryObject<AuthorBookDTO, AuthorBook>(_mapper, authorBookQuery);

            var bookInstancePrevService = new BookInstancePreviewService
                (null, _mapper, bookInstanceQueryObject, null);
            var authorService = new AuthorService(authorRepo, _mapper, null, authorBookQueryObject);

            var bookInstanceFacadee = new BookInstanceFacade(uow, null, null, bookInstancePrevService, null, authorService,null);

            return bookInstanceFacadee;
        }

        [Fact]
        public async Task GetBookInstancePrevsByUser1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookInstanceFacade = SetupGetBookInstancePrevsByUser(mock, GetEntriesGetBookInstancePrevsByUser);

                var data = GetEntriesGetBookInstancePrevsByUser();
                var result = await bookInstanceFacade.GetBookInstancePrevsByUser(data.Item1);
                EvaluateGetBookInstancePrevsByUser(mock, data, result);
            }
        }

        [Fact]
        public async Task GetBookInstancePrevsByUser2()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookInstanceFacade = SetupGetBookInstancePrevsByUser(mock, GetEntriesGetBookInstancePrevsByUser2);

                var data = GetEntriesGetBookInstancePrevsByUser2();
                var result = await bookInstanceFacade.GetBookInstancePrevsByUser(data.Item1);
                EvaluateGetBookInstancePrevsByUser(mock, data, result);
            }
        }

        [Fact]
        public async Task GetBookInstancePrevsByUser3()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookInstanceFacade = SetupGetBookInstancePrevsByUser(mock, GetEntriesGetBookInstancePrevsByUser3);

                var data = GetEntriesGetBookInstancePrevsByUser3();
                var result = await bookInstanceFacade.GetBookInstancePrevsByUser(data.Item1);
                EvaluateGetBookInstancePrevsByUser(mock, data, result);
            }
        }

        private static void EvaluateGetBookInstancePrevsByUser(AutoMock mock, Tuple<int, List<BookInstance>, List<AuthorBook>> data, IEnumerable<BookInstancePrevDTO> result)
        {
            foreach (var entry in data.Item2)
            {
                Assert.Contains(mock.Mock<IQuery<AuthorBook>>().Invocations.Where(invo => invo.Method.Name == nameof(IQuery<AuthorBook>.Where))
                    , invo => (int)(invo.Arguments[0] as SimplePredicate).ComparedValue == entry.BookTemplateID);
            }

            Assert.True(result.Count() == data.Item2.Count());

            Assert.True(mock.Mock<IQuery<AuthorBook>>().
                Invocations.Where(invo => invo.Method.Name == nameof(IQuery<AuthorBook>.Execute)).Count() == data.Item2.Count());

            Assert.True(mock.Mock<IRepository<Author>>().Invocations.Count == data.Item2.Count * data.Item3.Count);
        }

        public Tuple<int,List<BookInstance>, List<AuthorBook>> GetEntriesGetBookInstancePrevsByUser()
        {
            var bookInstances = new List<BookInstance>()
            {
                new BookInstance()
                {
                    Id = 1,
                    BookTemplateID = 1,
                    FromBookTemplate = new Book()
                    {
                        Id = 1
                    }
                },
                new BookInstance()
                {
                    Id = 2,
                    BookTemplateID = 2,
                    FromBookTemplate = new Book()
                    {
                        Id = 2
                    }
                }
            };

            var randomAuthors = new List<AuthorBook>()
            {
                new AuthorBook()
                {
                    AuthorID = 1,
                },
                new AuthorBook()
                {
                    AuthorID = 2,
                },
                new AuthorBook()
                {
                    AuthorID = 3,
                }
            };

            return new Tuple<int, List<BookInstance>, List<AuthorBook>>(1, bookInstances, randomAuthors);
        }

        public Tuple<int, List<BookInstance>, List<AuthorBook>> GetEntriesGetBookInstancePrevsByUser2()
        {
            var bookInstances = new List<BookInstance>()
            {
                new BookInstance()
                {
                    Id = 1,
                    BookTemplateID = 1,
                    FromBookTemplate = new Book()
                    {
                        Id = 1
                    }
                },
                new BookInstance()
                {
                    Id = 2,
                    BookTemplateID = 2,
                    FromBookTemplate = new Book()
                    {
                        Id = 2
                    }
                }
            };

            var randomAuthors = new List<AuthorBook>()
            {
                
            };

            return new Tuple<int, List<BookInstance>, List<AuthorBook>>(1, bookInstances, randomAuthors);
        }

        public Tuple<int, List<BookInstance>, List<AuthorBook>> GetEntriesGetBookInstancePrevsByUser3()
        {
            var bookInstances = new List<BookInstance>()
            {
                new BookInstance()
                {
                    Id = 1,
                    BookTemplateID = 1,
                    FromBookTemplate = new Book()
                    {
                        Id = 1
                    }
                },                
            };

            var randomAuthors = new List<AuthorBook>()
            {
                new AuthorBook()
                {
                    AuthorID = 1,
                },
            };

            return new Tuple<int, List<BookInstance>, List<AuthorBook>>(1, bookInstances, randomAuthors);
        }

        #endregion
    }
}
