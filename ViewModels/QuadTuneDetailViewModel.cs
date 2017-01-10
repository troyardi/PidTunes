using PIDHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDHub.ViewModels
{
    public class QuadTuneDetailViewModel
    {
        public TuneDetail TuneDetail { get; set; }

        public void GetTuneDetail (int tuneID)
        {
            HomeModel homeModel = new HomeModel();
            TuneDetail = homeModel.GetTuneDetail(tuneID);
        }
    }

    public class TuneDetail
    {
        public int QuadID { get; set; }
        public string QuadName { get; set; }
        public int TuneID { get; set; }
        public string TuneName { get; set; }
        public string PilotName { get; set; }
        public int FlightControllerSoftwareID { get; set; }
        public string FlightControllerSoftwareName { get; set; }
        public string FlightControllerHardwareName { get; set; }
        public string FrameName { get; set; }
        public string MotorName { get; set; }
        public string ESCName { get; set; }
        public string PropName { get; set; }
        public string BatteryName { get; set; }

    }
}