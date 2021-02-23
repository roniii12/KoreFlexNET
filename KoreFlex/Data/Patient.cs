using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KoreFlex.Data
{
    [Table("Patients")]
    public class Patient
    {
        [Key]
        [Required]
        [MaxLength]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        //[ForeignKey("id")]
        //public ApplicationIdentityDbContext User { get; set; }
        
        //[ForeignKey("Guid")]
        public IList<PatientMeeting> PatientMeetings { get; set; }
        public IList<UnhandledCancelMeeting> unhandledCancelMeetings{ get; set; }

    }
}
