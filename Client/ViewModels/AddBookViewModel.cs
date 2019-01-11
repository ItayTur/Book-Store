using Common.Enums;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;

namespace Client.ViewModels
{
    public class AddBookViewModel : INotifyPropertyChanged
    {

        #region Private fields

        private readonly string booksApiUrl;

        private readonly HttpClient httpClient;

        private readonly INavigationService navigationService;

        #endregion


        #region Properties


        public string Name { get; set; }


        public string Publisher { get; set; }


        public DateTimeOffset PublishDate { get; set; }


        public string Price { get; set; }


        public string Amount { get; set; }


        public CategoryEnum Category { get; set; }

        public IEnumerable<CategoryEnum> Categories
        {
            get
            {
                return Enum.GetValues(typeof(CategoryEnum)).Cast<CategoryEnum>();
            }

        }


        public string DiscountPercentage { get; set; }


        public string Edition { get; set; }


        public string Author { get; set; }


        public string Summery { get; set; }


        public string Issue { get; set; }


        public string Writer { get; set; }


        private string message;


        public string Message
        {
            get { return message; }
            set { message = value; Notify(nameof(Message)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private bool isJournal;

        public bool IsJournal
        {
            get { return isJournal; }
            set
            {
                isJournal = value;
                Notify(nameof(IsJournal));
                if (isJournal)
                {
                    IsBook = false;
                }
            }
        }

        private bool isBook;

        public bool IsBook
        {
            get { return isBook; }
            set
            {
                isBook = value;
                Notify(nameof(IsBook));
                if (isBook)
                {
                    IsJournal = false;
                }
            }
        }

        public ObservableCollection<BookModel> Books { get; set; }

        public ObservableCollection<JournalModel> Journals { get; set; }

        #endregion



        /// <summary>
        /// Constructor.
        /// </summary>
        public AddBookViewModel(AddBookPacketModel packet)
        {
            booksApiUrl = ConstantsHelper.ApiUrl + "Books";

            httpClient = new HttpClient();

            this.navigationService = packet.NavigationService;

            Books = packet.Books;

            Journals = packet.Journals;
        }



        /// <summary>
        /// Invokes the PropertyChanged event 
        /// for the property associated with the property name specified.
        /// </summary>
        /// <param name="propertyName">The property identifier.</param>
        public void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        /// <summary>
        /// Posts new book to the server.
        /// </summary>
        public void AddBook()
        {
            AbstractBookModel abstractBook = null;
            HttpResponseMessage httpResponse = null;
            if (IsJournal)
            {
                abstractBook = GetJournal();
                if (abstractBook != null)
                {
                    httpResponse = ConstantsHelper.Post(abstractBook, booksApiUrl + "/PostJournal", httpClient);
                    var addedBook = httpResponse.Content.ReadAsAsync<JournalModel>().Result;
                    Journals.Add(addedBook);
                }
            }
            else if (IsBook)
            {
                abstractBook = GetBook();
                if (abstractBook != null)
                {
                    httpResponse = ConstantsHelper.Post(abstractBook, booksApiUrl + "/PostBooks", httpClient);
                    var addedBook = httpResponse.Content.ReadAsAsync<BookModel>().Result;
                    Books.Add(addedBook);
                }

            }
            else
            {
                Message = "Book or journal must be choose";
            }


            if (httpResponse != null)
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    Message = $"{abstractBook.Name} has been added successfully";
                }

                else
                {
                    Message = "There was an error! check the fields to see if anything is missing";
                }
            }
        }



        /// <summary>
        /// Gets a new BookModel instance based on the Bounded properties.
        /// </summary>
        /// <returns>A new fully populated BookModel instance.</returns>
        private BookModel GetBook()
        {

            int intAmount = 0;
            int intEdition = 0;
            double doublePrice = 0;
            double doubleDiscount = 0;
            BookModel book = null;

            if (BookNumericPropertiesValid(out intAmount, ref intEdition, ref doublePrice, ref doubleDiscount)&& StringBookPropertiesValid())
            {
                book = new BookModel
                {
                    Name = Name,
                    Author = Author,
                    Amount = intAmount,
                    Category = Category,
                    Edition = intEdition,
                    Price = doublePrice,
                    PublishDate = PublishDate.Date,
                    Publisher = Publisher,
                    Summery = Summery,
                    DiscountPercentage = doubleDiscount
                };
            }

            return book;
        }



        /// <summary>
        /// Checks if the journal numeric properties are valid.
        /// </summary>
        private bool JournalNumericPropertiesValid(out int intAmount, ref int intIssue, ref double doublePrice, ref double doubleDiscount)
        {
            return CheckValidInt(Amount, out intAmount, "Amount gets integers only") && CheckValidInt(Issue, out intIssue, "Issue gets integers only") && CheckValidDouble(Price, out doublePrice, "Price gets double only") && CheckValidDouble(DiscountPercentage, out doubleDiscount, "Discount gets double only") && (IsJournal || IsBook);
        }


        /// <summary>
        /// Checks if the book numeric properties are valid.
        /// </summary>
        private bool BookNumericPropertiesValid(out int intAmount, ref int intEdition, ref double doublePrice, ref double doubleDiscount)
        {
            return CheckValidInt(Amount, out intAmount, "Amount gets integers only") && CheckValidInt(Edition, out intEdition, "Edition gets integers only") && CheckValidDouble(Price, out doublePrice, "Price gets double only") && CheckValidDouble(DiscountPercentage, out doubleDiscount, "Discount gets double only") && (IsJournal || IsBook);
        }



        /// <summary>
        /// Checks if the journal text properties are valid.
        /// </summary>
        private bool StringBookPropertiesValid()
        {
            bool isValid = Name != null && Author != null && Publisher != null && Summery != null;
            if (!isValid)
            {
                Message = "String fields are empty";
            }
            return isValid;
        }


        
        /// <summary>
        /// Checks if the journal text properties are valid.
        /// </summary>
        private bool StringJournalPropertiesValid()
        {
            bool isValid = Name != null && Writer != null && Publisher != null;
            if (!isValid)
            {
                Message = "String fields are empty";
            }
            return isValid;
        }


        /// <summary>
        /// Checks if numeric values are valid double.
        /// </summary>
        private bool CheckValidDouble(string number, out double result, string errorMessage)
        {
            bool flag = false;
            if (double.TryParse(number, out result))
            {
                flag = true;
            }
            else
            {
                Message = errorMessage;
            }

            return flag;
        }



        /// <summary>
        /// Checks if numeric values are valid int.
        /// </summary>
        private bool CheckValidInt(string number, out int result, string errorMessage)
        {
            bool flag = false;
            if (int.TryParse(number, out result))
            {
                flag = true;
            }
            else
            {
                Message = errorMessage;
            }

            return flag;
        }



        /// <summary>
        /// Gets a new JournalModel instance based on the Bounded properties.
        /// </summary>
        /// <returns>A new fully populated JournalModel instance.</returns>
        private JournalModel GetJournal()
        {
            int intAmount = 0;
            int intIssue = 0;
            double doublePrice = 0;
            double doubleDiscount = 0;
            JournalModel journal = null;

            if (JournalNumericPropertiesValid(out intAmount, ref intIssue, ref doublePrice, ref doubleDiscount)&& StringJournalPropertiesValid())
            {
                journal = new JournalModel
                {
                    Name = Name,
                    Amount = intAmount,
                    Category = Category,
                    Price = doublePrice,
                    PublishDate = PublishDate.Date,
                    Publisher = Publisher,
                    DiscountPercentage = doubleDiscount,
                    Issue = intIssue,
                };
            }

            return journal;
        }



        /// <summary>
        /// Navigates the frame back to the home view.
        /// </summary>
        public void GoBack()
        {
            navigationService.GoBack();
        }
    }
}
