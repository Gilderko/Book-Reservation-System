using Autofac.Extras.Moq;
using AutoMapper;
using BL.Config;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;
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
    public class BookCollectionFacadeTests
    {
        private IMapper _mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

        private BookCollectionFacade Setup(AutoMock mock)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;
            var bookColectionRepo = mock.Mock<IRepository<BookCollection>>().Object;
            var bookCollectionBookRepo = mock.Mock<IRepository<BookCollectionBook>>().Object;

            var bookCollectionService = new CRUDService<BookCollectionDTO, BookCollection>(bookColectionRepo, _mapper);
            var bookColectionBookService = new CRUDService<BookCollectionBookDTO, BookCollectionBook>
                (bookCollectionBookRepo, _mapper);

            var bookCollectionFacade = new BookCollectionFacade(uow, bookCollectionService, bookColectionBookService);
            return bookCollectionFacade;
        }

        [Fact]
        public void AddBookToCollectionTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                BookCollectionFacade bookCollectionFacade = Setup(mock);

                int counter = 0;
                foreach (var data in GetEntries())
                {
                    bookCollectionFacade.AddBookToCollection(data.Item1, data.Item2);
                    counter++;

                    Assert.True(mock.Mock<IRepository<BookCollectionBook>>().Invocations.Count == counter);

                    var calledMethod = mock.Mock<IRepository<BookCollectionBook>>()
                        .Invocations.Last((IInvocation x) => x.Method.Name == "Insert");

                    Assert.True(calledMethod != null);

                    var argument = (calledMethod.Arguments.First() as BookCollectionBook);

                    Assert.True(argument != null);

                    Assert.True(argument.BookCollectionID == data.Item1.Id && argument.BookID == data.Item2.Id);
                }
            }
        }        

        [Fact]
        public void DeleteBookFromCollectionTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                BookCollectionFacade bookCollectionFacade = Setup(mock);

                int counter = 0;
                foreach (var tupleValue in GetEntries())
                {
                    bookCollectionFacade.DeleteBookFromCollection(tupleValue.Item1, tupleValue.Item2);
                    counter++;

                    Assert.True(mock.Mock<IRepository<BookCollectionBook>>().Invocations.Count == counter);

                    var calledMethod = mock.Mock<IRepository<BookCollectionBook>>()
                        .Invocations.Last((IInvocation x) => x.Method.Name == "Delete");

                    Assert.True(calledMethod != null);

                    var argument = (calledMethod.Arguments.First() as BookCollectionBook);

                    Assert.True(argument != null);

                    Assert.True(argument.BookCollectionID == tupleValue.Item1.Id && argument.BookID == tupleValue.Item2.Id);
                }
            }
        }

        public IEnumerable<Tuple<BookCollectionDTO, BookDTO>> GetEntries()
        {
            var book1 = new BookDTO { Id = 1 };
            var book2 = new BookDTO { Id = 2 };
            var book3 = new BookDTO { Id = 3 };
            var book4 = new BookDTO { Id = 4 };
            var book5 = new BookDTO { Id = 5 };

            var bookCollection1 = new BookCollectionDTO { Id = 1 };
            var bookCollection2 = new BookCollectionDTO { Id = 2 };
            var bookCollection3 = new BookCollectionDTO { Id = 3 };

            yield return new Tuple<BookCollectionDTO, BookDTO>(bookCollection1, book1);
            yield return new Tuple<BookCollectionDTO, BookDTO>(bookCollection2, book2);
            yield return new Tuple<BookCollectionDTO, BookDTO>(bookCollection1, book3);
            yield return new Tuple<BookCollectionDTO, BookDTO>(bookCollection3, book5);
            yield return new Tuple<BookCollectionDTO, BookDTO>(bookCollection1, book4);
        }
    }
}
