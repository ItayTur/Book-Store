using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace Common.Helpers
{
    /// <summary>
    /// Helper provides the Api url and common operations.
    /// </summary>
    public static class ConstantsHelper
    {
        
        public const string ApiUrl = "http://localhost:52119/api/";


        /// <summary>
        /// Converts IEnumerable collection to ObservableCollection of the same type.
        /// </summary>
        /// <typeparam name="T">The collection items type.</typeparam>
        /// <param name="collection">The collection going to be converted</param>
        /// <returns>ObservableCollection instance containing all the items from the IEnumerable collection.</returns>
        public static ObservableCollection<T> ConvertToObervableCollection<T>(IEnumerable<T> collection)
        {
            ObservableCollection<T> collectionToReturn = new ObservableCollection<T>();

            foreach (var item in collection)
            {
                collectionToReturn.Add(item);
            }

            return collectionToReturn;
        }



        /// <summary>
        /// Posts an object to the server.
        /// </summary>
        /// <typeparam name="T">The type of the object being posted.</typeparam>
        /// <param name="objectToPost">The object being posted.</param>
        /// <param name="apiUrl">The url that the post request is sent to.</param>
        /// <returns>The response of the request<./returns>
        public static HttpResponseMessage Post<T>(T objectToPost, string apiUrl, HttpClient httpClient)
        {
            var jsonObject = JsonConvert.SerializeObject(objectToPost);
            var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(apiUrl, stringContent).Result;
        }
        

        
    }
}
