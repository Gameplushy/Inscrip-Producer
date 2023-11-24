using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client.Exceptions;
using System.Net.Mail;

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
        [ProducesResponseType(200, Type = typeof(Inscription))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult SignUp(string username, string mailAddress)
        {
            try
            {
                _ = new MailAddress(mailAddress);
            }
            catch (Exception)
            {
                return BadRequest("This is not a valid mail address.");
            }

            try
            {
                var res = _context.Inscriptions.Add(new Inscription() { FullName = username, MailAddress = mailAddress });
                _context.SaveChanges();
                return Ok(res.Entity);
            }
            catch(BrokerUnreachableException)
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