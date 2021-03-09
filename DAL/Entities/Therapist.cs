using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Therapists")]
    public class Therapist
    {
        [Required]
        [MaxLength]
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }


        [Required]
        public string UserId { get; set; }

        //[ForeignKey("id")]
        //public ApplicationIdentityDbContext User { get; set; }

        //[ForeignKey("Guid")]
        public IList<Meeting> Meetings { get; set; }
        public User User { get; set; }
    }
}
