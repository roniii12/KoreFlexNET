using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KoreFlex.Model
{
    public class User:IdentityUser
    {
        [MaxLength(20)]
        public string Ip { get; set; }
    }
}
