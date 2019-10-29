using Features.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Features.Models
{
    // Models returned by AccountController actions.

    public class UTRGVCredentials
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }

    public class UTRGVUserProfile
    {
        public string Email { get; set; }
        public string Cn { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string[] AdGroups { get; set; }

    }


}
