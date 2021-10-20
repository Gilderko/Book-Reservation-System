using System;
using System.Linq;
using DAL;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using DAL.Enums;
using EFInfrastructure;
using Infrastructure.Query.Operators;
using Infrastructure.Query.Predicates;
using Microsoft.EntityFrameworkCore;

namespace TestingConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new BookRentalDbContext();
            var unitOfWork = new UnitOfWork(context);

            var pred = new SimplePredicate("Id", 1, ValueComparingOperator.Equal);

            var quer = new Query<BookInstance>(unitOfWork);
            quer.Where(pred);
            quer.LoadExplicitReferences("FromBookTemplate");

            var result = quer.Execute();
            Console.WriteLine(result.Items.First().FromBookTemplate.Description);
        }
    }
}
