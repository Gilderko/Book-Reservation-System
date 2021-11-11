using Autofac;
using AutoMapper;
using BL.Config;
using BL.DTOs.Filters;
using BL.DTOs.FullVersions;
using BL.Facades;
using BL.QueryObjects;
using BL.Services;
using DAL;
using DAL.Entities;
using EFInfrastructure;
using Infrastructure;
using Infrastructure.Query;
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
