using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestTestWebApp.Models
{
    public class UserModel
    {
        public long Id { get; set; }

        [Required()]
        public string Nombre { get; set; }
        
        [Required(), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
