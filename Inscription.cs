using System.ComponentModel.DataAnnotations;

namespace Inscrip
{
    public class Inscription
    {
        [Key]
        public required string MailAddress { get; set; }
        public required string FullName { get; set; }

        public string? JWT { get; set; }
    }
}