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

            var uof = new UnitOfWork(new BookRentalDbContext());
            var mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));

            var qObj = new QueryObject<BookDTO, Book>(mapper, uof);

            var pred = new PredicateDto("PageCount", 100, Infrastructure.Query.Operators.ValueComparingOperator.GreaterThanOrEqual);

            var filter = new FilterDto();
            filter.Predicate = pred;

            qObj.LoadExplicitCollections(book => new string[] { nameof(book.BookInstances)});
            
            var result = qObj.ExecuteQuery(filter);

            foreach(var value in result.Items)
            {
                Console.WriteLine(value.BookInstances.Count);
            }

        }
    }
}
