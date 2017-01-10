using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIDHub.Models;
using System.Web.Mvc;

namespace PIDHub.ViewModels
{
    /// <summary>
    /// List of tunes for the users quad. 
    /// </summary>
    public class UserQuadTuneViewModel
    {
        public List<UserQuadTune> UserQuadTuneList { get; set; }

        // used by JQuery grid
        public int? page { get; set; }
        public int? limit { get; set; }
        public string sortBy { get; set; }
        public string direction { get; set; }
        public string searchString { get; set; }
        public int totalRecords { get; set; }

        public int QuadID { get; set; }
        public string QuadName { get; set; }
        public int FlightControllerSoftwareID { get; set; }

        public void GetUserQuadTuneList(UserQuadTuneViewModel userQuadTuneViewModel)
        {
            int total;
            HomeModel homeModel = new HomeModel();
            UserQuadTuneList = homeModel.GetUserQuadTuneList(userQuadTuneViewModel, out total);
            totalRecords = total; 
        }

        public void GetFCSoftwareIDbyQuadID(int quadID)
        {
            HomeModel homeModel = new HomeModel();
            FlightControllerSoftwareID = homeModel.GetFCSoftwareIDbyQuadID(quadID);
        }

    }

    public class UserQuadTune
    {
        public int TuneID { get; set; }
        public string TuneName { get; set; }
    }
}