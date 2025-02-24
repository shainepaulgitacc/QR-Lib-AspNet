using LibraryManagement.Data;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;

namespace LibraryManagement.Model.Infrastracture.Implementations
{
    public class BookRepository : DbBaseRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext db):base(db)
        {
            _context = db;
        }
        public async Task<Book> GetOneBook(int Id)
        {
            return await _context.Books.FindAsync(Id);
        }
    }
}
