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
        private IUnitOfWork _unitOfWork;
        private CRUDService<ReservationDTO, Reservation> _service;

        public ReservationFacade(IUnitOfWork unitOfWork, CRUDService<ReservationDTO, Reservation> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(ReservationDTO reservation)
        {
            _service.Insert(reservation);
        }

        public ReservationDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(ReservationDTO reservation)
        {
            _service.Update(reservation);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
