using LibraryManagement.Data;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace LibraryManagement.Model.Infrastracture.Implementations
{
    public class DbEmployeeRepository: DbBaseRepository<Employee>,IEmployeeRepository
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _db;
        public DbEmployeeRepository(ApplicationDbContext db, IWebHostEnvironment env) : base(db)
        {
            _env = env;
            _db = db;
        }

        public async Task<string> UploadProfile(IFormFile file)
        {
            string fileName = null;
            if(file != null)
            {
                string uploadDr = Path.Combine(_env.WebRootPath, "ProfilePicture");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDr, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {   
                    file.CopyTo(fileStream);
                }
                
            }
            return fileName;
        }
    }
}
