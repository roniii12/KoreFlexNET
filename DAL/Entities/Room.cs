using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class Room
    {
        [Key]
        [MaxLength]
        public string Id {get;set;}

        [MaxLength(70)]
        [Required]
        public string Name { get; set; }

        public IList<Meeting> Meetings { get; set; }
    }
}
