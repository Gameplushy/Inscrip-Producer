using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client.Exceptions;

namespace Inscrip.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InscriptionController : ControllerBase
    {
        private readonly APIContext _context;

        public InscriptionController(APIContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SignUp(string username, string mailAddress)
        {
            try
            {
                var res = _context.Inscriptions.Add(new Inscription() { FullName = username, MailAddress = mailAddress });
                _context.SaveChanges();
                return Ok(res.Entity);
            }
            catch(BrokerUnreachableException bue)
            {
                return BadRequest("Rabbit server broker was not found. Did you forget to turn on its Docker image?");
            }
            catch(ArgumentException ae)
            {
                return BadRequest("An account with this mail address already exists...");    
            }
        }
    }
}