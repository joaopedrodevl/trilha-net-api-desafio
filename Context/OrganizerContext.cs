using Microsoft.EntityFrameworkCore;
using trilha_net_api_desafio.Models;

namespace Context{
    public class OrganizerContext : DbContext
    {
        public OrganizerContext(DbContextOptions<OrganizerContext> options) : base(options)
        {
        }

        public DbSet<Tasks> Tasks { get; set; }
    }
}