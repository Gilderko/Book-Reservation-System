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

            var repo = new Repository<BookInstance>(uof);

            var books = repo.GetByID(1, null, new string[] { "AllReservations" });

            Console.WriteLine(books.AllReservations.Count);
        }
    }
}
