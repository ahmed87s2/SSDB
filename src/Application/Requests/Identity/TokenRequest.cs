using System.ComponentModel.DataAnnotations;

namespace SSDB.Application.Requests.Identity
{
    public class TokenRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}