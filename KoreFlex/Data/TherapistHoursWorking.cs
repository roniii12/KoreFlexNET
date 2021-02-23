using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KoreFlex.Data
{
    [Table("TherapistHoursWorking")]
    public class TherapistHoursWorking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength]
        public string TherapistId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [MaxLength(3)]
        public int LengthInMin { get; set; }

        public Therapist Therapist { get; set; }
    }
}
