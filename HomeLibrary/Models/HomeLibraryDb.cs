using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HomeLibrary.Models
{
    public class HomeLibraryDb : DbContext
{
    public HomeLibraryDb(DbContextOptions<HomeLibraryDb> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
}

}
