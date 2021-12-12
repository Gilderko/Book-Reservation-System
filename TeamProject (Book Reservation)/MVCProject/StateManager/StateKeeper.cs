using Autofac;
using BL.Config;
using BL.DTOs.Entities.Reservation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MVCProject.StateManager.FilterStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.StateManager
{
    public sealed class StateKeeper
    {
        // Singleton pattern 

        private static readonly StateKeeper instance = new StateKeeper();

        static StateKeeper()
        {
        }

        private StateKeeper()
        {

        }

        public static StateKeeper Instance
        {
            get
            {
                return instance;
            }
        }

        // Singleton pattern        

        // Temporary data

        /// <summary>
        /// Saves the object into temporary data till next request.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddTillNextRequest(Controller controller, TempDataKeys key, object value)
        {
            controller.TempData[key.ToString()] = value;
        }        

        /// <summary>
        /// Peeks at the value and doesnt destroy it immidiately even if the save wasnt called
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public object ReadDataFromLastRequest(Controller controller, TempDataKeys key)
        {
            return controller.TempData.Peek(key.ToString());
        }

        public object ReadDataFromLastRequest(ITempDataDictionary tempData, TempDataKeys key)
        {
            return tempData.Peek(key.ToString());
        }

        /// <summary>
        /// Saves all values in temporary data so even when ReadAndDestroyNow is called they wont be destroyed
        /// </summary>
        /// <param name="controller"></param>
        public void SaveExistingObjectsForNextRequest(Controller controller)
        {
            controller.TempData.Keep();
        }

        /// <summary>
        /// Saves a specific object in temporary data so even when ReadAndDestroyNow is called they wont be destroyed
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="key"></param>
        public void SaveSpecificObjectForNextRequest(Controller controller, TempDataKeys key)
        {
            controller.TempData.Keep(key.ToString());
        }

        // Temporary data

        // Session data

        private const string _reservationKey = "currentReservation";

        /// <summary>
        /// Saves this reservation into sessions for at least 5 minutes
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="newReserv"></param>
        public void SetReservationInSession(Controller controller, ReservationDTO newReserv)
        {
            controller.HttpContext.Session.Set<ReservationDTO>(_reservationKey, newReserv);
        }

        /// <summary>
        /// Gets the current active reservation in current session
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public ReservationDTO GetReservationInSession(Controller controller)
        {
            return controller.HttpContext.Session.Get<ReservationDTO>(_reservationKey);
        }

        public ReservationDTO GetReservationInSession(ISession currSession)
        {
            return currSession.Get<ReservationDTO>(_reservationKey);
        }
    }
}
