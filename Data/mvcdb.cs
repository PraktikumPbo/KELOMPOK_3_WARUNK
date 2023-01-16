using app_testlar.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace app_testlar.Data
{
    public class mvcdb : DbContext

    {
        public mvcdb(DbContextOptions options) : base(options)
        {

        }
        public DbSet<employe> employes { get; set; }
    }
}
