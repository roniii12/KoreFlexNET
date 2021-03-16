using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PatientLogic : BaseLogic
    {
        public PatientLogic(MoodyDbContext moodyContext) : base(moodyContext) { }
        public IEnumerable<Patient> GetAll()
        {
            return moodyContext.Patients.ToList();
        }
        public Patient GetPatientByUserId(string userId)
        {
            return moodyContext.Patients.FirstOrDefault(patient => patient.UserId == userId);
        }
        public IEnumerable<Patient> GetAllPatientsByTherapist(string therapistUserId)
        {
            return moodyContext.Therapists.Where(t => t.UserId == therapistUserId)
                .Join(moodyContext.Meetings, t => t.Id, m => m.TherapistId, (t, m) => new
                {
                    PatientId = m.PatientId
                }).Join(moodyContext.Patients, m => m.PatientId, p => p.Id, (m, p) => new Patient() 
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    UserId = p.UserId
                }).Distinct<Patient>();
        }
        public Patient CreatePatient(Patient patient)
        {
            patient.Id = Guid.NewGuid().ToString();
            moodyContext.Entry<Patient>(patient).State = EntityState.Added;
            moodyContext.SaveChanges();
            return patient;
        }
        public void DeletePatient(string id)
        {
            moodyContext.Entry<Patient>(new Patient { Id = id }).State = EntityState.Deleted;
            moodyContext.SaveChanges();
        }
        public void UpdatePatient(Patient patient)
        {
            moodyContext.Entry<Patient>(patient).State = EntityState.Modified;
            moodyContext.SaveChanges();
        }
    }
}
