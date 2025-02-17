using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
                string jwt = JwtBuilder.Create().WithAlgorithm(new HMACSHA256Algorithm())
                   .AddClaims(new Dictionary<string, object>()
                   {
                                    { "FullName", username },
                                    { "Mail", mailAddress },
                   }).WithSecret("MailHogging").Encode();
                var res = _context.Inscriptions.Add(new Inscription() { FullName = username, MailAddress = mailAddress, JWT = jwt });
                _context.SaveChanges();
                return Ok(res.Entity);
            }
            catch(BrokerUnreachableException)
            {
                return BadRequest("Rabbit server broker was not found. Did you forget to turn on its Docker image?");
            }
            catch(DbUpdateException dbue)
            {
                SqlException? innerException = (SqlException?)dbue.InnerException;
                if (innerException != null && innerException.Number == 2627) //Id for unique constraint error
                {
                    return BadRequest("Someone with the same mail address already exists in the database.");
                }
                else
                {
                    return BadRequest("Something went wrong when updating the database...");
                }     
            }
        }
    }
}