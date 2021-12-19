using Autofac.Extras.Moq;
using AutoMapper;
using BL.Config;
using BL.DTOs.Entities.EReader;
using BL.DTOs.Entities.EReaderInstance;
using BL.Facades;
using BL.QueryObjects;
using BL.Services.Implementations;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;



namespace MoqTest
{
    public class EReaderInstanceFacadeTests
    {
        private IMapper _mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

        private EReaderInstanceFacade Setup(AutoMock mock, Func<Tuple<string, string, string, int?, int?, QueryResult<EReader>, QueryResult<EReaderInstance>>> result)
        {
            var uow = mock.Mock<IUnitOfWork>().Object;

            mock.Mock<IQuery<EReader>>()
                .Setup(x => x.Execute().Result)
                .Returns(result().Item6);

            var ereaderQuery = mock.Create<IQuery<EReader>>();

            mock.Mock<IQuery<EReaderInstance>>()
                .Setup(x => x.Execute().Result)
                .Returns(result().Item7);

            var ereaderInstanceQuery = mock.Create<IQuery<EReaderInstance>>();

            var ereaderQueryObject = new QueryObject<EReaderPrevDTO, EReader>(_mapper, ereaderQuery);
            var ereaderInstanceQueryObject =
                new QueryObject<EReaderInstancePrevDTO, EReaderInstance>(_mapper, ereaderInstanceQuery);

            var ereaderService = new CRUDService<EReaderPrevDTO, EReader>(null, _mapper, ereaderQueryObject);
            var ereaderInstanceService = new EReaderInstancePreviewService(null, _mapper, ereaderInstanceQueryObject);

            var ereaderInstanceFacade = new EReaderInstanceFacade(uow, null, null, null, ereaderInstanceService,
                ereaderService, null, null);

            return ereaderInstanceFacade;
        }

        [Fact]
        public async Task GetEReaderInstancePrevsBy1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookCollectionFacade = Setup(mock, GetEntries);

                var data = GetEntries();
                await Evaluate(mock, bookCollectionFacade, data);
            }
        }

        [Fact]
        public async Task GetEReaderInstancePrevsBy2()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookCollectionFacade = Setup(mock, GetEntries2);

                var data = GetEntries2();
                await Evaluate(mock, bookCollectionFacade, data);
            }
        }

        [Fact]
        public async Task GetEReaderInstancePrevsBy3()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var bookCollectionFacade = Setup(mock, GetEntries3);

                var data = GetEntries3();
                await Evaluate(mock, bookCollectionFacade, data);
            }
        }

        private static async Task Evaluate(AutoMock mock, EReaderInstanceFacade bookCollectionFacade, Tuple<string, string, string, int?, int?, QueryResult<EReader>, QueryResult<EReaderInstance>> data)
        {
            var (result, _) = await bookCollectionFacade.GetEReaderInstancePrevsBy(null, null, data.Item1, data.Item2, data.Item3, data.Item4, data.Item5);

            EReaderInstanceInvocationsInclude(mock, data.Item1);
            EReaderInvocationsInclude(mock, data.Item2);
            EReaderInvocationsInclude(mock, data.Item3);
            EReaderInvocationsInclude(mock, data.Item4);
            EReaderInvocationsInclude(mock, data.Item5);

            Assert.True(result.Count() == data.Item7.Items.Count());

            Assert.True(mock.Mock<IQuery<EReader>>()
                .Invocations.Where(invo => invo.Method.Name == nameof(IQuery<EReader>.Execute)).Count() == 1);

            Assert.True(mock.Mock<IQuery<EReaderInstance>>()
                .Invocations.Where(invo => invo.Method.Name == nameof(IQuery<EReader>.Execute)).Count() == 1);
        }

        private static void EReaderInvocationsInclude(AutoMock mock, object argument)
        {
            var predicates = mock.Mock<IQuery<EReader>>().Invocations
                                .Where(invo => invo.Method.Name == nameof(IQuery<EReader>.Where))
                                .Where(invo => invo.Arguments[0] is CompositePredicate)
                                .Select(invo => invo.Arguments[0] as CompositePredicate).First();

            Assert.Contains(predicates.Predicates, pred => (pred as SimplePredicate).ComparedValue.Equals(argument));
        }

        private static void EReaderInstanceInvocationsInclude(AutoMock mock, object argument)
        {
            var predicates = mock.Mock<IQuery<EReaderInstance>>().Invocations
                                .Where(invo => invo.Method.Name == nameof(IQuery<EReader>.Where))
                                .Where(invo => invo.Arguments[0] is CompositePredicate)
                                .Select(invo => invo.Arguments[0] as CompositePredicate).First();

            Assert.Contains(predicates.Predicates, pred => (pred as SimplePredicate).ComparedValue == argument);
        }

        public Tuple<string, string, string, int?, int?, QueryResult<EReader>, QueryResult<EReaderInstance>> GetEntries()
        {
            string desc = "Ahoj";
            string company = "Good company";
            string model = "Good model";
            int? memorySizeFrom = 2;
            int? memoerySizeTo = 3;

            var ereaders = new QueryResult<EReader>()
            {
                Items = new List<EReader>
                {
                    new EReader()
                    {
                        Id = 1
                    },
                    new EReader()
                    {
                        Id = 2
                    }
                }
            };

            var ereaderInstances = new QueryResult<EReaderInstance>()
            {
                Items = new List<EReaderInstance>
                {
                    new EReaderInstance()
                    {
                        Id = 1
                    },
                    new EReaderInstance()
                    {
                        Id = 2
                    }
                }
            };


            var result = new Tuple<string, string, string, int?, int?,
                QueryResult<EReader>, QueryResult<EReaderInstance>>
                (desc, company, model, memorySizeFrom, memoerySizeTo, ereaders, ereaderInstances);

            return result;
        }

        public Tuple<string, string, string, int?, int?, QueryResult<EReader>, QueryResult<EReaderInstance>> GetEntries2()
        {
            string desc = "Nazdar";
            string company = "Kto company";
            string model = "Some model";
            int? memorySizeFrom = 555;
            int? memoerySizeTo = 2155;

            var ereaders = new QueryResult<EReader>()
            {
                Items = new List<EReader>
                {
                    new EReader()
                    {
                        Id = 3
                    },
                    new EReader()
                    {
                        Id = 6
                    }
                }
            };

            var ereaderInstances = new QueryResult<EReaderInstance>()
            {
                Items = new List<EReaderInstance>
                {
                    new EReaderInstance()
                    {
                        Id = 1,
                        EReaderTemplateID = 3
                    },
                    new EReaderInstance()
                    {
                        Id = 2,
                        EReaderTemplateID = 6
                    }
                }
            };


            var result = new Tuple<string, string, string, int?, int?,
                QueryResult<EReader>, QueryResult<EReaderInstance>>
                (desc, company, model, memorySizeFrom, memoerySizeTo, ereaders, ereaderInstances);

            return result;
        }

        public Tuple<string, string, string, int?, int?, QueryResult<EReader>, QueryResult<EReaderInstance>> GetEntries3()
        {
            string desc = "";
            string company = "";
            string model = "";
            int? memorySizeFrom = 55;
            int? memoerySizeTo = 2114;

            var ereaders = new QueryResult<EReader>()
            {
                Items = new List<EReader>
                {
                    new EReader()
                    {
                        Id = 3
                    },
                    new EReader()
                    {
                        Id = 4
                    },
                    new EReader()
                    {
                        Id = 5
                    },
                    new EReader()
                    {
                        Id = 6
                    }
                }
            };

            var ereaderInstances = new QueryResult<EReaderInstance>()
            {
                Items = new List<EReaderInstance>
                {
                    new EReaderInstance()
                    {
                        Id = 1,
                        EReaderTemplateID = 5
                    },
                    new EReaderInstance()
                    {
                        Id = 2,
                        EReaderTemplateID = 6
                    },
                    new EReaderInstance()
                    {
                        Id = 3,
                        EReaderTemplateID = 4
                    }
                }
            };


            var result = new Tuple<string, string, string, int?, int?,
                QueryResult<EReader>, QueryResult<EReaderInstance>>
                (desc, company, model, memorySizeFrom, memoerySizeTo, ereaders, ereaderInstances);

            return result;
        }
    }
}
