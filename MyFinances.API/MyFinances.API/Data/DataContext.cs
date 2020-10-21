using Microsoft.EntityFrameworkCore;

namespace MyFinances.API.Data
{
    public class DataContext : DbContext
    {
        protected DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
    }
}
