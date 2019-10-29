using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Cn { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public int RoleId { get; set; }


    }
}
