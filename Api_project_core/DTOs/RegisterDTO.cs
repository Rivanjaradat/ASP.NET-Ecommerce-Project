using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_project_core.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public  string UserName { get; set; }
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int Gov_Code { get; set; }
        [Required]
        public int City_Code { get; set; }
        [Required]

        public int Zone_Code { get; set; }
        [Required]

        public int Cus_classId { get; set; }
    }
}
