using DAL.Entities;
using System;
using System.Collections.Generic;
using BL.DTOs.Entities.Reservation;

namespace BL.Services
{
    public interface IReservationService : ICRUDService<ReservationDTO, Reservation>
    {
        public IEnumerable<ReservationPrevDTO> GetReservationsPreviewByUser(int userId, DateTime from, DateTime to);

        public IEnumerable<ReservationPrevDTO> GetReservationPrevsByEReader(int eReaderId, DateTime from, DateTime to);

        public IEnumerable<ReservationPrevDTO>
            GetReservationPrevsByBookInstance(int bookId, DateTime from, DateTime to);
    }
}