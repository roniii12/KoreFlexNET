using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class RoomLogic : BaseLogic
    {
        public RoomLogic(MoodyDbContext moodyContext) : base(moodyContext) { }
        public List<Room> GetAll()
        {
            return moodyContext.Rooms.ToList();
        }
        public Room getRoomByName(string name)
        {
            return moodyContext.Rooms.FirstOrDefault(r => r.Name == name);
        }
        public Room CreateRoom(Room room)
        {
            room.Id = Guid.NewGuid().ToString();
            moodyContext.Entry<Room>(room).State = EntityState.Added;
            moodyContext.SaveChanges();
            return room;
        }
        public void DeleteRoom(string id)
        {
            moodyContext.Entry<Room>(new Room { Id = id }).State = EntityState.Deleted;
            moodyContext.SaveChanges();
        }
        public void UpdateRoom(Room Room)
        {
            moodyContext.Entry<Room>(Room).State = EntityState.Modified;
            moodyContext.SaveChanges();
        }
    }
}
