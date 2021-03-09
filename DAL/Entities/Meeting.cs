using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Meetings")]
    public class Meeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength]
        public string PatientId { get; set; }

        [Required]
        [MaxLength]
        public string TherapistId { get; set; }

        [Required]
        [MaxLength]
        public string RoomId { get; set; }

        [Required]
        public DateTime startDate { get; set; } 

        [Required]
        [MaxLength(3)]
        public int MeetLenghtMin { get; set; }

        public Therapist Therapist { get; set; }
        public Patient Patient { get; set; }
        public Room Room { get; set; }

    }
}
