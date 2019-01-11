using Client.Views;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Client.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {


        #region Private fields

        private readonly string customersApiUrl;

        private readonly string booksApiUrl;

        private readonly string purchasesApiUrl;

        private readonly HttpClient httpClient;

        public readonly INavigationService NavigationService;

        private CustomerModel customer;

        private AbstractBookModel abstractBook;

        #endregion



        #region Properties

        public EmployeeModel Employee { get; set; }

        public ObservableCollection<BookModel> Books { get; set; }

       //public ObservableCollection<JournalModel> Journals { get; set; }

        private ObservableCollection<JournalModel> journals;

        public ObservableCollection<JournalModel> Journals
        {
            get { return journals; }
            set { journals = value; /*Notify(nameof(Journals));*/ }
        }


        public string UserEmail { get; set; }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; Notify(nameof(Message)); }
        }

        private bool isBuyable;

        public bool IsBuyable
        {
            get { return isBuyable; }
            set { isBuyable = value; Notify(nameof(IsBuyable)); }
        }


        private BookModel book;

        public BookModel Book
        {
            get { return book; }
            set
            {
                book = value;
                if (book != null)
                {
                    BookName = book.Name;
                    abstractBook = book;
                    if (book.DiscountPercentage != 0)
                    {
                        TotalePrice = (book.DiscountPercentage / 100) * book.Price;
                    }
                    else
                    {
                        TotalePrice = book.Price;
                    }
                    IsRemoveEnabled = true;
                    CheckIfBuyable();
                }
            }
        }



        private JournalModel journal;

        public JournalModel Journal
        {
            get { return journal; }
            set
            {
                journal = value;
                if (journal != null)
                {
                    BookName = journal.Name;
                    abstractBook = journal;
                    if (journal.DiscountPercentage != 0)
                    {
                        TotalePrice = (journal.DiscountPercentage / 100) * journal.Price;
                    }
                    else
                    {
                        TotalePrice = journal.Price;
                    }
                    IsRemoveEnabled = true;
                    CheckIfBuyable();
                    
                }
            }
        }

        private double totalPrice;

        public double TotalePrice
        {
            get { return totalPrice; }
            set
            { totalPrice = value; Notify(nameof(TotalePrice)); }
        }

        private bool isRemoveEnabled;

        public bool IsRemoveEnabled
        {
            get { return isRemoveEnabled; }
            set { isRemoveEnabled = value; Notify(nameof(IsRemoveEnabled)); }
        }


        private string bookName;

        public string BookName
        {
            get { return bookName; }
            set { bookName = value; Notify(nameof(BookName)); }
        }


        private string sellerName;

        public string SellerName
        {
            get { return sellerName; }
            set { sellerName = value; Notify(nameof(SellerName)); }
        }

        private string buyierName;

        public string BuyierName
        {
            get { return buyierName; }
            set { buyierName = value; Notify(nameof(BuyierName)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        #endregion



        /// <summary>
        /// Constructor.
        /// </summary>
        public HomeViewModel(EmployeeModel employee, INavigationService navigationService)
        {
            booksApiUrl = ConstantsHelper.ApiUrl + "Books";

            customersApiUrl = ConstantsHelper.ApiUrl + "Customers";

            purchasesApiUrl = ConstantsHelper.ApiUrl + "Purchases";

            httpClient = new HttpClient();

            Employee = employee;

            SellerName = Employee.FirstName + " " + Employee.LastName;

            Books = new ObservableCollection<BookModel>();

            Journals = new ObservableCollection<JournalModel>();

            this.NavigationService = navigationService;

            PopulateCollections();
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
        /// Populates the observable collections with all the books in the server. 
        /// </summary>
        private void PopulateCollections()
        {

            HttpResponseMessage httpBooksResponse = httpClient.GetAsync(booksApiUrl).Result;
            IEnumerable<BookModel> books = httpBooksResponse.Content.ReadAsAsync<IEnumerable<BookModel>>().Result;
            HttpResponseMessage httpJournalsResponse = httpClient.GetAsync(booksApiUrl + "/Journals").Result;
            IEnumerable<JournalModel> journals = httpJournalsResponse.Content.ReadAsAsync<IEnumerable<JournalModel>>().Result;
            FillBooksObervableCollections(books);
            FillJournalsObervableCollections(journals);
        }



        /// <summary>
        /// Populates the books observalbe collection.
        /// </summary>
        /// <param name="books">The collection from which the items are taken.</param>
        private void FillBooksObervableCollections(IEnumerable<BookModel> books)
        {
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }



        /// <summary>
        /// Populates the journals observalbe collection.
        /// </summary>
        /// <param name="journals">The collection from which the items are taken.</param>
        private void FillJournalsObervableCollections(IEnumerable<JournalModel> journals)
        {
            foreach (var journal in journals)
            {
                Journals.Add(journal);
            }
        }



        /// <summary>
        /// Gets the customer associated with the specified Email.
        /// </summary>
        public void ConnectCustomer()
        {
            if (IsUserEmailValid())
            {
                var customerLoginModel = new CustomerLoginModel(UserEmail);
                HttpResponseMessage httpResponse = Post(customerLoginModel, customersApiUrl + "/GetCustomerByEmail");

                if (httpResponse.IsSuccessStatusCode)
                {
                    customer = httpResponse.Content.ReadAsAsync<CustomerModel>().Result;
                    CheckIfBuyable();
                    BuyierName = customer.FirstName + " " + customer.LastName;
                    Message = $"{customer.FirstName} {customer.LastName} is in!";
                }
                else
                {
                    Message = $"{UserEmail} not found";
                }
            }
            else
            {
                Message = "Email was not entered.";
            }


        }


        /// <summary>
        /// Checks if the user Email is valid. 
        /// </summary>
        /// <returns>True if it's valid, false otherwise.</returns>
        private bool IsUserEmailValid()
        {
            return UserEmail != "" && UserEmail != null;
        }



        /// <summary>
        /// Creates a new purchase and send it to the server.
        /// </summary>
        public void Buy()
        {
            PurchaseModel purchase = new PurchaseModel
            {
                AbstractBookId = abstractBook.Id,
                CustomerId = customer.Id,
                EmployeeId = Employee.Id,
                Date = new SqlDateTime(DateTime.Now)
            };

            if(abstractBook is BookModel bookModel)
            {
                UpdateBookAmount(bookModel);
            }
            else if(abstractBook is JournalModel journalModel)
            {
                UpdateJournalAmount(journalModel);
            }

            HttpResponseMessage httpResponseMessage = Post(purchase, purchasesApiUrl);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Message = "Sold successfully!";
            }
            else
            {
                Message = "An error occured. Check server connection.";
            }
        }


        /// <summary>
        /// Updates journal's amount.
        /// </summary>
        private void UpdateJournalAmount(JournalModel journalModel)
        {
            journalModel.Amount--;
            HttpResponseMessage httpResponseMessage = ConstantsHelper.Post(journalModel, booksApiUrl + "/PutJournal", httpClient);
            Journals.Clear();
            Books.Clear();
            PopulateCollections();

            if (journalModel.Amount <= 0)
            {
                var journalToRemove = Journals.FirstOrDefault(j => j.Amount == 0);
                Journals.Remove(journalToRemove);
            }
        }



        /// <summary>
        /// Posts an object to the server.
        /// </summary>
        /// <typeparam name="T">The type of the object being posted.</typeparam>
        /// <param name="objectToPost">The object being posted.</param>
        /// <param name="apiUrl">The url that the post request is sent to.</param>
        /// <returns>The response of the request<./returns>
        private HttpResponseMessage Post<T>(T objectToPost, string apiUrl)
        {
            var jsonObject = JsonConvert.SerializeObject(objectToPost);
            var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(apiUrl, stringContent).Result;
        }



        /// <summary>
        /// Upades book's amount.
        /// </summary>
        private void UpdateBookAmount(BookModel bookModel)
        {
            bookModel.Amount--;
            HttpResponseMessage httpResponseMessage = ConstantsHelper.Post(bookModel, booksApiUrl + "/PutBook", httpClient);
            Journals.Clear();
            Books.Clear();
            PopulateCollections();
            if (bookModel.Amount <= 0)
            {
                var bookToRemove = Books.FirstOrDefault(b => b.Amount == 0);
                Books.Remove(bookToRemove);
            }
        }


        /// <summary>
        /// Opens the AddBook View.
        /// Provide it with the current navigation service.
        /// </summary>
        public void OpenAddBookView()
        {
            NavigationService.NavigateTo(typeof(AddBookView), new AddBookPacketModel(NavigationService,Books,Journals));
        }



        /// <summary>
        /// Opens the MainPage view (Login view).
        /// </summary>
        public void OpenLoginView()
        {
            NavigationService.GoBack();
        }



        /// <summary>
        /// Removes the book from the view and the server.
        /// </summary>
        public void RemoveBook()
        {
            HttpResponseMessage httpResponse = httpClient.DeleteAsync(booksApiUrl + $"/DeleteBook/{abstractBook.Id}").Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                Message = $"{bookName} has been removed";

                if (abstractBook is BookModel bookModel)
                    Books.Remove(bookModel);
                else if (abstractBook is JournalModel journalModel)
                    Journals.Remove(journalModel);
                BookName = "";
                TotalePrice = 0;
                IsBuyable = false;
                
            }
            else
            {
                Message = "Check server connection";
            }
        }


        
        /// <summary>
        /// Enable or disable the buying action.
        /// </summary>
        private void CheckIfBuyable()
        {
            IsBuyable = (Book != null || Journal != null) && customer != null;
        }


    }
}
