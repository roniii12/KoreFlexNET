using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class MeetingLogic : BaseLogic
    {
        public MeetingLogic(MoodyDbContext moodyContext) : base(moodyContext) { }
        public List<Meeting> GetAll()
        {
            return moodyContext.Meetings.ToList();
        }
        public IQueryable<object> getRangeMeetingByPatient(DateTime start, DateTime end, string patientId)
        {
            return moodyContext.Meetings.Where(m => m.startDate >= start && m.startDate <= end)
                .Join(moodyContext.Patients, meeting => meeting.PatientId, patient => patient.Id, (meeting, patient) => new
                {
                    meeting,
                    patientName = patient.FullName,
                    patientId = patient.Id
                }).Where(m => m.patientId == patientId)
                .Join(moodyContext.Therapists, m => m.meeting.TherapistId, t => t.Id, (m, t) => new
                {
                    meetingWithPatinent = m,
                    therapistName = t.FullName,
                }).Join(moodyContext.Rooms, m => m.meetingWithPatinent.meeting.RoomId, r => r.Id, (m, r) => new
                {
                    Date = m.meetingWithPatinent.meeting.startDate,
                    LengthOfMeet = m.meetingWithPatinent.meeting.MeetLenghtMin,
                    RoomName = r.Name,
                    PatientName = m.meetingWithPatinent.patientName,
                    TherapistName = m.therapistName
                });
        }
        public IQueryable<object> getRangeMeetingByTherapist(DateTime start, DateTime end, string therapistId)
        {
            return moodyContext.Meetings.Where(m => m.startDate >= start && m.startDate <= end)
                .Join(moodyContext.Patients, meeting => meeting.PatientId, patient => patient.Id, (meeting, patient) => new
                {
                    meeting,
                    patientName = patient.FullName,
                    patientId = patient.Id
                }).Join(moodyContext.Therapists, m => m.meeting.TherapistId, t => t.Id, (m, t) => new
                {
                    meetingWithPatinent = m,
                    therapistName = t.FullName,
                    therapistId = t.Id
                }).Where(m => m.therapistId == therapistId)
                .Join(moodyContext.Rooms, m => m.meetingWithPatinent.meeting.RoomId, r => r.Id, (m, r) => new
                {
                    Date = m.meetingWithPatinent.meeting.startDate,
                    LengthOfMeet = m.meetingWithPatinent.meeting.MeetLenghtMin,
                    RoomName = r.Name,
                    PatientName = m.meetingWithPatinent.patientName,
                    TherapistName = m.therapistName
                });
        }
        public IQueryable<object> getRangeMeetingByPatientAndTherapist(DateTime start, DateTime end, string patientId, string therapistId)
        {
            return moodyContext.Meetings.Where(m => m.startDate >= start && m.startDate <= end)
                .Join(moodyContext.Patients, meeting => meeting.PatientId, patient => patient.Id, (meeting, patient) => new
                {
                    meeting,
                    patientName = patient.FullName,
                    patientId = patient.Id
                }).Where(m => m.patientId == patientId)
                .Join(moodyContext.Therapists, m => m.meeting.TherapistId, t => t.Id, (m, t) => new
                {
                    meetingWithPatinent = m,
                    therapistName = t.FullName,
                    therapistId = t.Id
                }).Where(t=>t.therapistId == therapistId)
                .Join(moodyContext.Rooms, m => m.meetingWithPatinent.meeting.RoomId, r => r.Id, (m, r) => new
                {
                    Date = m.meetingWithPatinent.meeting.startDate,
                    LengthOfMeet = m.meetingWithPatinent.meeting.MeetLenghtMin,
                    RoomName = r.Name,
                    PatientName = m.meetingWithPatinent.patientName,
                    TherapistName = m.therapistName
                });
        }
        public IQueryable<object> getRangeMeetingByRoom(DateTime start, DateTime end, string roomId)
        {
            return moodyContext.Meetings.Where(m => m.startDate >= start && m.startDate <= end)
                .Join(moodyContext.Patients, meeting => meeting.PatientId, patient => patient.Id, (meeting, patient) => new
                {
                    meeting,
                    patientName = patient.FullName,
                    patientId = patient.Id
                }).Join(moodyContext.Rooms, m => m.meeting.RoomId, r => r.Id, (m, r) => new
                {
                    meetingWithPatinent = m,
                    roomName = r.Name,
                    roomId = r.Id
                }).Where(m => m.roomId == roomId)
                .Join(moodyContext.Therapists, m => m.meetingWithPatinent.meeting.TherapistId, t => t.Id, (m, t) => new
                {
                    Date = m.meetingWithPatinent.meeting.startDate,
                    LengthOfMeet = m.meetingWithPatinent.meeting.MeetLenghtMin,
                    RoomName = m.roomName,
                    PatientName = m.meetingWithPatinent.patientName,
                    TherapistName = t.FullName
                });
        }
        public IQueryable<object> getAllRangeMeeting(DateTime start, DateTime end)
        {
            return moodyContext.Meetings.Where(m => m.startDate >= start && m.startDate <= end)
                .Join(moodyContext.Patients, meeting => meeting.PatientId, patient => patient.Id, (meeting, patient) => new
                {
                    meeting,
                    patientName = patient.FullName,
                    patientId = patient.Id
                }).Join(moodyContext.Rooms, m => m.meeting.RoomId, r => r.Id, (m, r) => new
                {
                    meetingWithPatinent = m,
                    roomName = r.Name,
                    roomId = r.Id
                })
                .Join(moodyContext.Therapists, m => m.meetingWithPatinent.meeting.TherapistId, t => t.Id, (m, t) => new
                {
                    Date = m.meetingWithPatinent.meeting.startDate,
                    LengthOfMeet = m.meetingWithPatinent.meeting.MeetLenghtMin,
                    RoomName = m.roomName,
                    PatientName = m.meetingWithPatinent.patientName,
                    TherapistName = t.FullName
                });
        }
        public Meeting CreateMeeting(Meeting meeting)
        {
            moodyContext.Entry<Meeting>(meeting).State = EntityState.Added;
            moodyContext.SaveChanges();
            return meeting;
        }
        public void DeleteMeeting(int id)
        {
            moodyContext.Entry<Meeting>(new Meeting { Id = id }).State = EntityState.Deleted;
            moodyContext.SaveChanges();
        }
        public void UpdateMeeting(Meeting Meeting)
        {
            moodyContext.Entry<Meeting>(Meeting).State = EntityState.Modified;
            moodyContext.SaveChanges();
        }
    }
}
