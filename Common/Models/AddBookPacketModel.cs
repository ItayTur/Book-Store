using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Common.Models
{
    /// <summary>
    /// Holding the info the add book view needs.
    /// </summary>
    public class AddBookPacketModel
    {

        public INavigationService NavigationService { get; set; }

        public ObservableCollection<BookModel> Books { get; set; }

        public ObservableCollection<JournalModel> Journals { get; set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        public AddBookPacketModel(INavigationService navigationService, ObservableCollection<BookModel> books, ObservableCollection<JournalModel> journals)
        {
            NavigationService = navigationService;

            Books = books;

            Journals = journals;
        }
    }
}
