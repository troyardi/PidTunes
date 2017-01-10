using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIDHub.Models;

namespace PIDHub.ViewModels
{
    public class SearchResultsViewModel
    {
        public List<QuadTune> QuadTuneList { get; set; }
        public int totalRecords { get; set; }
        /// <summary>
        /// Get Quad/Tunes search results from model and return as List
        /// </summary>
        /// <param name="searchViewModel"></param>
        public void GetSearchResults(SearchViewModel searchViewModel)
        {
            int total; 
            HomeModel homeModel = new HomeModel();
            List<QuadTune> quadTuneList = homeModel.GetSearchResults(searchViewModel, out total);
            totalRecords = total; 
            QuadTuneList = quadTuneList;
        }

    }

    public class QuadTune
    {
        public int QuadID { get; set; }
        public string QuadName { get; set; }
        public int TuneID { get; set; }
        public string TuneName { get; set; }
        public string PilotName { get; set; }
        public string FlightControllerSoftwareName { get; set; }
        public string FlightControllerHardwareName { get; set; }
        public string FrameName { get; set; }
        public string MotorName { get; set; }
        public string ESCName { get; set; }
        public string PropName { get; set; }
        public string BatteryName { get; set; }

    }


}