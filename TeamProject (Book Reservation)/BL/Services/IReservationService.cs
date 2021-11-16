using DAL.Entities;
using System;
using System.Collections.Generic;
using BL.DTOs.Entities.Reservation;

namespace BL.Services
{
    public interface IReservationService<TEntityDTO, TEntity> : ICRUDService<TEntityDTO, TEntity> where TEntityDTO : ReservationDTO
                                                                                            where TEntity : Reservation
    {
        public IEnumerable<ReservationPrevDTO> GetReservationsPreviewByUser(int userId, DateTime from, DateTime to);

        public IEnumerable<ReservationPrevDTO> GetReservationPrevsByEReader(int eReaderId, DateTime from, DateTime to);

        public IEnumerable<ReservationPrevDTO>
            GetReservationPrevsByBookInstance(int bookId, DateTime from, DateTime to);
    }
}