using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    /// <summary>
    /// Provides a mechanism to navigate between pages.
    /// </summary>
    public interface INavigationService
    {


        /// <summary>
        /// Navigates to the specified page.
        /// </summary>
        /// <param name="page"></param>
        void NavigateTo(Type viewType);


        /// <summary>
        /// Navigates to the specified page and
        /// supply additional page-specific parameters.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="parameter"></param>
        void NavigateTo(Type viewType, object parameter);


        /// <summary>
        /// Navigates to the previous page in the navigation history.
        /// </summary>
        void GoBack();


    }
}
