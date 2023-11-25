using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services
{
    public class BooksService
    {
        private readonly IMongoCollection<Book> _booksCollection;
        public BooksService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _booksCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BooksCollectionName);
        }
        public async Task<List<Book>> GetBooksAsync() => await _booksCollection.Find(_ => true).ToListAsync();
        public async Task<Book?> GetBooksAsync(string id) => await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreaterBook(Book newBook) => await _booksCollection.InsertOneAsync(newBook);
        public async Task UpdateBook(string id, Book newBook) => await _booksCollection.ReplaceOneAsync(x => x.Id == id, newBook);
        public async Task DeleteBook(string id) => await _booksCollection.DeleteOneAsync(id);
    }
}
