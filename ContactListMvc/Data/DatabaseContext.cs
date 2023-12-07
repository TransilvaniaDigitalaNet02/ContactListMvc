using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContactListMvc.Models;

namespace ContactListMvc.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<ContactListMvc.Models.ContactListEntry> ContactListEntry { get; set; } = default!;
    }
}
