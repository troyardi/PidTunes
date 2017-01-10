using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIDHub.Models;
using System.Web.Mvc;


namespace PIDHub.ViewModels
{
    public class SearchViewModel
    {
        public int SelectedFCSoftwareID { get; set; }
        public int SelectedFCHardwareID { get; set; }
        public int SelectedFrameID { get; set; }
        public int SelectedMotorID { get; set; }
        public int SelectedESCID { get; set; }
        public int SelectedPropID { get; set; }
        public int SelectedBatteryID { get; set; }

        // used by JQuery grid
        public int? page { get; set; }
        public int? limit { get; set; }
        public string sortBy { get; set; }
        public string direction { get; set; }
        public string searchString { get; set; }

        public SelectList FlightControllerSoftwareList
        {
            get
            {
                // get entity list from model and return as SelectList
                HomeModel homeModel = new HomeModel();
                List<FlightControllerSoftware> fcSoftwareList = homeModel.GetFCControllerSoftwareList();
                SelectList slFCSoftwareList = new SelectList(fcSoftwareList, "ID", "FCSoftwareName", 0);
                return slFCSoftwareList;
            }
        }

        public SelectList FlightControllerHardwareList
        {
            get
            {
                // get entity list from model and return as SelectList
                HomeModel homeModel = new HomeModel();
                List<FlightControllerHardware> fcHardwareList = homeModel.GetFCControllerHardwareList();
                SelectList slFCHardwareList = new SelectList(fcHardwareList, "ID", "FCHardwareName", 0);
                return slFCHardwareList;
            }
        }

        public SelectList FrameList
        {
            get
            {
                // get entity list from model and return as SelectList
                HomeModel homeModel = new HomeModel();
                List<Frame> frameList = homeModel.GetFrameList();
                SelectList slFrameList = new SelectList(frameList, "ID", "FrameName", 0);
                return slFrameList;
            }
        }

        public SelectList MotorList
        {
            get
            {
                // get entity list from model and return as SelectList
                HomeModel homeModel = new HomeModel();
                List<Motor> MotorList = homeModel.GetMotorList();
                SelectList slMotorList = new SelectList(MotorList, "ID", "MotorName", 0);
                return slMotorList;
            }
        }

        public SelectList ESCList
        {
            get
            {
                HomeModel homeModel = new HomeModel();
                List<ESC> ESCList = homeModel.GetESCList();
                SelectList slESCList = new SelectList(ESCList, "ID", "ESCName", 0);
                return slESCList;
            }
        }
        public SelectList PropList
        {
            get
            {
                HomeModel homeModel = new HomeModel();
                List<Prop> PropList = homeModel.GetPropList();
                SelectList slPropList = new SelectList(PropList, "ID", "PropName", 0);
                return slPropList;
            }
        }
                    
            public SelectList BatteryList
        {
            get
            {
                HomeModel homeModel = new HomeModel();
                List<Battery> BatteryList = homeModel.GetBatteryList();
                SelectList slBatteryList = new SelectList(BatteryList, "ID", "BatteryName", 0);
                return slBatteryList;
            }
        }
    }
}

 