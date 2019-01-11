using Common.Interfaces;
using Common.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Server.Controllers
{
    public class BooksController : ApiController
    {

        private readonly IAbstractBooksRepository booksRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="booksRepository">The implamented IAbstractBooksRepository instance.</param>
        public BooksController(IAbstractBooksRepository booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        [HttpGet]
        /// <summary>
        /// Gets all the books located in the database.
        /// </summary>
        /// <returns>A colletion of AbstractBookModel instances.</returns>
        public IEnumerable<BookModel> GetBooks()
        {
            return ConvertToBookModelsCollection(booksRepository.GetAll());

        }



        [HttpGet]
        [Route("api/Books/Journals")]
        /// <summary>
        /// Gets all the journals located in the database.
        /// </summary>
        /// <returns>A fully populated JournalModel collection.</returns>
        public IEnumerable<JournalModel> GetJournals()
        {
            return ConvertToJournalModelsCollection(booksRepository.GetAll());
        }



        /// <summary>
        /// Convertes the abstract book models collection into book model collection.
        /// </summary>
        /// <param name="abstractBooks">The abstract collection being converted.</param>
        /// <returns>The converted book models collection.</returns>
        private ICollection<BookModel> ConvertToBookModelsCollection(IEnumerable<AbstractBookModel> abstractBooks)
        {
            ICollection<BookModel> books = new List<BookModel>();

            foreach (var abstractBook in abstractBooks)
            {
                if (abstractBook is BookModel book)
                {
                    books.Add(book);
                }
            }

            return books;
        }



        /// <summary>
        /// Convertes the abstract book models collection into journal model collection.
        /// </summary>
        /// <param name="abstractBooks">The abstract collection being converted.</param>
        /// <returns>The converted journal models collection.</returns>
        private ICollection<JournalModel> ConvertToJournalModelsCollection(IEnumerable<AbstractBookModel> abstractBooks)
        {
            ICollection<JournalModel> journals = new List<JournalModel>();

            foreach (var abstractBook in abstractBooks)
            {
                if (abstractBook is JournalModel journal)
                {
                    journals.Add(journal);
                }
            }

            return journals;
        }



        [HttpPost]
        [Route("api/Books/PostBooks")]
        public IHttpActionResult AddBook(BookModel book)
        {
            IHttpActionResult actionResult = BadRequest();

            if (ModelState.IsValid)
            {
                var addedBook = booksRepository.Add(book);
                actionResult = Ok(addedBook);
            }

            return actionResult;
        }



        [HttpPost]
        [Route("api/Books/PostJournal")]
        public IHttpActionResult AddBook(JournalModel book)
        {
            IHttpActionResult actionResult = BadRequest();

            if (ModelState.IsValid)
            {
                var addedBook = booksRepository.Add(book);
                actionResult = Ok(addedBook);
            }

            return actionResult;
        }


        [HttpDelete]
        [Route("api/Books/DeleteBook/{bookId}")]
        public IHttpActionResult DeleteBook(int bookId)
        {
            booksRepository.Remove(bookId);
            return Ok();
        }

        [HttpPost]
        [Route("api/Books/PutBook")]
        public IHttpActionResult UpdateBook(BookModel book)
        {
            booksRepository.Update(book);
            return Ok();
        }

        [HttpPost]
        [Route("api/Books/PutJournal")]
        public IHttpActionResult UpdateJournal(JournalModel journal)
        {
            booksRepository.Update(journal);
            return Ok();
        }
    }
}
