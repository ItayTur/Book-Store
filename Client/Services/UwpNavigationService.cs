using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Client.Services
{
    /// <summary>
    /// Provides a mechanism to navigate between pages.
    /// </summary>
    public sealed class UwpNavigationService : INavigationService
    {

        private readonly MainPage mainPage;

        public UwpNavigationService(MainPage mainPage)
        {
            this.mainPage = mainPage;
        }

        
        /// <summary>
        /// Navigates to the previous page in the navigation history.
        /// </summary>
        public void GoBack()
        {
            mainPage.Frame.GoBack();
        }
        
        /// <summary>
        /// Navigates to the specified page.
        /// </summary>
        /// <param name="page"></param>
        public void NavigateTo(Type viewType)
        {
            mainPage.Frame.Navigate(viewType);
        }

        
        /// <summary>
        /// Navigates to the specified page and
        /// supply additional page-specific parameters.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="parameter"></param>
        public void NavigateTo(Type viewType, object parameter)
        {
            mainPage.Frame.Navigate(viewType, parameter);
        }
    }
}
