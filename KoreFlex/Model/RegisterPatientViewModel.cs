﻿using System.ComponentModel.DataAnnotations;

namespace KoreFlex
{
    public class RegisterPatientViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
