using MongodbApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace MongodbApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public async Task<IList<Book>> GetAll() {
            IAsyncCursor<Book> cursor = await _books.FindAsync(book => true);
            IList<Book> books = await cursor.ToListAsync();
            return books;
        }

        public async Task<Book> Get(string id) =>
            await _books.Find(book => book.Id == id).FirstOrDefaultAsync();

        public async Task<Book> Create(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
        }

        public async Task Update(string id, Book bookIn) =>

            await _books.ReplaceOneAsync(book => book.Id == id, bookIn);

        public async Task Remove(Book bookIn) =>
            await _books.DeleteOneAsync(book => book.Id == bookIn.Id);

        public async Task Remove(string id) =>
            await _books.DeleteOneAsync(book => book.Id == id);

        public async Task<BookPaginatedList> GetAllPaginated(int offset, int limit)
        {
            FindOptions<Book> options = new FindOptions<Book> { Skip = offset, Limit = limit };

            IAsyncCursor<Book> documents = await _books.FindAsync(book => true, options);

            List<Book> result = await documents.ToListAsync();

            var count = await _books.CountDocumentsAsync(new BsonDocument());

            return new BookPaginatedList
            {
                Result = result,
                Count = count
            };
        }
    }
}