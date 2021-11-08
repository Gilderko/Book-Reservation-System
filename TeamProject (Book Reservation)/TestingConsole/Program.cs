using AutoMapper;
using BL.Config;
using BL.DTOs.Filters;
using BL.DTOs.FullVersions;
using BL.QueryObjects;
using DAL;
using DAL.Entities;
using EFInfrastructure;
using Infrastructure;
using System;

namespace TestingConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {

            IMapper map = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));
            IUnitOfWork uof = new UnitOfWork(new BookRentalDbContext());


            var qObj = new QueryObject<BookDTO,Book>(map, uof);

            var filter = new FilterDto();

            filter.SortAscending = true;

            filter.Predicate = new PredicateDto("PageCount", 100, Infrastructure.Query.Operators.ValueComparingOperator.GreaterThan);

            var result = qObj.ExecuteQuery(filter);

            Console.WriteLine($"Entries {result.TotalItemsCount}");
        }
    }
}
