using Microsoft.EntityFrameworkCore;
using BulkyBook.Models;

namespace BulkyBook.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        } 

        public DbSet<Category> Categories { get; set; }
    }
}   
