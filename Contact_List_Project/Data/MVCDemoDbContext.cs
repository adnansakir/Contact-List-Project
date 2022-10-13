using Contact_List_Project.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Contact_List_Project.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Contact> Contacts { get; set; }
    }
}
