//using DAL;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace KoreFlex.Controller
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PatientsController : ControllerBase
//    {
//        private KoreFlexDb moodyCtx;
//        private UserManager<User> userManager;
//        public PatientsController(
//            KoreFlexDb moodyCtx,
//            UserManager<User> usermgr)
//        {
//            this.moodyCtx = moodyCtx;
//            this.userManager = usermgr;
//        }
//        // GET: api/<PatientsController>
//        [HttpGet("all")]
//        public async Task<ActionResult> All()
//        {
//            string name = User.Identity.Name;
//            var allPatients = this.moodyCtx.Patients.Join(moodyCtx.PatientMeetings, p => p.Id, pm => pm.PatientId, (patient, meet) => new
//            {
//                patient,
//                meet
//            }).Join(moodyCtx.Therapists, ppm => ppm.meet.TherapistId, t => t.Id, (patientAndMeeting, therapist) => new
//            {
//                patientName = patientAndMeeting.patient.FullName,
//                therapistName = therapist.FullName
//            }).Where(fq => fq.therapistName == name);
//            return Ok(new
//            {
//                allPatients = allPatients
//            });
//        }

//        // GET api/<PatientsController>/5
//        //[HttpPut("createPatients")]
//        //public async Task<IActionResult> CreatePatient([FromBody] RegisterPatientViewModel registerPatient)
//        //{
//        //    User user = new User() { UserName = registerPatient.Username, Email = registerPatient.Email };
//        //    var isSucceed = await userManager.CreateAsync(user, registerPatient.Password);
//        //    if (isSucceed.Succeeded)
//        //    {
//        //        Guid guid = Guid.NewGuid();
//        //        Patient patient = new Patient() { Username = registerPatient.Username, FullName = registerPatient.FullName, Id = guid.ToString() };
//        //        moodyCtx.Patients.Add(patient);
//        //        moodyCtx.SaveChanges();
//        //        return Ok(new
//        //        {
//        //            success = true
//        //        });
//        //    }
//        //    return BadRequest();
//        //}

//        // POST api/<PatientsController>
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/<PatientsController>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<PatientsController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//    public class test
//    {
//        public string Username { get; set; }
//    }
//    public class test2
//    {
//        public string Password { get; set; }
//    }
//}
