using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DAL
{
    public class User:IdentityUser
    {
        [MaxLength(45)]
        public string Ip { get; set; }

        public Therapist Therapist { get; set; }
        public Patient Patient { get; set; }
    }
}
