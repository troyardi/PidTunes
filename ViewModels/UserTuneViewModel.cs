using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIDHub.Models;
using System.Web.Mvc;

namespace PIDHub.ViewModels
{
    public class UserTuneViewModel
    {
        public int QuadID { get; set; }
        public int TuneID { get; set; }
        public int FlightControllerSoftwareID { get; set; }
        public string TuneName { get; set; }
        public decimal? Roll_P { get; set; }
        public decimal? Pitch_P { get; set; }
        public decimal? Yaw_P { get; set; }
        public decimal? Roll_I { get; set; }
        public decimal? Pitch_I { get; set; }
        public decimal? Yaw_I { get; set; }
        public decimal? Roll_D { get; set; }
        public decimal? Pitch_D { get; set; }
        public decimal? Yaw_D { get; set; }
        public decimal? Roll_Rate { get; set; }
        public decimal? Pitch_Rate { get; set; }
        public decimal? Yaw_Rate { get; set; }
        public decimal? Yaw_RCRate { get; set; }
        public decimal? Pitch_RCRate { get; set; }
        public decimal? Roll_RCRate { get; set; }
        public decimal? Roll_RCCurve { get; set; }
        public decimal? Pitch_RCCurve { get; set; }
        public decimal? Yaw_RCCurve { get; set; }
        public decimal? Roll_RCExpo { get; set; }
        public decimal? Pitch_RCExpo { get; set; }
        public decimal? Yaw_RCExpo { get; set; }

        public bool isEditable { get; set; }

        public UserTuneViewModel GetUserTuneDetail(int tuneID)
        {
            HomeModel homeModel = new HomeModel();
            UserTuneViewModel userTuneViewModel = new UserTuneViewModel();
            userTuneViewModel.TuneID = tuneID;
            userTuneViewModel = homeModel.GetUserTuneDetail(userTuneViewModel);
            return userTuneViewModel;
        }
    }
}