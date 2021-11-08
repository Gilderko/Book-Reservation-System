using BL.DTOs.FullVersions;
using BL.Services;
using DAL;
using DAL.Entities;
using EFInfrastructure;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class ReservationFacade
    {
        private IUnitOfWork unitOfWork;
        private CRUDService<ReservationDTO, Reservation> service;

        public ReservationFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<ReservationDTO, Reservation>(unitOfWork);
        }

        public void Create(ReservationDTO reservation)
        {
            service.Insert(reservation);
        }

        public ReservationDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(ReservationDTO reservation)
        {
            service.Update(reservation);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
