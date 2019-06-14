using Microsoft.EntityFrameworkCore;
using LoginReg.Models;
namespace LoginReg.Models
{
    public class LogRegContext : DbContext {
        public LogRegContext(DbContextOptions options) : base(options) {}
        public DbSet<RegUser> Users {get;set;}
    }
}