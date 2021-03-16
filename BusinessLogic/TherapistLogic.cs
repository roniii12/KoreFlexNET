using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
    public class TherapistLogic : BaseLogic
    {
        public TherapistLogic(MoodyDbContext moodyContext) : base(moodyContext) { }
        public List<Therapist> GetAll()
        {
            return moodyContext.Therapists.ToList();
        }
        public Therapist GetTherapistByUserId(string userId)
        {
            return moodyContext.Therapists.FirstOrDefault(therapist => therapist.UserId == userId);
        }
        public IEnumerable<Therapist> GetAllPatientsByPatient(string patientUserId)
        {
            return moodyContext.Patients.Where(p => p.UserId == patientUserId)
                .Join(moodyContext.Meetings, p => p.Id, m => m.PatientId, (p, m) => new
                {
                    TherapistId = m.TherapistId
                }).Join(moodyContext.Therapists, m => m.TherapistId, t => t.Id, (m, t) => new Therapist()
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    UserId = t.UserId
                }).Distinct<Therapist>();
        }
        public Therapist CreateTherapist(Therapist therapist)
        {
            therapist.Id = Guid.NewGuid().ToString();
            moodyContext.Entry<Therapist>(therapist).State = EntityState.Added;
            moodyContext.SaveChanges();
            return therapist;
        }
        public void DeleteTherapist(string id)
        {
            moodyContext.Entry<Therapist>(new Therapist { Id = id }).State = EntityState.Deleted;
            moodyContext.SaveChanges();
        }
        public void UpdateTherapist(Therapist Therapist)
        {
            moodyContext.Entry<Therapist>(Therapist).State = EntityState.Modified;
            moodyContext.SaveChanges();
        }
    }
}
