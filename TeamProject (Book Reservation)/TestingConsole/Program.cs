using Autofac;
using BL.Config;
using BL.Facades;
using System;

namespace TestingConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var container = AutofacBLConfig.Configure();

            var serv = container.Resolve<AuthorFacade>();

            var name = serv.Get(1);

            Console.WriteLine(name.Name);
        }
    }
}
