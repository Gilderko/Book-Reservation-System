using DAL.Entities;
using System;
using System.Collections.Generic;
using BL.DTOs.Entities.Reservation;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IReservationService : ICRUDService<ReservationDTO, Reservation>
    {
        public Task<IEnumerable<ReservationPrevDTO>> GetReservationsPreviewByUser(int userId, DateTime from, DateTime to);

        public Task<IEnumerable<ReservationPrevDTO>> GetReservationPrevsByEReader(int eReaderId, DateTime? from, DateTime? to);

        public Task<IEnumerable<ReservationPrevDTO>>
            GetReservationPrevsByBookInstance(int bookId, DateTime? from, DateTime? to);
    }
}