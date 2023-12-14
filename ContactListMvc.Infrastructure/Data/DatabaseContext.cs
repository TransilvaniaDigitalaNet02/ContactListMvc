using ContactListMvc.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactListMvc.Infrastructure.Data
{
    internal class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<ContactListEntryEntity> ContactListEntry { get; set; } = default!;
    }
}
