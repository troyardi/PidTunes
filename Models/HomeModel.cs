using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIDHub.ViewModels; 

namespace PIDHub.Models
{

    public class HomeModel
    {

        public List<FlightControllerSoftware> GetFCControllerSoftwareList()
        {
            pidhubEntities entity = new pidhubEntities();
            List<FlightControllerSoftware> fcSoftwareList = (from fcs in entity.FlightControllerSoftwares select fcs).ToList();
            return fcSoftwareList;
        }

        public List<FlightControllerHardware> GetFCControllerHardwareList()
        {
            pidhubEntities entity = new pidhubEntities();
            List<FlightControllerHardware> fcHardwareList = (from fch in entity.FlightControllerHardwares select fch).ToList();
            return fcHardwareList;
        }

        public List<Frame> GetFrameList()
        {
            pidhubEntities entity = new pidhubEntities();
            List<Frame> frameList = (from f in entity.Frames select f).ToList();
            return frameList;
        }

        public List<Motor> GetMotorList()
        {
            pidhubEntities entity = new pidhubEntities();
            List<Motor> motorList = (from m in entity.Motors select m).ToList();
            return motorList;
        }

        public List<ESC> GetESCList()
        {
            pidhubEntities entity = new pidhubEntities();
            List<ESC> ESCList = (from e in entity.ESCs select e).ToList();
            return ESCList; 
        }
        
        public List<Prop> GetPropList()
        {
            pidhubEntities entity = new pidhubEntities();
            List<Prop> PropList = (from p in entity.Props select p).ToList();
            return PropList;
        }

        public List<Battery> GetBatteryList()
        {
            pidhubEntities entity = new pidhubEntities();
            List<Battery> BatteryList = (from b in entity.Batteries select b).ToList();
            return BatteryList;
        }

        public List<QuadTune> GetSearchResults(SearchViewModel searchViewModel, out int totalRecords)
        {
            pidhubEntities entity = new pidhubEntities();
            var searchResults = (from tune in entity.Tunes
                                 .Include("Quad")
                                 .Include("Quad.UserAccount")
                                 select tune);

            if (searchViewModel.SelectedFCHardwareID > 0)
                searchResults = searchResults.Where(t => t.Quad.FlightControllerHardwareID == searchViewModel.SelectedFCHardwareID);

            if (searchViewModel.SelectedFCSoftwareID > 0)
                searchResults = searchResults.Where(t => t.Quad.FlightControllerSoftwareID == searchViewModel.SelectedFCSoftwareID);

            if (searchViewModel.SelectedFrameID > 0)
                searchResults = searchResults.Where(t => t.Quad.FrameID == searchViewModel.SelectedFrameID);

            if (searchViewModel.SelectedMotorID > 0)
                searchResults = searchResults.Where(t => t.Quad.MotorID == searchViewModel.SelectedMotorID);

            if (searchViewModel.SelectedESCID > 0)
                searchResults = searchResults.Where(t => t.Quad.ESCID == searchViewModel.SelectedESCID);

            if (searchViewModel.SelectedPropID > 0)
                searchResults = searchResults.Where(t => t.Quad.PropID == searchViewModel.SelectedPropID);

            if (searchViewModel.SelectedBatteryID > 0)
                searchResults = searchResults.Where(t => t.Quad.BatteryID == searchViewModel.SelectedBatteryID);

            string sortBy = searchViewModel.sortBy;
            string direction = searchViewModel.direction;

            switch (sortBy)
            {
                case "PilotName":
                    if (direction == "asc")
                        searchResults = searchResults.OrderBy(t => t.Quad.UserAccount.PilotName);
                    else
                        searchResults = searchResults.OrderByDescending(t => t.Quad.UserAccount.PilotName);
                    break;

                case "TuneName":
                    if (direction == "asc")
                        searchResults = searchResults.OrderBy(t => t.TuneName);
                    else
                        searchResults = searchResults.OrderByDescending(t => t.TuneName);
                    break;

                case "FrameName":
                    if (direction == "asc")
                        searchResults = searchResults.OrderBy(t => t.Quad.Frame.FrameName);
                    else
                        searchResults = searchResults.OrderByDescending(t => t.Quad.Frame.FrameName);
                    break;

                case "MotorName":
                    if (direction == "asc")
                        searchResults = searchResults.OrderBy(t => t.Quad.Motor.MotorName);
                    else
                        searchResults = searchResults.OrderByDescending(t => t.Quad.Motor.MotorName);
                    break;

                case "ESCName":
                    if (direction == "asc")
                        searchResults = searchResults.OrderBy(t => t.Quad.ESC.ESCName);
                    else
                        searchResults = searchResults.OrderByDescending(t => t.Quad.ESC.ESCName);
                    break;

                case "FlightControllerHardwareName":
                    if (direction == "asc")
                        searchResults = searchResults.OrderBy(t => t.Quad.FlightControllerHardware.FCHardwareName);
                    else
                        searchResults = searchResults.OrderByDescending(t => t.Quad.FlightControllerHardware.FCHardwareName);
                    break;

                case "FlightControllerSoftwareName":
                    if (direction == "asc")
                        searchResults = searchResults.OrderBy(t => t.Quad.FlightControllerSoftware.FCSoftwareName);
                    else
                        searchResults = searchResults.OrderByDescending(t => t.Quad.FlightControllerSoftware.FCSoftwareName);
                    break;

                case "PropName":
                    if (direction == "asc")
                        searchResults = searchResults.OrderBy(t => t.Quad.Prop.PropName);
                    else
                        searchResults = searchResults.OrderByDescending(t => t.Quad.Prop.PropName);
                    break;

                case "BatteryName":
                    if (direction == "asc")
                        searchResults = searchResults.OrderBy(t => t.Quad.Battery.BatteryName);
                    else
                        searchResults = searchResults.OrderByDescending(t => t.Quad.Battery.BatteryName);
                    break;

                default:
                    searchResults = searchResults.OrderBy(t => t.Tune_ID);
                    break;
            }

            totalRecords = searchResults.Count(); 

            if (searchViewModel.page != null & searchViewModel.limit != null)
            {
                int page = (int)searchViewModel.page - 1;
                int limit = (int)searchViewModel.limit; 

                searchResults = searchResults.Skip(page * limit).Take(limit);
            }

            List<QuadTune> quadTuneList = new List<QuadTune>();
            foreach (Tune tune in searchResults.ToList())
            {
                quadTuneList.Add(new QuadTune(){
                    QuadID = tune.Quad.Quad_ID,
                    QuadName = tune.Quad.QuadName,
                    TuneID = tune.Tune_ID,
                    TuneName = tune.TuneName,
                    PilotName = tune.Quad.UserAccount.PilotName,
                    FlightControllerSoftwareName = tune.Quad.FlightControllerSoftware.FCSoftwareName,
                    FlightControllerHardwareName = tune.Quad.FlightControllerHardware.FCHardwareName,
                    FrameName = tune.Quad.Frame.FrameName,   
                    MotorName = tune.Quad.Motor.MotorName,
                    ESCName = tune.Quad.ESC.ESCName,
                    PropName = tune.Quad.Prop.PropName,
                    BatteryName = tune.Quad.Battery.BatteryName
                });
            }

            return quadTuneList;
        }

        public TuneDetail GetTuneDetail(int tuneID)
        {
            pidhubEntities entity = new pidhubEntities();
            var tuneDetailResult = (from tune in entity.Tunes
                                    .Include("Quad")
                                    .Include("Quad.UserAccount")
                                    where(tune.Tune_ID == tuneID)
                                     select tune).FirstOrDefault();

            TuneDetail tuneDetail = new TuneDetail();
            tuneDetail.TuneID = tuneDetailResult.Tune_ID;
            tuneDetail.TuneName = tuneDetailResult.TuneName;
            tuneDetail.MotorName = tuneDetailResult.Quad.Motor.MotorName;
            tuneDetail.FlightControllerSoftwareID = tuneDetailResult.Quad.FlightControllerSoftwareID;
            tuneDetail.FrameName = tuneDetailResult.Quad.Frame.FrameName;
            tuneDetail.PropName = tuneDetailResult.Quad.Prop.PropName;
            tuneDetail.ESCName = tuneDetailResult.Quad.ESC.ESCName;
            tuneDetail.FlightControllerHardwareName = tuneDetailResult.Quad.FlightControllerHardware.FCHardwareName;
            tuneDetail.FlightControllerSoftwareName = tuneDetailResult.Quad.FlightControllerSoftware.FCSoftwareName;
            tuneDetail.PilotName = tuneDetailResult.Quad.UserAccount.PilotName;
            tuneDetail.BatteryName = tuneDetailResult.Quad.Battery.BatteryName; 
            
            return tuneDetail;
        }

        public UserAccountViewModel ValidateUser(UserAccountViewModel userViewModel)
        {
            pidhubEntities entity = new pidhubEntities();
            var userAccount = (from user in entity.UserAccounts
                        where user.Email == userViewModel.Email
                        && user.Password == userViewModel.Password
                        select user).FirstOrDefault();

            if (userAccount != null)
            {
                userViewModel.UserID = userAccount.User_ID;
                userViewModel.PilotName = userAccount.PilotName;
                return userViewModel;
            }
            else
            {
                return null; 
            }
        }

        public UserAccountViewModel CreateUserAccount(UserAccountViewModel userViewModel)
        {
            try
            {
                pidhubEntities entity = new pidhubEntities();
                UserAccount userAccount = new UserAccount()
                {
                    PilotName = userViewModel.PilotName,
                    Email = userViewModel.Email,
                    Password = userViewModel.Password
                };

                entity.UserAccounts.Add(userAccount);
                entity.SaveChanges();


                userViewModel.UserID = userAccount.User_ID;
            }
            catch(Exception ex)
            {
                //TODO Change method to return SaveResult
                //saveResult.ErrorMessage = String.Format("There was an error creating your account {0}.", ex.Message);
            }
            return userViewModel;
        }

        public UserAccountViewModel GetUserAccount(int userID)
        { 
            UserAccountViewModel userViewModel = new UserAccountViewModel();
            try
            {
                pidhubEntities entity = new pidhubEntities();
                var user = (from ua in entity.UserAccounts
                            where ua.User_ID == userID
                            select ua).FirstOrDefault();



                if (user != null)
                {
                    userViewModel.UserID = user.User_ID;
                    userViewModel.PilotName = user.PilotName;
                    userViewModel.Email = user.Email;
                    userViewModel.Password = user.Password;
                }
            }
            catch(Exception ex)
            {

            }
            return userViewModel;
        }

        public List<UserQuad> GetUserQuadList(UserQuadViewModel userQuadViewModel, out int totalRecords)
        {
            pidhubEntities entity = new pidhubEntities();
            var quads = from q in entity.Quads
                        .Include("FlightControllerHardware")
                        .Include("FlightControllerSoftware")
                        .Include("Frame")
                        .Include("Battery")
                        .Include("ESC")
                        .Include("Prop")
                        .Include("Motor")
                        where q.User_ID == userQuadViewModel.userID
                        select q;

            string sortBy = userQuadViewModel.sortBy;
            string direction = userQuadViewModel.direction;

            switch (sortBy)
            {
                case "QuadName":
                    if (direction == "asc")
                        quads = quads.OrderBy(t => t.QuadName);
                    else
                        quads = quads.OrderByDescending(t => t.QuadName);
                    break;

                case "FrameName":
                    if (direction == "asc")
                        quads = quads.OrderBy(t => t.Frame.FrameName);
                    else
                        quads = quads.OrderByDescending(t => t.Frame.FrameName);
                    break;

                case "FlightControllerSoftwareName":
                    if (direction == "asc")
                        quads = quads.OrderBy(t => t.FlightControllerSoftware.FCSoftwareName);
                    else
                        quads = quads.OrderByDescending(t => t.FlightControllerSoftware.FCSoftwareName);
                    break;

                case "FlightControllerHardwareName":
                    if (direction == "asc")
                        quads = quads.OrderBy(t => t.FlightControllerHardware.FCHardwareName);
                    else
                        quads = quads.OrderByDescending(t => t.FlightControllerHardware.FCHardwareName);
                    break;

                case "MotorName":
                    if (direction == "asc")
                        quads = quads.OrderBy(t => t.Motor.MotorName);
                    else
                        quads = quads.OrderByDescending(t => t.Motor.MotorName);
                    break;

                case "ESCName":
                    if (direction == "asc")
                        quads = quads.OrderBy(t => t.ESC.ESCName);
                    else
                        quads = quads.OrderByDescending(t => t.ESC.ESCName);
                    break;

                case "PropName":
                    if (direction == "asc")
                        quads = quads.OrderBy(t => t.Prop.PropName);
                    else
                        quads = quads.OrderByDescending(t => t.Prop.PropName);
                    break;

                case "BatteryName":
                    if (direction == "asc")
                        quads = quads.OrderBy(t => t.Battery.BatteryName);
                    else
                        quads = quads.OrderByDescending(t => t.Battery.BatteryName);
                    break;

                default:
                    quads = quads.OrderBy(t => t.Quad_ID);
                    break;
            }

            totalRecords = quads.Count();

            if (userQuadViewModel.page != null & userQuadViewModel.limit != null)
            {
                int page = (int)userQuadViewModel.page - 1;
                int limit = (int)userQuadViewModel.limit;

                quads = quads.Skip(page * limit).Take(limit);
            }

            List<UserQuad> quadList = new List<UserQuad>();
            foreach (Quad quad in quads.ToList())
            {
                quadList.Add(new UserQuad(){
                    QuadID = quad.Quad_ID,
                    QuadName = quad.QuadName,
                    FlightControllerSoftwareName = quad.FlightControllerSoftware.FCSoftwareName,
                    FlightControllerHardwareName = quad.FlightControllerHardware.FCHardwareName,
                    FrameName = quad.Frame.FrameName,
                    MotorName = quad.Motor.MotorName,
                    ESCName = quad.ESC.ESCName,
                    PropName = quad.Prop.PropName,
                    BatteryName = quad.Battery.BatteryName,
                });
            }
            return quadList;

        }

        public List<UserQuadTune> GetUserQuadTuneList(UserQuadTuneViewModel userQuadTuneViewModel, out int totalRecords)
        {
            pidhubEntities entity = new pidhubEntities();
            var tunes = from t in entity.Tunes
                        .Include("Quad")
                        where t.Quad_ID == userQuadTuneViewModel.QuadID
                        select t;

            string sortBy = userQuadTuneViewModel.sortBy;
            string direction = userQuadTuneViewModel.direction;

            switch (sortBy)
            {
                case "TuneName":
                    if (direction == "asc")
                        tunes = tunes.OrderBy(t => t.TuneName);
                    else
                        tunes = tunes.OrderByDescending(t => t.TuneName);
                    break;

                default:
                    tunes = tunes.OrderBy(t => t.Tune_ID);
                    break;
            }

            totalRecords = tunes.Count();

            if (userQuadTuneViewModel.page != null & userQuadTuneViewModel.limit != null)
            {
                int page = (int)userQuadTuneViewModel.page - 1;
                int limit = (int)userQuadTuneViewModel.limit;

                tunes = tunes.Skip(page * limit).Take(limit);
            }

            List<UserQuadTune> userQuadTuneList = new List<UserQuadTune>();
            foreach (Tune tune in tunes.ToList())
            {
                userQuadTuneList.Add(new UserQuadTune()
                {
                    TuneName = tune.TuneName,
                    TuneID = tune.Tune_ID,
                });
            }
            return userQuadTuneList;
        }

        public SaveResult SaveQuad(QuadDetailViewModel quadDetailsViewModel)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                pidhubEntities entity = new pidhubEntities();
                Quad quad;

                if (quadDetailsViewModel.QuadID == 0)
                {
                    quad = new Quad();
                }
                else
                {
                    quad = (from q in entity.Quads
                                where q.Quad_ID == quadDetailsViewModel.QuadID
                                select q).FirstOrDefault();
                }

                    quad.User_ID = quadDetailsViewModel.UserID;
                    quad.QuadName = quadDetailsViewModel.QuadName;
                    quad.FlightControllerSoftwareID = quadDetailsViewModel.SelectedFCSoftwareID;
                    quad.FlightControllerHardwareID = quadDetailsViewModel.SelectedFCHardwareID;
                    quad.FrameID = quadDetailsViewModel.SelectedFrameID;
                    quad.MotorID = quadDetailsViewModel.SelectedMotorID;
                    quad.ESCID = quadDetailsViewModel.SelectedESCID;
                    quad.PropID = quadDetailsViewModel.SelectedPropID;
                    quad.BatteryID = quadDetailsViewModel.SelectedBatteryID;

                    if (quadDetailsViewModel.QuadID == 0)
                        entity.Quads.Add(quad);

                    entity.SaveChanges();
            }
            catch (Exception ex)
            {   
               saveResult.ErrorMessage = String.Format("There was an error saving your quad: {0}.",ex.Message);
            }
            return saveResult; 
        }

        public SaveResult SaveTune(UserTuneViewModel userTuneViewModel)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                pidhubEntities entity = new pidhubEntities();
                Tune tune;

                // if Tune ID is not present then create a new Tune, else get the existing entity
                if (userTuneViewModel.TuneID == 0)
                {
                    tune = new Tune();
                }
                else
                {
                    tune = (from t in entity.Tunes
                            where t.Tune_ID == userTuneViewModel.TuneID
                            select t).FirstOrDefault();
                }

                // update values in entity
                tune.Quad_ID = userTuneViewModel.QuadID;
                tune.TuneName = userTuneViewModel.TuneName;
                tune.Roll_P = userTuneViewModel.Roll_P;
                tune.Pitch_P = userTuneViewModel.Pitch_P;
                tune.Yaw_P = userTuneViewModel.Yaw_P;
                tune.Roll_I = userTuneViewModel.Roll_I;
                tune.Pitch_I = userTuneViewModel.Pitch_I;
                tune.Yaw_I = userTuneViewModel.Yaw_I;
                tune.Roll_D = userTuneViewModel.Roll_D;
                tune.Pitch_D = userTuneViewModel.Pitch_D;
                tune.Yaw_D = userTuneViewModel.Yaw_D;
                tune.Roll_Rate = userTuneViewModel.Roll_Rate;
                tune.Pitch_Rate = userTuneViewModel.Pitch_Rate;
                tune.Pitch_RCRate = userTuneViewModel.Pitch_RCRate;
                tune.Yaw_Rate = userTuneViewModel.Yaw_Rate;
                tune.Yaw_RCRate = userTuneViewModel.Yaw_RCRate;
                tune.Roll_RCRate = userTuneViewModel.Roll_RCRate;
                tune.Roll_RCCurve = userTuneViewModel.Roll_RCCurve;
                tune.Pitch_RCCurve = userTuneViewModel.Pitch_RCCurve;
                tune.Yaw_RCCurve = userTuneViewModel.Yaw_RCCurve;
                tune.Roll_RCExpo = userTuneViewModel.Roll_RCExpo;
                tune.Pitch_RCExpo = userTuneViewModel.Pitch_RCExpo;
                tune.Yaw_RCExpo = userTuneViewModel.Yaw_RCExpo;

                // if this is a new Tune then must 'Add' to entity
                if (userTuneViewModel.TuneID == 0)
                    entity.Tunes.Add(tune);

                entity.SaveChanges();
            }
            catch (Exception ex)
            {
                saveResult.ErrorMessage = String.Format("There was an error saving your tune: {0}.", ex.Message);
            }
            return saveResult;
        }

        public QuadDetailViewModel GetQuadDetail(QuadDetailViewModel quadDetailViewModel)
        {
            pidhubEntities entity = new pidhubEntities();
            var quad = (from q in entity.Quads
                       where q.Quad_ID == quadDetailViewModel.QuadID
                       select q).FirstOrDefault();

            quadDetailViewModel.UserID = quad.User_ID;
            quadDetailViewModel.QuadName = quad.QuadName;
            quadDetailViewModel.SelectedFCSoftwareID = quad.FlightControllerSoftwareID;
            quadDetailViewModel.SelectedFCHardwareID = quad.FlightControllerHardwareID;
            quadDetailViewModel.SelectedFrameID = quad.FrameID;
            quadDetailViewModel.SelectedMotorID = quad.MotorID;
            quadDetailViewModel.SelectedESCID = quad.ESCID;
            quadDetailViewModel.SelectedPropID = quad.PropID;
            quadDetailViewModel.SelectedBatteryID = quad.BatteryID;

            return quadDetailViewModel; 
            
        }

        public UserTuneViewModel GetUserTuneDetail(UserTuneViewModel userTuneViewModel)
        {
            pidhubEntities entity = new pidhubEntities();
            var tune = (from t in entity.Tunes
                        where t.Tune_ID == userTuneViewModel.TuneID
                        select t).FirstOrDefault();

            userTuneViewModel.QuadID = tune.Quad_ID;
            userTuneViewModel.TuneID = tune.Tune_ID;
            userTuneViewModel.FlightControllerSoftwareID = tune.Quad.FlightControllerSoftwareID;
            userTuneViewModel.TuneName = tune.TuneName;
            userTuneViewModel.Roll_P = tune.Roll_P == null ? 0 : tune.Roll_P;
            userTuneViewModel.Pitch_P = tune.Pitch_P;
            userTuneViewModel.Yaw_P = tune.Yaw_P;
            userTuneViewModel.Roll_I = tune.Roll_I;
            userTuneViewModel.Pitch_I = tune.Pitch_I;
            userTuneViewModel.Yaw_I = tune.Yaw_I;
            userTuneViewModel.Roll_D = tune.Roll_D;
            userTuneViewModel.Pitch_D = tune.Pitch_D;
            userTuneViewModel.Yaw_D = tune.Yaw_D;
            userTuneViewModel.Roll_Rate = tune.Roll_Rate;
            userTuneViewModel.Pitch_Rate = tune.Pitch_Rate;
            userTuneViewModel.Yaw_Rate = tune.Yaw_Rate;
            userTuneViewModel.Roll_RCRate = tune.Roll_RCRate;
            userTuneViewModel.Pitch_RCRate = tune.Pitch_RCRate;
            userTuneViewModel.Yaw_RCRate = tune.Yaw_RCRate;
            userTuneViewModel.Roll_RCCurve = tune.Roll_RCCurve;
            userTuneViewModel.Yaw_RCCurve = tune.Yaw_RCCurve;
            userTuneViewModel.Roll_RCExpo = tune.Roll_RCExpo;
            userTuneViewModel.Pitch_RCExpo = tune.Pitch_RCExpo;
            userTuneViewModel.Yaw_RCExpo = tune.Yaw_RCExpo;

            return userTuneViewModel;
        
        }

        public SaveResult DeleteQuad(int quadID)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                pidhubEntities entity = new pidhubEntities();

                var quad = (from q in entity.Quads
                            where q.Quad_ID == quadID
                            select q).FirstOrDefault();

                entity.Tunes.RemoveRange(quad.Tunes);
                entity.Quads.Remove(quad);
                entity.SaveChanges();
            }
            catch (Exception ex)
            {
                saveResult.ErrorMessage = String.Format("There was an error deleting your quad: {0}.", ex.Message);
            }
            return saveResult;

        }

        public SaveResult DeleteTune(int tuneID)
        {
            SaveResult saveResult = new SaveResult();
            try
            {
                pidhubEntities entity = new pidhubEntities();
                var tune = (from t in entity.Tunes
                            where t.Tune_ID == tuneID
                            select t).FirstOrDefault();

                entity.Tunes.Remove(tune);
                entity.SaveChanges();
            }
            catch (Exception ex)
            {
                saveResult.ErrorMessage = String.Format("There was an error deleting your Tune: {0}", ex.Message);
            }
            return saveResult;
        }

        public Quad GetQuadByQuadID(int quadID)
        {
            pidhubEntities entity = new pidhubEntities();
            var quad = (from q in entity.Quads
                        where q.Quad_ID == quadID
                        select q).FirstOrDefault();

            return quad; 
        }           

        public int GetFCSoftwareIDbyQuadID(int quadID)
        {
            pidhubEntities entity = new pidhubEntities();
            var fcID = (from q in entity.Quads
                        where q.Quad_ID == quadID
                        select q.FlightControllerSoftwareID).FirstOrDefault();

            return fcID; 
        }
    }
}