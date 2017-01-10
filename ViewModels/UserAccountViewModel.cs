using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIDHub.Models;

namespace PIDHub.ViewModels
{
    public class UserAccountViewModel
    {
        public int UserID { get; set; }
        public string PilotName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserAccountViewModel ValidateUserLogin(UserAccountViewModel userViewModel)
        {
            HomeModel homeModel = new HomeModel();
            return homeModel.ValidateUser(userViewModel);
        }

        public UserAccountViewModel CreateUserAccount(UserAccountViewModel userViewModel)
        {
            HomeModel homeModel = new HomeModel();
            return homeModel.CreateUserAccount(userViewModel);
        }

        public UserAccountViewModel GetUserAccount(int userID)
        {
            HomeModel homeModel = new HomeModel();
            return homeModel.GetUserAccount(userID);
        }
    }
}