using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoreFlex
{
    public class MeetingDetailsViewModel
    {
        public string TherapistName { get; set; }
        public string PatientName { get; set; }
        public string RoomName { get; set; }
        public DateTime date { get; set; }
        public int lengthOfMeeting { get; set; }
    }
}
