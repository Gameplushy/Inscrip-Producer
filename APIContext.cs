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

        public override int SaveChanges()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672/%2f");

            using (var conn = factory.CreateConnection())
            {
                using (var ch = conn.CreateModel())
                {
                    ch.QueueDeclare(queue: "myQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var message = JsonConvert.SerializeObject(ChangeTracker.Entries().Where(e=>e.Entity is Inscription).First().Entity);
                    var body = Encoding.UTF8.GetBytes(message);

                    ch.BasicPublish(exchange: "", routingKey: "myQueue", basicProperties: null, body: body);
                }
            }
            int res = base.SaveChanges();
            return res;
        }
    }
}
