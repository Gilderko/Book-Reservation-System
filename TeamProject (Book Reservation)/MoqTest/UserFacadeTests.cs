using Autofac.Extras.Moq;
using AutoMapper;
using BL.Config;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.User;
using BL.Facades;
using BL.Services;
using BL.Services.Implementations;
using DAL;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace MoqTest
{   
    public class UserFacadeTests
    {
        private IMapper _mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

        private UserFacade Setup(AutoMock mock)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            var userRepo = mock.Mock<IRepository<User>>().Object;
            var bookCollectionRepo = mock.Mock<IRepository<BookCollection>>().Object;
            var bookInstanceRepo = mock.Mock<IRepository<BookInstance>>().Object;
            var eReaderInstanceRepo = mock.Mock<IRepository<EReaderInstance>>().Object;

            var userService = new UserService(userRepo, _mapper);
            var bookCollectionService = new CRUDService<BookCollectionDTO, BookCollection>(bookCollectionRepo, _mapper);
            var bookInstanceService = new CRUDService<BookInstanceDTO, BookInstance>(bookInstanceRepo, _mapper);
            var EReaderInstanceService = new CRUDService<EReaderInstanceDTO, EReaderInstance>(eReaderInstanceRepo, _mapper);

            var userFacade = new UserFacade(uow, userService, bookCollectionService, bookInstanceService, EReaderInstanceService);

            return userFacade;
        }

        [Fact]
        public void AddBookCollectionTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var facade = Setup(mock);

                int counter = 0;
                foreach (var data in GetBookCollectEntries())
                {
                    facade.AddBookCollection(data.Item1, data.Item2);
                    counter++;

                    Assert.True(mock.Mock<IRepository<BookCollection>>().Invocations
                    .Where(x => x.Method.Name == nameof(IRepository<BookCollection>.Insert)).Count() == counter);

                    var calledMethod = mock.Mock<IRepository<BookCollection>>()
                        .Invocations.Last((IInvocation x) => x.Method.Name == nameof(IRepository<BookCollection>.Insert));

                    Assert.True(calledMethod != null); ;

                    var argument = (calledMethod.Arguments.First() as BookCollection);

                    Assert.True(argument != null);

                    Assert.True(argument.UserId == data.Item1 && argument.Id == data.Item2.Id);
                }
            }
        }

        private IEnumerable<Tuple<int,BookCollectionDTO>> GetBookCollectEntries()
        {
            yield return new Tuple<int, BookCollectionDTO>(1, new BookCollectionDTO { Id = 1 });
            yield return new Tuple<int, BookCollectionDTO>(2, new BookCollectionDTO { Id = 2 });
            yield return new Tuple<int, BookCollectionDTO>(3, new BookCollectionDTO { Id = 3 });
            yield return new Tuple<int, BookCollectionDTO>(4, new BookCollectionDTO { Id = 4 });
        }

        [Fact]
        public void AddBookInstanceTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var facade = Setup(mock);

                int counter = 0;
                foreach (var data in GetBookInstanceEntries())
                {
                    facade.AddBookInstance(data.Item1, data.Item2);
                    counter++;

                    Assert.True(mock.Mock<IRepository<BookInstance>>().Invocations
                    .Where(x => x.Method.Name == nameof(IRepository<BookInstance>.Insert)).Count() == counter);

                    var calledMethod = mock.Mock<IRepository<BookInstance>>()
                        .Invocations.Last((IInvocation x) => x.Method.Name == nameof(IRepository<BookInstance>.Insert));

                    Assert.True(calledMethod != null); ;

                    var argument = (calledMethod.Arguments.First() as BookInstance);

                    Assert.True(argument != null);

                    Assert.True(argument.BookOwnerId == data.Item1 && argument.Id == data.Item2.Id);
                }
            }
        }

        private IEnumerable<Tuple<int, BookInstanceDTO>> GetBookInstanceEntries()
        {
            yield return new Tuple<int, BookInstanceDTO>(1, new BookInstanceDTO { Id = 1 });
            yield return new Tuple<int, BookInstanceDTO>(2, new BookInstanceDTO { Id = 2 });
            yield return new Tuple<int, BookInstanceDTO>(3, new BookInstanceDTO { Id = 3 });
            yield return new Tuple<int, BookInstanceDTO>(4, new BookInstanceDTO { Id = 4 });
        }


        [Fact]
        public void AddEreaderInstanceTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var facade = Setup(mock);

                int counter = 0;
                foreach (var data in GetEReaderInstanceEntries())
                {
                    facade.AddEreaderInstance(data.Item1, data.Item2);
                    counter++;

                    Assert.True(mock.Mock<IRepository<EReaderInstance>>().Invocations
                    .Where(x => x.Method.Name == nameof(IRepository<EReaderInstance>.Insert)).Count() == counter);

                    var calledMethod = mock.Mock<IRepository<EReaderInstance>>()
                        .Invocations.Last((IInvocation x) => x.Method.Name == nameof(IRepository<EReaderInstance>.Insert));

                    Assert.True(calledMethod != null); ;

                    var argument = (calledMethod.Arguments.First() as EReaderInstance);

                    Assert.True(argument != null);

                    Assert.True(argument.EreaderOwnerId == data.Item1 && argument.Id == data.Item2.Id);
                }
            }
        }

        private IEnumerable<Tuple<int, EReaderInstanceDTO>> GetEReaderInstanceEntries()
        {
            yield return new Tuple<int, EReaderInstanceDTO>(1, new EReaderInstanceDTO { Id = 1 });
            yield return new Tuple<int, EReaderInstanceDTO>(2, new EReaderInstanceDTO { Id = 2 });
            yield return new Tuple<int, EReaderInstanceDTO>(3, new EReaderInstanceDTO { Id = 3 });
            yield return new Tuple<int, EReaderInstanceDTO>(4, new EReaderInstanceDTO { Id = 4 });
        }


    }
}
