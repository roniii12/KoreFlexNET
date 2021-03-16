using BusinessLogic;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoreFlex.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private UserManager<User> userManager;
        private TherapistLogic therapistLogic;
        private MeetingLogic meetingLogic;
        private PatientLogic patientLogic;
        private RoomLogic roomLogic;
        private RoleManager<User> roleManager;
        public PatientsController(
            TherapistLogic therapistLogic,
            UserManager<User> usermgr,
            MeetingLogic meetingLogic,
            PatientLogic patientLogic,
            RoomLogic roomLogic,
            RoleManager<User> roleManager)
        {
            //this.moodyCtx = moodyCtx;
            this.userManager = usermgr;
            this.therapistLogic = therapistLogic;
            this.meetingLogic = meetingLogic;
            this.patientLogic = patientLogic;
            this.roomLogic = roomLogic;
            this.roleManager = roleManager;
        }
        // GET: api/<PatientsController>
        [HttpGet("all")]
        public async Task<ActionResult> All()
        {
            string name = User.Identity.Name;
            //var allPatients = this.moodyCtx.Patients.Join(moodyCtx.PatientMeetings, p => p.Id, pm => pm.PatientId, (patient, meet) => new
            //{
            //    patient,
            //    meet
            //}).Join(moodyCtx.Therapists, ppm => ppm.meet.TherapistId, t => t.Id, (patientAndMeeting, therapist) => new
            //{
            //    patientName = patientAndMeeting.patient.FullName,
            //    therapistName = therapist.FullName
            //}).Where(fq => fq.therapistName == name);
            //return Ok(new
            //{
            //    allPatients = allPatients
            //});
            //Patient patient = patientLogic.CreatePatient(new Patient
            //{
            //    FullName = "Yosi",
            //    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            //});
            //Therapist therapist = therapistLogic.CreateTherapist(new Therapist
            //{
            //    FullName = "Yair",
            //    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            //});
            //Room room = roomLogic.CreateRoom(new Room
            //{
            //    Name = "Segel"
            //});
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Patient patient = patientLogic.GetPatientByUserId(userId);
            Therapist therapist = therapistLogic.GetTherapistByUserId(userId);
            Room room = roomLogic.getRoomByName("segel");
            DateTime dateLater = DateTime.Now;
            dateLater =  dateLater.AddHours(5);
            DateTime dateEarlier = DateTime.Now;
            dateEarlier = dateEarlier.AddDays(-2);
            //Meeting meetingToday = meetingLogic.CreateMeeting(new Meeting
            //{
            //    startDate = dateLater,
            //    MeetLenghtMin = 45,
            //    PatientId = patient.Id,
            //    TherapistId = therapist.Id,
            //    RoomId = room.Id
            //});
            //Meeting meetingYesterday = meetingLogic.CreateMeeting(new Meeting
            //{
            //    startDate = dateEarlier,
            //    MeetLenghtMin = 45,
            //    PatientId = patient.Id,
            //    TherapistId = therapist.Id,
            //    RoomId = room.Id
            //}); 
            //IQueryable relevantMeeting = meetingLogic.getRangeMeetingByRoom(DateTime.Now.AddDays(-2), DateTime.Now, "d2c3ba69-bbc9-4642-ace3-07cb72491d8b");
            IEnumerable<Patient> patientsList;
            if (User.IsInRole("admin"))
                patientsList = patientLogic.GetAll();    
            patientsList = patientLogic.GetAllPatientsByTherapist(userId);
            return Ok(new
            {
                meetings = patientsList
            });
        }

        // GET api/<PatientsController>/5
        //[HttpPut("createPatients")]
        //public async Task<IActionResult> CreatePatient([FromBody] RegisterPatientViewModel registerPatient)
        //{
        //    User user = new User() { UserName = registerPatient.Username, Email = registerPatient.Email };
        //    var isSucceed = await userManager.CreateAsync(user, registerPatient.Password);
        //    if (isSucceed.Succeeded)
        //    {
        //        Guid guid = Guid.NewGuid();
        //        Patient patient = new Patient() { Username = registerPatient.Username, FullName = registerPatient.FullName, Id = guid.ToString() };
        //        moodyCtx.Patients.Add(patient);
        //        moodyCtx.SaveChanges();
        //        return Ok(new
        //        {
        //            success = true
        //        });
        //    }
        //    return BadRequest();
        //}

        // POST api/<PatientsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
