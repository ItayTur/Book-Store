using Client.Services;
using Client.Views;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Client.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        #region Private fields

        private readonly string loginUrl;

        private readonly INavigationService navigationService;

        #endregion


        #region Properties

        public string UserName { get; set; }

        public string Password { get; set; }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; Notify(nameof(Message)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            loginUrl = ConstantsHelper.ApiUrl + "Employees/Login";
        }



        /// <summary>
        /// Invokes the propertyChanged event notifying the bound properties.
        /// </summary>
        /// <param name="propertyName">The name of the property being changed.</param>
        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        /// <summary>
        /// Tries to login the employee to the server
        /// with the specified user name and password retrived
        /// via binding.
        /// </summary>
        public void Login()
        {
            
            UserNamePasswordModel userNamePassword = new UserNamePasswordModel(UserName, Password);
            var jsonObject = JsonConvert.SerializeObject(userNamePassword);
            var stringContent = new StringContent(jsonObject, UnicodeEncoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage res = httpClient.PostAsync(loginUrl, stringContent).Result;
            if (res.IsSuccessStatusCode)
            {
                EmployeeModel employee = res.Content.ReadAsAsync<EmployeeModel>().Result;
                OpenHomeViewAsync(employee);
            }
            else
            {
                Message = "User name or password are incorrect";
            }
        }



        /// <summary>
        /// Opens the home view with a new HomeViewModel instance.
        /// </summary>
        private void OpenHomeViewAsync(EmployeeModel employee)
        {

            navigationService.NavigateTo(typeof(HomeView), new HomeViewModel(employee, navigationService));
        }
    }
}
