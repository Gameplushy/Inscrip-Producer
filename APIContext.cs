using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace Inscrip
{
    public class APIContext : DbContext
    {
        public DbSet<Inscription> Inscriptions { get; set; }

        public APIContext(DbContextOptions<APIContext> options) : base(options) { }

    }
}
