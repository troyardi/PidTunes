using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIDHub.Models;
using System.Web.Mvc;

namespace PIDHub.ViewModels
{
    /// <summary>
    /// List of user's quads. 
    /// </summary>
    public class UserQuadViewModel
    {
        public List<UserQuad> UserQuadList { get; set; }

        // used by JQuery grid
        public int? page { get; set; }
        public int? limit { get; set; }
        public string sortBy { get; set; }
        public string direction { get; set; }
        public string searchString { get; set; }
        public int totalRecords { get; set; }

        public int userID { get; set; }

        public void GetUserQuadList(UserQuadViewModel userQuadViewModel)
        {
            int total;
            HomeModel homeModel = new HomeModel();
            UserQuadList = homeModel.GetUserQuadList(userQuadViewModel, out total);
            totalRecords = total; 
        }

    }

    public class UserQuad
    {
        public int QuadID { get; set; }
        public string QuadName { get; set; }
        public string FlightControllerSoftwareName { get; set; }
        public string FlightControllerHardwareName { get; set; }
        public string FrameName { get; set; }
        public string MotorName { get; set; }
        public string ESCName { get; set; }
        public string PropName { get; set; }
        public string BatteryName { get; set; }
    }
}
