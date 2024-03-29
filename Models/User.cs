using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Cn { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        [Required, ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role {get; set;}


    }
}
