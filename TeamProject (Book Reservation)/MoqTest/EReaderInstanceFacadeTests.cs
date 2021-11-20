using Autofac.Extras.Moq;
using AutoMapper;
using BL.Config;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EReaderInstance;
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
using Xunit;


namespace MoqTest
{   
    public class EReaderInstanceFacadeTests
    {
        private IMapper _mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

        private EReaderInstanceFacade Setup(AutoMock mock)
        { 
            var uow = mock.Mock<IUnitOfWork>().Object;
            var eReaderInstanceRepo = mock.Mock<IRepository<EReaderInstance>>().Object;
            var reservationRepo = mock.Mock<IRepository<Reservation>>().Object;

            mock.Mock<IQuery<Reservation>>()
               .Setup(x => x.Execute())
               .Returns(GetEntries());

            var resBookQuery = mock.Create<IQuery<Reservation>>();

            var quer1 = new QueryObject<ReservationPrevDTO, Reservation>(_mapper, resBookQuery);
            var quer2 = new QueryObject<ReservationBookInstanceDTO, ReservationBookInstance>(_mapper, null);

            var reservationService = new ReservationService(reservationRepo, _mapper, quer1, quer2);
            var eReaderInstanceService = new EReaderInstanceService(eReaderInstanceRepo, _mapper, null);

            var eReaderInstanceFacade = new EReaderInstanceFacade(uow, eReaderInstanceService, reservationService);
            return eReaderInstanceFacade;
        }

        [Fact]
        public void GetBookReservationPrevsByEReader()
        {
            using (var mock = AutoMock.GetLoose())
            {
                Test(mock, new DateTime(1895, 1, 1), new DateTime(2010, 1, 1), 1);
            }
        }

        private void Test(AutoMock mock, DateTime dateFrom, DateTime dateTill, int eReaderId)
        {
            EReaderInstanceFacade eReaderInstanceFacade = Setup(mock);

            var result = eReaderInstanceFacade.GetReservationPrevsByDate(new EReaderInstanceDTO { Id = eReaderId }, dateFrom, dateTill);

            Assert.True(result != null);

            Assert.True(result.Count() == GetEntries().Items.Count());
        }

        private void TestAddEBook()
        {

        }

        private void TestDeleteEBook()
        {

        }


        public QueryResult<Reservation> GetEntries()
        {
            var result = new QueryResult<Reservation>();
            result.Items = new List<Reservation>()
            {
                new Reservation { Id = 1, DateFrom = new DateTime(256,9,10), DateTill = new DateTime(1875,9,8), EReaderID = 1 },
                new Reservation { Id = 2, DateFrom = new DateTime(2000,9,10), DateTill = new DateTime(2003,9,8), EReaderID = 4 },
                new Reservation { Id = 3, DateFrom = new DateTime(2012,9,10), DateTill = new DateTime(2017,9,8), EReaderID = 3 },
                new Reservation { Id = 4, DateFrom = new DateTime(1999,9,10), DateTill = new DateTime(2005,9,8), EReaderID = 2 },
                new Reservation { Id = 5, DateFrom = new DateTime(2015,9,10), DateTill = new DateTime(2018,9,8), EReaderID = 4 },
                new Reservation { Id = 6, DateFrom = new DateTime(2003,9,10), DateTill = new DateTime(2007,9,8), EReaderID = 5 },
                new Reservation { Id = 7, DateFrom = new DateTime(2001,9,10), DateTill = new DateTime(2004,9,8), EReaderID = 3 },
                new Reservation { Id = 8, DateFrom = new DateTime(2565,9,10), DateTill = new DateTime(2565,10,8), EReaderID = 2 },
            };

            return result;
        }
    }
}
