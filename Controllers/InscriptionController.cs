using Microsoft.AspNetCore.Mvc;

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
            var res = _context.Inscriptions.Add(new Inscription() { FullName = username, MailAddress = mailAddress });
            _context.SaveChanges();
            return Ok(res.Entity);
        }
    }
}