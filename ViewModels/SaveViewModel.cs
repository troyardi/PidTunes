using PIDHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDHub.ViewModels
{
    public class SaveViewModel
    {
        private HomeModel homeModel = new HomeModel();
        private SaveResult saveResult = new SaveResult();

        public SaveResult SaveQuad(QuadDetailViewModel quadDetailsViewModel)
        {

            saveResult = homeModel.SaveQuad(quadDetailsViewModel);
            return saveResult; 
        }

        public SaveResult DeleteQuad(int quadID)
        {
            saveResult = homeModel.DeleteQuad(quadID);
            return saveResult; 
        }

        public SaveResult SaveTune(UserTuneViewModel userTuneViewModel)
        {
            saveResult = homeModel.SaveTune(userTuneViewModel);
            return saveResult; 
        }

        public SaveResult DeleteTune(int tuneID)
        {
            saveResult = homeModel.DeleteTune(tuneID);
            return saveResult;
        }
       
    }

    public class SaveResult
    {
        public string ErrorMessage;
    }
}
    