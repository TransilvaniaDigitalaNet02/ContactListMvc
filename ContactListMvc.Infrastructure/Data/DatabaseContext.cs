using Microsoft.EntityFrameworkCore;
using ContactListMvc.Infrastructure.Entities;

namespace ContactListMvc.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<ContactListEntryEntity> ContactListEntry { get; set; } = default!;
    }
}
