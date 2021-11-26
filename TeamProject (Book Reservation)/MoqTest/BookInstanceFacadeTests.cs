using Autofac.Extras.Moq;
using AutoMapper;
using BL.Config;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.Reservation;
using BL.Facades;
using BL.QueryObjects;
using BL.Services;
using BL.Services.Implementations;
using DAL;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;
using Infrastructure.Query;
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

        private BookInstanceFacade Setup(AutoMock mock)
        { 
            var uow = mock.Mock<IUnitOfWork>().Object;
            var bookInstanceRepo = mock.Mock<IRepository<BookInstance>>().Object;
            var reservationRepo = mock.Mock<IRepository<Reservation>>().Object;
            
            mock.Mock<IQuery<ReservationBookInstance>>()
                .Setup(x => x.Execute())
                .Returns(GetTestEntries());

            var resBookQuery = mock.Create<IQuery<ReservationBookInstance>>();

            mock.Mock<IQuery<ReservationBookInstance>>();

            var quer1 = new QueryObject<ReservationPrevDTO, Reservation>(_mapper, null);
            var quer2 = new QueryObject<ReservationBookInstanceDTO, ReservationBookInstance>(_mapper, resBookQuery);

            var reservationService = new ReservationService(reservationRepo,_mapper, quer1, quer2);
            var bookColectionBookService = new CRUDService<BookInstanceDTO, BookInstance>(bookInstanceRepo, _mapper);

            var bookCollectionFacade = new BookInstanceFacade(uow, bookColectionBookService, reservationService);
            return bookCollectionFacade;
        }

        [Fact]
        public void GetBookReservationPrevsByUserTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                Test(mock, new DateTime(1895, 1, 1), new DateTime(2010, 1, 1));
                Test(mock, new DateTime(2005, 1, 1), new DateTime(2010, 1, 1));
                Test(mock, new DateTime(1999, 1, 1), new DateTime(2065, 1, 1));
            }
        }

        private void Test(AutoMock mock, DateTime dateFrom, DateTime dateTill)
        {
            BookInstanceFacade bookCollectionFacade = Setup(mock);

            var result = bookCollectionFacade.GetBookReservationPrevsByUser(1, dateFrom, dateTill);

            Assert.True(mock.Mock<IRepository<Reservation>>().Invocations.Count == 0 &&
                mock.Mock<IRepository<BookInstance>>().Invocations.Count == 0);

            Assert.True(result.Result.Count() == GetValidCount(GetTestEntries().Result.Items, dateFrom, dateTill));
        }

        private int GetValidCount(IEnumerable<ReservationBookInstance> data, DateTime from, DateTime till)
        {
            var count = data.Where(x => x.Reservation.DateFrom >= from && x.Reservation.DateTill <= till).Count();
            return count;
        }

        private async Task<QueryResult<ReservationBookInstance>> GetTestEntries()
        {
            var result = new QueryResult<ReservationBookInstance>();

            result.Items = new List<ReservationBookInstance>()
            {
                new ReservationBookInstance() 
                { 
                    ReservationID = 1, 
                    Reservation = new Reservation { Id = 1, DateFrom = new DateTime(2012,9,10), DateTill = new DateTime(2055,9,8) } , 
                    BookInstanceID = 1 
                },
                new ReservationBookInstance() {
                    ReservationID = 2,
                    Reservation = new Reservation { Id = 2, DateFrom = new DateTime(251,9,10), DateTill = new DateTime(2015,9,8) } ,
                    BookInstanceID = 1
                },
                new ReservationBookInstance() {
                    ReservationID = 3,
                    Reservation = new Reservation { Id = 3, DateFrom = new DateTime(2002,9,10), DateTill = new DateTime(2003,9,8) } ,
                    BookInstanceID = 1
                },
                new ReservationBookInstance() {
                    ReservationID = 3,
                    Reservation = new Reservation { Id = 3, DateFrom = new DateTime(2000,9,10), DateTill = new DateTime(2008,9,8) } ,
                    BookInstanceID = 1
                },
                new ReservationBookInstance() {
                    ReservationID = 3,
                    Reservation = new Reservation { Id = 3, DateFrom = new DateTime(1999,9,10), DateTill = new DateTime(2006,9,8) } ,
                    BookInstanceID = 1
                }
            };

            return result;
        }
    }
}
