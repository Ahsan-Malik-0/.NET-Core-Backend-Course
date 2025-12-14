using ASP.NET_Web_API_CRUD_Operations.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Web_API_CRUD_Operations.Data
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
