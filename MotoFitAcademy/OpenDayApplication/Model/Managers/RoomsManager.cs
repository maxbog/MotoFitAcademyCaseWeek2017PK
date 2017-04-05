#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Collections.Generic;
using System.Linq;
using OpenDayApplication.Model.Database;

namespace OpenDayApplication.Model.Managers
{
  public class RoomsManager
  {
    public List<Room> GetRooms()
    {
      var _rooms = new List<Room>();
      using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
      {
        _rooms = dataContext.Rooms.ToList();
      }
      return _rooms;
    }
    public void AddRoom(Room room)
    {
      using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
      {
        dataContext.Rooms.InsertOnSubmit(room);
        try { dataContext.SubmitChanges(); }
        catch (System.Exception e)
        {
            System.Windows.MessageBox.Show("Room name can't be empty");
        }
               
      
      }
    }
    public void EditRoom(Room room)
    {
      using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
      {
        var roomToEdit = dataContext.Rooms.FirstOrDefault(r => r.ID == room.ID);
        roomToEdit.Name = room.Name;
        roomToEdit.Capacity = room.Capacity;
        dataContext.SubmitChanges();
      }
    }
  }
}
