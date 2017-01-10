using PIDHub.Models;
using PIDHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIDHub.Helpers;

namespace PIDHub.Controllers
{
    public class HomeController : Controller
    {
        private IGridMvcHelper gridMvcHelper = new GridMvcHelper();

        //HttpGET
        public ActionResult Index()
        {
            SearchViewModel vm = new SearchViewModel();
            return View(vm);
        }

        [HttpPost]
        public PartialViewResult Search(SearchViewModel searchViewModel)
        {
            Session["searchVM"] = searchViewModel;
            return PartialView("~/Views/_SearchResultsGrid.cshtml");
        }

        [HttpGet, OutputCache(NoStore = true, Duration = 0)]
        public JsonResult GetSearchResults(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            SearchViewModel searchViewModel = (SearchViewModel)Session["searchVM"];
            searchViewModel.page = page;
            searchViewModel.limit = limit;
            searchViewModel.sortBy = sortBy;
            searchViewModel.direction = direction;
            searchViewModel.searchString = searchString; 
            SearchResultsViewModel searchResultsViewModel = new SearchResultsViewModel();
            searchResultsViewModel.GetSearchResults(searchViewModel);

            List<QuadTune> records = searchResultsViewModel.QuadTuneList;
            int total = searchResultsViewModel.totalRecords;
            
            JsonResult gridData = Json(new { records, total }, JsonRequestBehavior.AllowGet);
            return gridData;
        }

        public PartialViewResult GetTuneDetail(int tuneID)
        {
            QuadTuneDetailViewModel quadTuneDetailViewModel = new QuadTuneDetailViewModel();
            quadTuneDetailViewModel.GetTuneDetail(tuneID);

            return PartialView("~/Views/_QuadTuneDetail.cshtml", quadTuneDetailViewModel);
        }

        public PartialViewResult GetPIDTable(int fcControllerSoftwareID, int tuneID, bool isEditable)
        {
            UserTuneViewModel userTuneViewModel = new UserTuneViewModel();

            if (tuneID != 0) 
            {
                userTuneViewModel = userTuneViewModel.GetUserTuneDetail(tuneID);
            }
            else
            {                
                userTuneViewModel.Roll_P = 0;
                userTuneViewModel.Pitch_P = 0;
                userTuneViewModel.Yaw_P = 0;
                userTuneViewModel.Roll_I = 0;
                userTuneViewModel.Pitch_I = 0;
                userTuneViewModel.Yaw_I = 0;
                userTuneViewModel.Roll_D = 0;
                userTuneViewModel.Pitch_D = 0;
                userTuneViewModel.Yaw_D = 0;
                userTuneViewModel.Roll_Rate = 0;
                userTuneViewModel.Pitch_Rate = 0;
                userTuneViewModel.Yaw_Rate = 0;
                userTuneViewModel.Yaw_RCRate = 0;
                userTuneViewModel.Pitch_RCRate = 0;
                userTuneViewModel.Roll_RCRate = 0;
                userTuneViewModel.Roll_RCCurve = 0;
                userTuneViewModel.Pitch_RCCurve = 0;
                userTuneViewModel.Yaw_RCCurve = 0;
                userTuneViewModel.Roll_RCExpo = 0;
                userTuneViewModel.Pitch_RCExpo = 0;
                userTuneViewModel.Yaw_RCExpo = 0;
            }

            userTuneViewModel.isEditable = isEditable;

            //use tuneID to get all the pid info for the tune and populate some viewmodel with it and send the viewmodel back with corosponding pid partial view. 
            switch (fcControllerSoftwareID)
            {
                case 1:
                    return PartialView("~/Views/_KissPIDS.cshtml", userTuneViewModel);
                case 2:
                    return PartialView("~/Views/_CleanFlightPIDS.cshtml", userTuneViewModel);
                case 3:
                    return PartialView("~/Views/_CleanFlightPIDS.cshtml", userTuneViewModel);

                default:
                    return null;
            }
        }

        [HttpPost]
        public string Login(UserAccountViewModel userViewModel)
        {
            UserAccountViewModel userViewModelLoggedIn = userViewModel.ValidateUserLogin(userViewModel);
            string msg = "";

            if (userViewModelLoggedIn != null)
            {
                Session["UserID"] = userViewModelLoggedIn.UserID;
                Session["PilotName"] = userViewModelLoggedIn.PilotName;
                Session["Email"] = userViewModelLoggedIn.Email;
                Session["Password"] = userViewModelLoggedIn.Password;
            }
            else
            {
                msg = "Invalid Email and/or Password";
            }

            return msg;
        }

        public void Logout()
        {
            Session.Clear();
        }

        public PartialViewResult ShowCreateUserAccount()
        {
            return PartialView("~/Views/_CreateUserAccount.cshtml");
        }

        [HttpPost]
        public string CreateUserAccount(UserAccountViewModel userViewModel)
        {
            UserAccountViewModel userViewModelNewAccount = userViewModel.CreateUserAccount(userViewModel);
            string msg = "";

            if (userViewModelNewAccount != null)
            {
                Session["UserID"] = userViewModelNewAccount.UserID;
                Session["PilotName"] = userViewModelNewAccount.PilotName;
                Session["Email"] = userViewModelNewAccount.Email;
                Session["Password"] = userViewModelNewAccount.Password;
            }
            else
            {
                msg = "Unable to create account :(";
            }

            return msg;
        }

        public PartialViewResult ShowUserAccount()
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            UserAccountViewModel userViewModel = new UserAccountViewModel();
            userViewModel = userViewModel.GetUserAccount(userID);

            return PartialView("~/Views/_CreateUserAccount.cshtml", userViewModel); 
        }

        public PartialViewResult ShowUserQuadList()
        {
            return PartialView("~/Views/_UserQuadList.cshtml");
        }

        [HttpGet, OutputCache(Duration = 0)]
        public JsonResult GetUserQuadList(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            UserQuadViewModel userQuadViewModel = new UserQuadViewModel();
            int userID = Convert.ToInt32(Session["UserID"]);
            userQuadViewModel.page = page;
            userQuadViewModel.limit = limit;
            userQuadViewModel.sortBy = sortBy;
            userQuadViewModel.direction = direction;
            userQuadViewModel.searchString = searchString;
            userQuadViewModel.userID = userID;
            userQuadViewModel.GetUserQuadList(userQuadViewModel);

            List<UserQuad> records = userQuadViewModel.UserQuadList;
            int total = userQuadViewModel.totalRecords;

            JsonResult gridData = Json(new { records, total }, JsonRequestBehavior.AllowGet);
            return gridData;
        }

        public PartialViewResult ShowUserQuadTuneList(int quadID)
        {
            Session["quadID"] = quadID;
            ViewBag.QuadID = quadID;

            UserQuadTuneViewModel userQuadTuneViewModel = new UserQuadTuneViewModel();
            userQuadTuneViewModel.GetFCSoftwareIDbyQuadID(quadID);

            ViewBag.FlightControllerSoftwareID = userQuadTuneViewModel.FlightControllerSoftwareID; 

            return PartialView("~/Views/_UserQuadTuneList.cshtml");
        }

        [HttpGet, OutputCache(Duration = 0)]
        public JsonResult GetUserQuadTuneList(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            UserQuadTuneViewModel userQuadTuneViewModel = new UserQuadTuneViewModel();
            int quadID = Convert.ToInt32(Session["quadID"]);
            userQuadTuneViewModel.page = page;
            userQuadTuneViewModel.limit = limit;
            userQuadTuneViewModel.sortBy = sortBy;
            userQuadTuneViewModel.direction = direction;
            userQuadTuneViewModel.searchString = searchString;
            userQuadTuneViewModel.QuadID = quadID;
            userQuadTuneViewModel.GetUserQuadTuneList(userQuadTuneViewModel);

            List<UserQuadTune> records = userQuadTuneViewModel.UserQuadTuneList;
            int total = userQuadTuneViewModel.totalRecords;

            JsonResult gridData = Json(new { records, total }, JsonRequestBehavior.AllowGet);
            return gridData;
        }

        public PartialViewResult AddQuad()
        {
            QuadDetailViewModel quadDetailViewModel = new QuadDetailViewModel();
            return PartialView("~/Views/_AddEditQuad.cshtml", quadDetailViewModel);
        }

        public PartialViewResult AddTune(int quadID, int fcSoftwareID)
        {
            UserTuneViewModel userTuneViewModel = new UserTuneViewModel();
            userTuneViewModel.QuadID = quadID;
            userTuneViewModel.FlightControllerSoftwareID = fcSoftwareID;

            return PartialView("~/Views/_AddEditTune.cshtml", userTuneViewModel);
        }

        public PartialViewResult EditQuad(int quadID)
        {
            QuadDetailViewModel quadDetailsViewModel = new QuadDetailViewModel();

            quadDetailsViewModel = quadDetailsViewModel.GetQuadDetails(quadID);

            return PartialView("~/Views/_AddEditQuad.cshtml", quadDetailsViewModel);
        }

        public PartialViewResult EditTune(int tuneID)
        {
            UserTuneViewModel userTuneViewModel = new UserTuneViewModel();

            userTuneViewModel = userTuneViewModel.GetUserTuneDetail(tuneID);

            return PartialView("~/Views/_AddEditTune.cshtml", userTuneViewModel);
        }

        [HttpPost]
        public string SaveQuad(QuadDetailViewModel quadDetailsViewModel)
        {
            quadDetailsViewModel.UserID = Convert.ToInt32(Session["UserID"]);

            SaveViewModel saveViewModel = new SaveViewModel();
            SaveResult saveResult = saveViewModel.SaveQuad(quadDetailsViewModel);

            if (String.IsNullOrEmpty(saveResult.ErrorMessage))
                return "";
            else
                return saveResult.ErrorMessage;
        }

        [HttpPost]
        public string SaveTune(UserTuneViewModel userTuneViewModel)
        {
            SaveViewModel saveViewModel = new SaveViewModel();
            SaveResult saveResult = saveViewModel.SaveTune(userTuneViewModel);

            if (String.IsNullOrEmpty(saveResult.ErrorMessage))
                return "";
            else
                return saveResult.ErrorMessage;
        }

        public string DeleteQuad(int quadID)
        {
            SaveViewModel saveViewModel = new SaveViewModel();
            SaveResult saveResult = saveViewModel.DeleteQuad(quadID);

            return saveResult.ErrorMessage;
        }

        public string DeleteTune(int tuneID)
        {
            SaveViewModel saveViewModel = new SaveViewModel();
            SaveResult saveResult = saveViewModel.DeleteTune(tuneID);

            return saveResult.ErrorMessage;
        }
    }
}