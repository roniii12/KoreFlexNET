//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Threading.Tasks;

//namespace KoreFlex.Data
//{
//    [Table("RoomHours")]
//    public class RoomHour
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }

//        [MaxLength]
//        [Required]
//        public string RoomId { get; set; }
//        [Required]
//        public string TherapistId { get; set; }
//        [Required]
//        public string PatientId { get; set; }
//        [Required]
//        public DateTime startDate { get; set; }

//        [Required]
//        [MaxLength(3)]
//        public int lengthInMin { get; set; }

//        public Room Room { get; set; }

//    }
//}