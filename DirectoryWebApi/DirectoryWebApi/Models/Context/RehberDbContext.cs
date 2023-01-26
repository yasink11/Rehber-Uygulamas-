using DirectoryWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DirectoryWebApi.Models.Context
{
    public class RehberDbContext : DbContext
    {
        internal object IletisimBilgileri;

        public RehberDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Rehber> Rehbers { get; set; }  
        public DbSet<IletisimBilgileri> IletisimBilgileris { get; set; }  
    }
}
