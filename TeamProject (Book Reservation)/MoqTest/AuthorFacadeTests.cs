using Autofac.Extras.Moq;
using AutoMapper;
using BL.Config;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
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
    public class AuthorFacadeTests
    {
        private IMapper _mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

        private AuthorFacade Setup(AutoMock mock)
        {
            var uow = mock.Mock<IUnitOfWork>();
            var authRepo = mock.Mock<IRepository<Author>>();
            var authBookRepo = mock.Mock<IRepository<AuthorBook>>();

            var authService = new CRUDService<AuthorDTO, Author>(authRepo.Object, _mapper);
            var authBookService = new CRUDService<AuthorBookDTO, AuthorBook>(authBookRepo.Object, _mapper);

            var autFac = new AuthorFacade(uow.Object, authService, authBookService);
            return autFac;
        }

        [Fact]
        public void AddBookToAuthorTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                AuthorFacade autFac = Setup(mock);

                int counter = 0;
                foreach (var tupleVal in GetEntries())
                {
                    autFac.AddBookToAuthor(tupleVal.Item1, tupleVal.Item2);
                    counter++;

                    Assert.True(mock.Mock<IRepository<AuthorBook>>().Invocations.Count == counter);

                    var calledMethod = mock.Mock<IRepository<AuthorBook>>()
                        .Invocations.Last((IInvocation x) => x.Method.Name == "Insert");

                    Assert.True(calledMethod != null); ;

                    var argument = (calledMethod.Arguments.First() as AuthorBook);

                    Assert.True(argument != null);

                    Assert.True(argument.AuthorID == tupleVal.Item1.Id && argument.BookID == tupleVal.Item2.Id);
                }
            }
        }

        public IEnumerable<Tuple<AuthorDTO, BookDTO>> GetEntries()
        {
            var book1 = new BookDTO { Id = 1 };
            var book2 = new BookDTO { Id = 2 };
            var book3 = new BookDTO { Id = 3 };
            var book4 = new BookDTO { Id = 4 };
            var book5 = new BookDTO { Id = 5 };

            var auth1 = new AuthorDTO { Id = 1 };
            var auth2 = new AuthorDTO { Id = 2 };
            var auth3 = new AuthorDTO { Id = 3 };

            yield return new Tuple<AuthorDTO, BookDTO>(auth1, book1);
            yield return new Tuple<AuthorDTO, BookDTO>(auth1, book2);
            yield return new Tuple<AuthorDTO, BookDTO>(auth2, book3);
            yield return new Tuple<AuthorDTO, BookDTO>(auth3, book5);
            yield return new Tuple<AuthorDTO, BookDTO>(auth2, book4);
        }
    }
}
