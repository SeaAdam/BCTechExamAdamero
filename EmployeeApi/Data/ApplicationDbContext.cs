using BCTechExamAdamero.Models;
using Microsoft.EntityFrameworkCore;

namespace BCTechExamAdamero.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base (dbContextOptions)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
