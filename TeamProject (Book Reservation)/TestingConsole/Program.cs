using System;
using System.Linq;
using DAL;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using DAL.Enums;
using DAL.Query;
using DAL.Query.Predicates;
using Microsoft.EntityFrameworkCore;

namespace TestingConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new BookRentalDbContext();

            var pred = new SimplePredicate("Id", 1, DAL.Query.Operators.ValueComparingOperator.Equal);

            var quer = new QueryBase<BookInstance>(context);
            quer.Where(pred);
            quer.LoadExplicitReferences("FromBookTemplate");

            var result = quer.Execute();
            Console.WriteLine(result.Items.First().FromBookTemplate.Description);
        }
    }
}
