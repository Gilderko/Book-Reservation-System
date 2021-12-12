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
    public class EBookFacadeTests
    {
        private IMapper _mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

        private EBookFacade Setup(AutoMock mock)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            var authorRepo = mock.Mock<IRepository<Author>>().Object;

            mock.Mock<IQuery<Author>>().Setup(x => x.Execute().Result)
                .Returns(new QueryResult<Author>() { Items = new List<Author>() });
            var authorQuery = mock.Create<IQuery<Author>>();

            mock.Mock<IQuery<Genre>>().Setup(x => x.Execute().Result)
                .Returns(new QueryResult<Genre>() { Items = new List<Genre>() });
            var genreQuery = mock.Create<IQuery<Genre>>();

            mock.Mock<IQuery<EBook>>().Setup(x => x.Execute().Result)
                .Returns(new QueryResult<EBook>() { Items = new List<EBook>() });
            var eBookQuery = mock.Create<IQuery<EBook>>();

            var authorQueryObject = new QueryObject<AuthorDTO, Author>(_mapper, authorQuery);
            var authorService = new AuthorService(authorRepo, _mapper, authorQueryObject, null);

            var genreQueryObject = new QueryObject<GenreDTO, Genre>(_mapper, genreQuery);
            var genreService = new GenreService(null, _mapper, genreQueryObject);

            var eBookQueryObject = new QueryObject<EBookPrevDTO, EBook>(_mapper, eBookQuery);
            var eBookService = new CRUDService<EBookPrevDTO, EBook>(null, _mapper, eBookQueryObject);

            var eBookFacade = new EBookFacade(uow, null, authorService, genreService, eBookService);

            return eBookFacade;
        }

        [Fact]
        public async Task GetBookPreviewsComplex()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookCollectionFacade = Setup(mock);

                var data = GetEntries();

                var result = await bookCollectionFacade.GetBookPreviews(null,null,data.Item1, data.Item2, data.Item3, data.Item4, data.Item5, data.Item6, data.Item7,
                data.Rest.Item1, data.Rest.Item2, data.Rest.Item3);

                AuthorInvocationsInclude(mock, data.Item2);
                AuthorInvocationsInclude(mock, data.Item3);

                GenreInvocationsInclude(mock, data.Item4);

                EBookInvocationsInclude(mock, data.Item1);
                EBookInvocationsInclude(mock, (int)data.Item5);
                EBookInvocationsInclude(mock, data.Item6);
                EBookInvocationsInclude(mock, data.Item7);
                EBookInvocationsInclude(mock, data.Rest.Item1);
                EBookInvocationsInclude(mock, data.Rest.Item2);
                EBookInvocationsInclude(mock, (int)data.Rest.Item3);

                Assert.True(mock.Mock<IQuery<EBook>>()
                    .Invocations.Where(invo => invo.Method.Name == nameof(IQuery<EBook>.Execute)).Count() == 1);

                Assert.True(mock.Mock<IQuery<Author>>()
                    .Invocations.Where(invo => invo.Method.Name == nameof(IQuery<Author>.Execute)).Count() == 1);

                Assert.True(mock.Mock<IQuery<Genre>>()
                    .Invocations.Where(invo => invo.Method.Name == nameof(IQuery<Genre>.Execute)).Count() == 1);

                Assert.True(mock.Mock<IRepository<Author>>()
                    .Invocations.Count() == 0);
            }
        }

        private static void AuthorInvocationsInclude(AutoMock mock, object argument)
        {
            var predicates = mock.Mock<IQuery<Author>>().Invocations
                                .Where(invo => invo.Method.Name == nameof(IQuery<EReader>.Where))
                                .Where(invo => invo.Arguments[0] is CompositePredicate)
                                .Select(invo => invo.Arguments[0] as CompositePredicate).First();

            Assert.Contains(predicates.Predicates, pred => (pred as SimplePredicate).ComparedValue.Equals(argument));                               
        }

        private static void GenreInvocationsInclude(AutoMock mock, GenreTypeDTO[] argument)
        {
            var predicate = mock.Mock<IQuery<Genre>>().Invocations
                                .Where(invo => invo.Method.Name == nameof(IQuery<EReader>.Where))
                                .Where(invo => invo.Arguments[0] is SimplePredicate)
                                .First().Arguments[0];

            var compare = argument.ToList();
            compare.Sort();

            var values = ((predicate as SimplePredicate).ComparedValue as GenreTypeDTO[]).ToList();
            values.Sort();
            for (int i = 0; i < compare.Count(); i++)
            {
                Assert.True(compare[i] == values[i]);
            }
        }

        private static void EBookInvocationsInclude(AutoMock mock, object argument)
        {
            var predicates = mock.Mock<IQuery<EBook>>().Invocations
                                .Where(invo => invo.Method.Name == nameof(IQuery<EReader>.Where))
                                .Where(invo => invo.Arguments[0] is CompositePredicate)
                                .Select(invo => invo.Arguments[0] as CompositePredicate).First();

            Assert.Contains(predicates.Predicates, pred => (pred as SimplePredicate).ComparedValue.Equals(argument));
        }


        public Tuple<string, string, string, GenreTypeDTO[], LanguageDTO?,int?,int?, Tuple<DateTime?, DateTime?, EBookFormatDTO?>>
            GetEntries()
        {
            string title = "Ahoj";
            string authorName = "Peter";
            string authorSurname = "Novotný";
            GenreTypeDTO[] genres = new GenreTypeDTO[] { GenreTypeDTO.Classic, GenreTypeDTO.Detective };
            LanguageDTO language = LanguageDTO.Czech;
            int pageFrom = 5;
            int pageTo = 8;
            DateTime releaseFrom = DateTime.Today;
            DateTime releaseTo = DateTime.Now;
            EBookFormatDTO format = EBookFormatDTO.EPUB;

            var tup2 = new Tuple<DateTime?, DateTime?, EBookFormatDTO?>(releaseFrom, releaseTo, format);
            var tup1 = new Tuple<string, string, string, GenreTypeDTO[], LanguageDTO?, int?, int?, Tuple<DateTime?, DateTime?, EBookFormatDTO?>>
                (title, authorName, authorSurname, genres, language, pageFrom, pageTo, tup2);

            return tup1;
        }
    }
}
