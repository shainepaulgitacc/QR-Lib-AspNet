namespace LibraryManagement.Model.Infrastracture.Contracts
{
    public interface IBaseRepository<T>
    {
        public Task<IEnumerable<T>> GetAllRecords();
        public Task<T> GetOneRecord(string id);
        public Task Add(object newData);
        public Task Update(string id, object newData);
        public Task Delete(string Id);
    }
}
