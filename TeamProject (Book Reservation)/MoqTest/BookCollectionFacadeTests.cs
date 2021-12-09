using Autofac.Extras.Moq;
using AutoMapper;
using BL.Config;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;
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
    public class BookCollectionFacadeTests
    {
        private IMapper _mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

        private BookCollectionFacade Setup(AutoMock mock, Func<Tuple<BookCollection, QueryResult<Book>>> result)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            mock.Mock<IQuery<Book>>()
                .Setup(x => x.Execute().Result)
                .Returns(result().Item2);

            var bookQuery = mock.Create<IQuery<Book>>();           

            var authorRepo = mock.Mock<IRepository<Author>>().Object;

            var bookPrevQuery = new QueryObject<BookPrevDTO, Book>(_mapper, bookQuery);

            var bookPrevService = new CRUDService<BookPrevDTO, Book>(null, _mapper, bookPrevQuery);
            var authorService = new AuthorService(authorRepo, _mapper, null, null);

            var bookCollectionFacade = new BookCollectionFacade(uow, null, null, authorService, bookPrevService);
            return bookCollectionFacade;
        }

        [Fact]
        public async Task GetBookPreviewsWithAuthForCollectionTest1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                BookCollectionFacade bookCollectionFacade = Setup(mock, GetEntries);

                var data = GetEntries();
                int authorRepoCount = data.Item2.Items.Aggregate(0, (x, y) => y.Authors.Count() + x);

                var result = await bookCollectionFacade.GetBookPreviewsWithAuthorsForCollection(_mapper.Map<BookCollectionDTO>(data.Item1));

                Assert.True(result.Count() == data.Item2.Items.Count());

                Assert.True(mock.Mock<IRepository<Author>>().Invocations.Count == authorRepoCount);               
            }
        }

        [Fact]
        public async Task GetBookPreviewsWithAuthForCollectionTest2()
        {
            using (var mock = AutoMock.GetLoose())
            {
                BookCollectionFacade bookCollectionFacade = Setup(mock, GetEntries2);

                var data = GetEntries2();
                int authorRepoCount = data.Item2.Items.Aggregate(0, (x, y) => y.Authors.Count() + x);

                var result = await bookCollectionFacade.GetBookPreviewsWithAuthorsForCollection(_mapper.Map<BookCollectionDTO>(data.Item1));

                Assert.True(result.Count() == data.Item2.Items.Count());

                Assert.True(mock.Mock<IRepository<Author>>().Invocations.Count == authorRepoCount);
            }
        }

        [Fact]
        public async Task GetBookPreviewsWithAuthForCollectionTest3()
        {
            using (var mock = AutoMock.GetLoose())
            {
                BookCollectionFacade bookCollectionFacade = Setup(mock, GetEntries3);

                var data = GetEntries3();
                int authorRepoCount = data.Item2.Items.Aggregate(0, (x, y) => y.Authors.Count() + x);

                var result = await bookCollectionFacade.GetBookPreviewsWithAuthorsForCollection(_mapper.Map<BookCollectionDTO>(data.Item1));

                Assert.True(result.Count() == data.Item2.Items.Count());

                Assert.True(mock.Mock<IRepository<Author>>().Invocations.Count == authorRepoCount);
            }
        }

        public Tuple<BookCollection, QueryResult<Book>> GetEntries()
        {
            var book1 = new Book()
            {
                Id = 1,
            };
            var book2 = new Book()
            {
                Id = 2
            };

            var auth1 = new Author()
            {
                Id = 1
            };
            var auth2 = new Author()
            {
                Id = 2
            };
            var auth3 = new Author()
            {
                Id = 3
            };

            book1.Authors = new List<AuthorBook>()
            {
                new AuthorBook()
                {
                    BookID = book1.Id,
                    AuthorID = auth1.Id,
                },
                new AuthorBook()
                {
                    BookID = book1.Id,
                    AuthorID = auth2.Id
                }
            };

            book2.Authors = new List<AuthorBook>()
            {
                new AuthorBook()
                {
                    BookID = book2.Id,
                    AuthorID = auth2.Id,
                },
                new AuthorBook()
                {
                    BookID = book2.Id,
                    AuthorID = auth3.Id
                }
            };

            var bookCollection1 = new BookCollection { Id = 1 };

            bookCollection1.Books = new List<BookCollectionBook>
            {
                new BookCollectionBook
                {
                    BookCollectionID = bookCollection1.Id,
                    BookID = book1.Id,
                },
                new BookCollectionBook
                {
                    BookCollectionID = bookCollection1.Id,
                    BookID = book2.Id,
                },
            };

            var querRes = new List<Book>()
            {
                new Book() { Id = book1.Id, Authors = book1.Authors.ToList() },
                new Book() { Id = book2.Id, Authors = book2.Authors.ToList() },
            };

            return new Tuple<BookCollection, QueryResult<Book>>(bookCollection1, new QueryResult<Book>() { Items = querRes });
        }

        public Tuple<BookCollection, QueryResult<Book>> GetEntries2()
        {
            var book2 = new Book()
            {
                Id = 2
            };
            var book3 = new Book()
            {
                Id = 3
            };
            var book4 = new Book()
            {
                Id = 4
            };

            var auth1 = new Author()
            {
                Id = 1
            };
            var auth2 = new Author()
            {
                Id = 2
            };
            var auth3 = new Author()
            {
                Id = 3
            };

            book2.Authors = new List<AuthorBook>()
            {
                new AuthorBook()
                {
                    BookID = book2.Id,
                    AuthorID = auth1.Id,
                },
                new AuthorBook()
                {
                    BookID = book2.Id,
                    AuthorID = auth2.Id
                }
            };

            book3.Authors = new List<AuthorBook>()
            {
                new AuthorBook()
                {
                    BookID = book3.Id,
                    AuthorID = auth2.Id,
                },
                new AuthorBook()
                {
                    BookID = book3.Id,
                    AuthorID = auth3.Id
                }
            };

            book4.Authors = new List<AuthorBook>()
            {
                new AuthorBook()
                {
                    BookID = book4.Id,
                    AuthorID = auth3.Id,
                },
            };

            var bookCollection2 = new BookCollection { Id = 2 };

            bookCollection2.Books = new List<BookCollectionBook>
            {
                new BookCollectionBook
                {
                    BookCollectionID = bookCollection2.Id,
                    BookID = book2.Id
                },
                new BookCollectionBook
                {
                    BookCollectionID = bookCollection2.Id,
                    BookID = book3.Id
                },
                new BookCollectionBook
                {
                    BookCollectionID = bookCollection2.Id,
                    BookID = book4.Id
                },
            };

            var querRes = new List<Book>()
            {
                new Book() { Id = book2.Id, Authors = book2.Authors.ToList() },
                new Book() { Id = book3.Id, Authors = book3.Authors.ToList() },
                new Book() { Id = book4.Id, Authors = book4.Authors.ToList() }
            };

            return new Tuple<BookCollection, QueryResult<Book>>(bookCollection2, new QueryResult<Book>() { Items = querRes });
        }

        public Tuple<BookCollection, QueryResult<Book>> GetEntries3()
        {
            var bookCollection3 = new BookCollection { Id = 3 };

            bookCollection3.Books = new List<BookCollectionBook>();

            return new Tuple<BookCollection, QueryResult<Book>>(bookCollection3, new QueryResult<Book>() { Items = new List<Book>() });
        }
    }
}
