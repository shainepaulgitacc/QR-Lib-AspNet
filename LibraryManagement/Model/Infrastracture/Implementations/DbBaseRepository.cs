using LibraryManagement.Data;
using LibraryManagement.Model.Infrastracture.Contracts;
using Microsoft.CodeAnalysis.Host.Mef;

namespace LibraryManagement.Model.Infrastracture.Implementations
{
    public class DbBaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext _db;
        public DbBaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Add(object newData)
        {
            await _db.Set<T>().AddAsync((T)newData);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var getOne = await GetOneRecord(id);
            if (getOne != null)
            {
                _db.Set<T>().Remove(getOne);
               await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllRecords()
        {
            return _db.Set<T>().ToList();    
        }

        public async Task<T> GetOneRecord(string id)
        {
            int parsedId = 0;
            if(int.TryParse(id,out parsedId))
            {
                return await _db.Set<T>().FindAsync(parsedId);
            }
            else
            {
                return await _db.Set<T>().FindAsync(id);
            }
           
        }
        public async Task Update(string id, object newData)
        {
            var getOne = await GetOneRecord(id);
            if(getOne != null)
            {
                _db.Entry(getOne).CurrentValues.SetValues(newData);
                await _db.SaveChangesAsync();
            }
        }
    }
}
