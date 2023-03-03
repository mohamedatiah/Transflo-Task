using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using TransfloRepository.Models;

namespace TransfloDriver.Models
{
    public class Driver:BaseEntity
    {

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? Createdby { get; set; } = null;
        public bool IsAdmin { get; set; } = false;

    }
}
