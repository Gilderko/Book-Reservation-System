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
    public class ReservationFacadeTests
    {
        private IMapper _mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

        #region GetDeatilWithLoadedBooks

        private ReservationFacade SetupGetDetailWithLoadedBooks
           (AutoMock mock, Func<Tuple<Reservation, string[], string[]>> dataFunc)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            var data = dataFunc();

            mock.Mock<IRepository<Reservation>>()
                .Setup(x => x.GetByID(data.Item1.Id, data.Item2, data.Item3).Result)
                .Returns(data.Item1);
            var reservationRepo = mock.Create<IRepository<Reservation>>();

            var eReaderInstanceRepo = mock.Mock<IRepository<EReaderInstance>>().Object;
            var bookInstanceRepo = mock.Mock<IRepository<BookInstance>>().Object;

            var reservService = new ReservationService(reservationRepo, _mapper, null, null, null);
            var bookInstService = new CRUDService<BookInstanceDTO, BookInstance>(bookInstanceRepo, _mapper, null);
            var eReaderInstanceService = new CRUDService<EReaderInstanceDTO, EReaderInstance>(eReaderInstanceRepo, _mapper, null);

            var reservationFacade = new ReservationFacade(uow, reservService, bookInstService, eReaderInstanceService, null);

            return reservationFacade;
        }

        [Fact]
        public async Task GetDetailWithLoadedBooks1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var reservationFacade = SetupGetDetailWithLoadedBooks(mock, GetEntriesDetailWithLoadedBooks);

                var data = GetEntriesDetailWithLoadedBooks();
                await reservationFacade.GetDetailWithLoadedBooks(data.Item1.Id);
                EvaluateGetDetailWithLoadedBooks(mock, data);
            }
        }

        [Fact]
        public async Task GetDetailWithLoadedBooks2()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var reservationFacade = SetupGetDetailWithLoadedBooks(mock, GetEntriesDetailWithLoadedBooks2);

                var data = GetEntriesDetailWithLoadedBooks2();
                await reservationFacade.GetDetailWithLoadedBooks(data.Item1.Id);
                EvaluateGetDetailWithLoadedBooks(mock, data);
            }
        }

        [Fact]
        public async Task GetDetailWithLoadedBooks3()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var reservationFacade = SetupGetDetailWithLoadedBooks(mock, GetEntriesDetailWithLoadedBooks3);

                var data = GetEntriesDetailWithLoadedBooks3();
                await reservationFacade.GetDetailWithLoadedBooks(data.Item1.Id);
                EvaluateGetDetailWithLoadedBooks(mock, data);
            }
        }

        private static void EvaluateGetDetailWithLoadedBooks(AutoMock mock, Tuple<Reservation, string[], string[]> data)
        {
            foreach (var bookInst in data.Item1.BookInstances)
            {
                BookInstanceRepoInvocationsInclude(mock, bookInst.BookInstanceID);
            }

            Assert.True(mock.Mock<IRepository<Reservation>>().Invocations
                .Where(invo => invo.Method.Name == nameof(IRepository<User>.GetByID)).Count() == 1);

            Assert.True(mock.Mock<IRepository<BookInstance>>().Invocations
               .Where(invo => invo.Method.Name == nameof(IRepository<BookInstance>.GetByID)).Count() == data.Item1.BookInstances.Count());

            if (data.Item1.EReaderID != null)
            {
                Assert.True(mock.Mock<IRepository<EReaderInstance>>().Invocations
                    .Where(invo => invo.Method.Name == nameof(IRepository<EReaderInstance>.GetByID)).Count() == 1);
            }
        }

        private static void BookInstanceRepoInvocationsInclude(AutoMock mock, object argument)
        {
            var predicates = mock.Mock<IRepository<BookInstance>>().Invocations
                                .Where(invo => invo.Method.Name == nameof(IRepository<User>.GetByID))
                                .Select(invo => (int)invo.Arguments[0]);

            Assert.Contains(predicates, pred => pred.Equals(argument));
        }

        public Tuple<Reservation, string[], string[]> GetEntriesDetailWithLoadedBooks()
        {
            var reservation = new Reservation()
            {
                Id = 1,
                EReaderID = 1,
                BookInstances = new List<ReservationBookInstance>()
                {
                    new ReservationBookInstance()
                    {
                        ReservationID = 1,
                        BookInstanceID = 1
                    },
                    new ReservationBookInstance()
                    {
                        ReservationID = 1,
                        BookInstanceID = 2
                    },
                    new ReservationBookInstance()
                    {
                        ReservationID = 1,
                        BookInstanceID = 3
                    }
                }
            };

            var collRefToLoad = new string[]
            {
                nameof(ReservationDTO.BookInstances)
            };

            return new Tuple<Reservation, string[], string[]>(reservation, null, collRefToLoad);
        }

        public Tuple<Reservation, string[], string[]> GetEntriesDetailWithLoadedBooks2()
        {
            var reservation = new Reservation()
            {
                Id = 1,
                BookInstances = new List<ReservationBookInstance>()
                {
                    new ReservationBookInstance()
                    {
                        ReservationID = 1,
                        BookInstanceID = 1
                    },
                }
            };

            var collRefToLoad = new string[]
            {
                nameof(ReservationDTO.BookInstances)
            };

            return new Tuple<Reservation, string[], string[]>(reservation, null, collRefToLoad);
        }

        public Tuple<Reservation, string[], string[]> GetEntriesDetailWithLoadedBooks3()
        {
            var reservation = new Reservation()
            {
                Id = 1,
                EReaderID = 1,
                BookInstances = new List<ReservationBookInstance>()
                {

                }
            };

            var collRefToLoad = new string[]
            {
                nameof(ReservationDTO.BookInstances)
            };

            return new Tuple<Reservation, string[], string[]>(reservation, null, collRefToLoad);
        }

        #endregion

        #region AddBookInstance

        private ReservationFacade SetupAddBookInstance
           (AutoMock mock, Func<Tuple<ReservationDTO, BookInstance, string[], string[], List<ReservationBookInstance>>> dataFunc)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            var data = dataFunc();

            mock.Mock<IQuery<ReservationBookInstance>>()
                .Setup(x => x.Execute().Result)
                .Returns(new QueryResult<ReservationBookInstance>() { Items = data.Item5 });
            var reservBookInstanceQuery = mock.Create<IQuery<ReservationBookInstance>>();

            mock.Mock<IRepository<BookInstance>>()
                .Setup(x => x.GetByID(data.Item2.Id, data.Item3, data.Item4).Result)
                .Returns(data.Item2);
            var bookInstanceRepo = mock.Create<IRepository<BookInstance>>();

            var reservBookInstanceQueryObject = 
                new QueryObject<ReservationBookInstanceDTO, ReservationBookInstance>(_mapper,reservBookInstanceQuery);

            var reservService = new ReservationService(null, _mapper, null, null, reservBookInstanceQueryObject);
            var bookInstService = new CRUDService<BookInstanceDTO, BookInstance>(bookInstanceRepo, _mapper, null);

            var reservationFacade = new ReservationFacade(uow, reservService, bookInstService, null, null);

            return reservationFacade;
        }

        [Fact]
        public async Task AddBookInstance1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var reservationFacade = SetupAddBookInstance(mock, GetEntriesAddBookInstance);

                var data = GetEntriesAddBookInstance();
                await reservationFacade.AddBookInstance(data.Item2.Id, data.Item1);

                Assert.True(mock.Mock<IRepository<BookInstance>>().Invocations
                    .Where(invo => invo.Method.Name == nameof(IRepository<BookInstance>.GetByID)).Count() == 1);

                Assert.Contains(data.Item1.BookInstances, entry => entry.BookInstanceID == data.Item2.Id);
            }
        }

        [Fact]
        public async Task AddBookInstance2()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var reservationFacade = SetupAddBookInstance(mock, GetEntriesAddBookInstance2);

                var data = GetEntriesAddBookInstance2();
                await reservationFacade.AddBookInstance(data.Item2.Id, data.Item1);

                Assert.True(mock.Mock<IRepository<BookInstance>>().Invocations
                    .Where(invo => invo.Method.Name == nameof(IRepository<BookInstance>.GetByID)).Count() == 1);

                Assert.DoesNotContain(data.Item1.BookInstances, entry => entry.BookInstanceID == data.Item2.Id);
            }
        }

        [Fact]
        public async Task AddBookInstance3()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var reservationFacade = SetupAddBookInstance(mock, GetEntriesAddBookInstance3);

                var data = GetEntriesAddBookInstance3();
                await reservationFacade.AddBookInstance(data.Item2.Id, data.Item1);

                Assert.True(mock.Mock<IRepository<BookInstance>>().Invocations
                    .Where(invo => invo.Method.Name == nameof(IRepository<BookInstance>.GetByID)).Count() == 1);

                Assert.Contains(data.Item1.BookInstances, entry => entry.BookInstanceID == data.Item2.Id);
            }
        }

        public Tuple<ReservationDTO, BookInstance, string[], string[], List<ReservationBookInstance>> GetEntriesAddBookInstance()
        {
            var reservation = new ReservationDTO()
            {
                Id = 1,
                DateFrom = new DateTime(2000, 1, 1),
                DateTill = new DateTime(2001, 1, 1),
                BookInstances = new List<ReservationBookInstanceDTO>()
            };

            var bookInstanceToAdd = new BookInstance()
            {
                Id = 3
            };

            var referencesToLoad = new string[]
            {
                nameof(BookInstanceDTO.FromBookTemplate),
                nameof(BookInstanceDTO.Owner)
            };

            var queryResult = new List<ReservationBookInstance>()
            {
                new ReservationBookInstance()
                {
                    Reservation = new Reservation()
                    {
                        DateFrom = new DateTime(1998,1,1),
                        DateTill = new DateTime(1999,1,1) 
                    }
                },
                new ReservationBookInstance()
                {
                    Reservation = new Reservation()
                    {
                        DateFrom = new DateTime(1970,1,1),
                        DateTill = new DateTime(1985,1,1)
                    }
                }
            };

            return new Tuple<ReservationDTO, BookInstance, string[], string[], List<ReservationBookInstance>>
                (reservation,bookInstanceToAdd,referencesToLoad,null, queryResult);
        }

        public Tuple<ReservationDTO, BookInstance, string[], string[], List<ReservationBookInstance>> GetEntriesAddBookInstance2()
        {
            var reservation = new ReservationDTO()
            {
                Id = 1,
                DateFrom = new DateTime(1999, 1, 1),
                DateTill = new DateTime(2001, 1, 1),
                BookInstances = new List<ReservationBookInstanceDTO>()
            };

            var bookInstanceToAdd = new BookInstance()
            {
                Id = 5
            };

            var referencesToLoad = new string[]
            {
                nameof(BookInstanceDTO.FromBookTemplate),
                nameof(BookInstanceDTO.Owner)
            };

            var queryResult = new List<ReservationBookInstance>()
            {
                new ReservationBookInstance()
                {
                    Reservation = new Reservation()
                    {
                        DateFrom = new DateTime(1998,1,1),
                        DateTill = new DateTime(2000,1,1)
                    }
                },
                new ReservationBookInstance()
                {
                    Reservation = new Reservation()
                    {
                        DateFrom = new DateTime(2001,1,1),
                        DateTill = new DateTime(2004,1,1)
                    }
                }
            };

            return new Tuple<ReservationDTO, BookInstance, string[], string[], List<ReservationBookInstance>>
                (reservation, bookInstanceToAdd, referencesToLoad, null, queryResult);
        }

        public Tuple<ReservationDTO, BookInstance, string[], string[], List<ReservationBookInstance>> GetEntriesAddBookInstance3()
        {
            var reservation = new ReservationDTO()
            {
                Id = 1,
                DateFrom = new DateTime(2000, 1, 1),
                DateTill = new DateTime(2001, 1, 1),
                BookInstances = new List<ReservationBookInstanceDTO>()
                {
                    new ReservationBookInstanceDTO()
                    {
                        BookInstanceID = 2
                    },
                    new ReservationBookInstanceDTO()
                    {
                        BookInstanceID = 8
                    }
                }
            };

            var bookInstanceToAdd = new BookInstance()
            {
                Id = 4
            };

            var referencesToLoad = new string[]
            {
                nameof(BookInstanceDTO.FromBookTemplate),
                nameof(BookInstanceDTO.Owner)
            };

            var queryResult = new List<ReservationBookInstance>()
            {
                
            };

            return new Tuple<ReservationDTO, BookInstance, string[], string[], List<ReservationBookInstance>>
                (reservation, bookInstanceToAdd, referencesToLoad, null, queryResult);
        }

        #endregion

        #region AddEReaderInstance

        private ReservationFacade SetupAddEReaderInstance
           (AutoMock mock, Func<Tuple<ReservationDTO, EReaderInstance, string[], string[], List<Reservation>>> dataFunc)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            var data = dataFunc();

            mock.Mock<IQuery<Reservation>>()
                .Setup(x => x.Execute().Result)
                .Returns(new QueryResult<Reservation>() { Items = data.Item5 });
            var reservQuery = mock.Create<IQuery<Reservation>>();

            mock.Mock<IRepository<EReaderInstance>>()
                .Setup(x => x.GetByID(data.Item2.Id, data.Item3, data.Item4).Result)
                .Returns(data.Item2);
            var eReaderRepo = mock.Create<IRepository<EReaderInstance>>();

            var reservQueryObject =
                new QueryObject<ReservationPrevDTO, Reservation>(_mapper, reservQuery);

            var reservService = new ReservationService(null, _mapper, null, reservQueryObject, null);
            var ereaderInstanceService = new CRUDService<EReaderInstanceDTO, EReaderInstance>(eReaderRepo, _mapper, null);

            var reservationFacade = new ReservationFacade(uow, reservService, null, ereaderInstanceService, null);

            return reservationFacade;
        }

        [Fact]
        public async Task AddEReaderInstance1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var reservationFacade = SetupAddEReaderInstance(mock, GetEntriesAddEReaderInstance);

                var data = GetEntriesAddEReaderInstance();
                await reservationFacade.AddEReaderInstance(data.Item2.Id, data.Item1);

                Assert.True(mock.Mock<IRepository<EReaderInstance>>().Invocations
                    .Where(invo => invo.Method.Name == nameof(IRepository<EReaderInstance>.GetByID)).Count() == 1);

                Assert.True(data.Item1.EReaderID == data.Item2.Id);
            }
        }

        [Fact]
        public async Task AddEReaderInstance2()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var reservationFacade = SetupAddEReaderInstance(mock, GetEntriesAddEReaderInstance2);

                var data = GetEntriesAddEReaderInstance2();
                await reservationFacade.AddEReaderInstance(data.Item2.Id, data.Item1);

                Assert.True(mock.Mock<IRepository<EReaderInstance>>().Invocations
                    .Where(invo => invo.Method.Name == nameof(IRepository<EReaderInstance>.GetByID)).Count() == 1);

                Assert.True(data.Item1.EReaderID == data.Item2.Id);
            }
        }

        [Fact]
        public async Task AddEReaderInstance3()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var reservationFacade = SetupAddEReaderInstance(mock, GetEntriesAddEReaderInstance3);

                var data = GetEntriesAddEReaderInstance3();
                await reservationFacade.AddEReaderInstance(data.Item2.Id, data.Item1);

                Assert.True(mock.Mock<IRepository<EReaderInstance>>().Invocations
                    .Where(invo => invo.Method.Name == nameof(IRepository<EReaderInstance>.GetByID)).Count() == 1);

                Assert.True(data.Item1.EReaderID != data.Item2.Id);
            }
        }

        private Tuple<ReservationDTO, EReaderInstance, string[], string[], List<Reservation>> GetEntriesAddEReaderInstance()
        {
            var ereaderToadd = new EReaderInstance()
            {
                Id = 1
            };

            var reservationDto = new ReservationDTO()
            {
                Id = 1,
                DateFrom = new DateTime(2005,1,1),
                DateTill = new DateTime(2006,1,1)
            };

            var referencesToLoad = new string[]
            {
                nameof(EReaderInstanceDTO.Owner),
            };

            var queryResult = new List<Reservation>()
            {
                new Reservation()
                {
                    Id = 1,
                    DateFrom = new DateTime(2000,1,1),
                    DateTill = new DateTime(2001,1,1)
                },
                new Reservation()
                {
                    Id = 1,
                    DateFrom = new DateTime(1999,1,1),
                    DateTill = new DateTime(1999,5,1)
                },
            };

            return new Tuple<ReservationDTO, EReaderInstance, string[], string[], List<Reservation>>
                (reservationDto, ereaderToadd, referencesToLoad, null, queryResult);
        }

        private Tuple<ReservationDTO, EReaderInstance, string[], string[], List<Reservation>> GetEntriesAddEReaderInstance2()
        {
            var ereaderToadd = new EReaderInstance()
            {
                Id = 1
            };

            var reservationDto = new ReservationDTO()
            {
                Id = 1,
                DateFrom = new DateTime(2005, 1, 1),
                DateTill = new DateTime(2006, 1, 1)
            };

            var referencesToLoad = new string[]
            {
                nameof(EReaderInstanceDTO.Owner),
            };

            var queryResult = new List<Reservation>()
            {
                
            };

            return new Tuple<ReservationDTO, EReaderInstance, string[], string[], List<Reservation>>
                (reservationDto, ereaderToadd, referencesToLoad, null, queryResult);
        }

        private Tuple<ReservationDTO, EReaderInstance, string[], string[], List<Reservation>> GetEntriesAddEReaderInstance3()
        {
            var ereaderToadd = new EReaderInstance()
            {
                Id = 1                
            };

            var reservationDto = new ReservationDTO()
            {
                Id = 1,
                EReaderID = 99,
                DateFrom = new DateTime(2000, 1, 1),
                DateTill = new DateTime(2006, 1, 1)
            };

            var referencesToLoad = new string[]
            {
                nameof(EReaderInstanceDTO.Owner),
            };

            var queryResult = new List<Reservation>()
            {
                new Reservation()
                {
                    Id = 1,
                    DateFrom = new DateTime(2000,1,1),
                    DateTill = new DateTime(2001,1,1)
                },
                new Reservation()
                {
                    Id = 1,
                    DateFrom = new DateTime(1999,1,1),
                    DateTill = new DateTime(1999,5,1)
                },
            };

            return new Tuple<ReservationDTO, EReaderInstance, string[], string[], List<Reservation>>
                (reservationDto, ereaderToadd, referencesToLoad, null, queryResult);
        }
        #endregion
    }
}
