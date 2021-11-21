using Autofac.Extras.Moq;
using AutoMapper;
using BL.Config;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EBook;
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
            var eBookEReaderInstanceRepo = mock.Mock<IRepository<EBookEReaderInstance>>().Object;

            mock.Mock<IQuery<Reservation>>()
               .Setup(x => x.Execute())
               .Returns(GetReservationEntries());

            var resBookQuery = mock.Create<IQuery<Reservation>>();

            var quer1 = new QueryObject<ReservationPrevDTO, Reservation>(_mapper, resBookQuery);
            var quer2 = new QueryObject<ReservationBookInstanceDTO, ReservationBookInstance>(_mapper, null);

            var reservationService = new ReservationService(reservationRepo, _mapper, quer1, quer2);
            var eReaderInstanceService = new CRUDService<EReaderInstanceDTO, EReaderInstance>(eReaderInstanceRepo, _mapper);
            var eBookEReaderInstanceService = new CRUDService<EBookEReaderInstanceDTO, EBookEReaderInstance>(eBookEReaderInstanceRepo, _mapper);

            var eReaderInstanceFacade = new EReaderInstanceFacade(uow, eReaderInstanceService, eBookEReaderInstanceService, reservationService);
            return eReaderInstanceFacade;
        }

        [Fact]
        public void TestEReaderInstanceFacade()
        {
            using (var mock = AutoMock.GetLoose())
            {
                TestQuery(mock, new DateTime(1895, 1, 1), new DateTime(2010, 1, 1), 1);
                TestAddEBook(mock);
                TestDeleteEBook(mock);
            }
        }

        private void TestQuery(AutoMock mock, DateTime dateFrom, DateTime dateTill, int eReaderId)
        {
            EReaderInstanceFacade eReaderInstanceFacade = Setup(mock);

            var result = eReaderInstanceFacade.GetReservationPrevsByDate(new EReaderInstanceDTO { Id = eReaderId }, dateFrom, dateTill);

            Assert.True(result != null);

            Assert.True(result.Count() == GetReservationEntries().Items.Count());
        }

        private void TestAddEBook(AutoMock mock)
        {
            EReaderInstanceFacade eReaderInstanceFacade = Setup(mock);

            var counter = 0;
            foreach (var data in GetNewReservations())
            {
                eReaderInstanceFacade.AddEBook(data.Item1, data.Item2);
                counter++;

                Assert.True(mock.Mock<IRepository<EBookEReaderInstance>>().Invocations
                    .Where(x => x.Method.Name == nameof(IRepository<EBookEReaderInstance>.Insert)).Count() == counter);

                var calledMethod = mock.Mock<IRepository<EBookEReaderInstance>>()
                    .Invocations.Last((IInvocation x) => x.Method.Name == nameof(IRepository<EBookEReaderInstance>.Insert));

                Assert.True(calledMethod != null); ;

                var argument = (calledMethod.Arguments.First() as EBookEReaderInstance);

                Assert.True(argument != null);

                Assert.True(argument.EReaderInstanceID == data.Item1.Id && argument.EBookID == data.Item2.Id);
            }

        }

        private void TestDeleteEBook(AutoMock mock)
        {
            EReaderInstanceFacade eReaderInstanceFacade = Setup(mock);

            var counter = 0;
            foreach (var data in GetNewReservations())
            {
                eReaderInstanceFacade.DeleteEBook(data.Item1,data.Item2);
                counter++;

                Assert.True(mock.Mock<IRepository<EBookEReaderInstance>>().Invocations
                    .Where(x => x.Method.Name == nameof(IRepository<EBookEReaderInstance>.Delete)).Count() == counter);

                var calledMethod = mock.Mock<IRepository<EBookEReaderInstance>>()
                    .Invocations.Last((IInvocation x) => x.Method.Name == nameof(IRepository<EBookEReaderInstance>.Delete));

                Assert.True(calledMethod != null); ;

                var argument = (calledMethod.Arguments.First() as EBookEReaderInstance);

                Assert.True(argument != null);

                Assert.True(argument.EReaderInstanceID == data.Item1.Id && argument.EBookID == data.Item2.Id);
            }            
        }

        private IEnumerable<Tuple<EReaderInstanceDTO,EBookDTO>> GetNewReservations()
        {
            var book1 = new EBookDTO() { Id = 1 };
            var book2 = new EBookDTO() { Id = 2 };
            var book3 = new EBookDTO() { Id = 3 };
            var book4 = new EBookDTO() { Id = 4 };

            var eReaderInst1 = new EReaderInstanceDTO() { Id = 1 };
            var eReaderInst2 = new EReaderInstanceDTO() { Id = 2 };

            yield return new Tuple<EReaderInstanceDTO, EBookDTO>(eReaderInst1, book1);
            yield return new Tuple<EReaderInstanceDTO, EBookDTO>(eReaderInst2, book2);
            yield return new Tuple<EReaderInstanceDTO, EBookDTO>(eReaderInst1, book3);
            yield return new Tuple<EReaderInstanceDTO, EBookDTO>(eReaderInst2, book4);
            yield return new Tuple<EReaderInstanceDTO, EBookDTO>(eReaderInst1, book4);
        }

        public QueryResult<Reservation> GetReservationEntries()
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
