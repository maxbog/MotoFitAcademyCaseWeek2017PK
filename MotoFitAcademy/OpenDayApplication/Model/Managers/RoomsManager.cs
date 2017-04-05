#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Collections.Generic;
using System.Linq;
using OpenDayApplication.Model.Database;
using System;
using System.Windows;

namespace OpenDayApplication.Model.Managers
{
  public class RoomsManager
  {
    public List<Room> GetRooms()
    {
      var _rooms = new List<Room>();
      using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
      {
           try { _rooms = dataContext.Rooms.ToList(); }
           catch (System.Exception e)
           {
               System.Windows.MessageBox.Show("Room name can't be get");
           }
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
            try
            {
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                  {
                    var roomToEdit = dataContext.Rooms.FirstOrDefault(r => r.ID == room.ID);
                    roomToEdit.Name = room.Name;
                    roomToEdit.Capacity = room.Capacity;
                    dataContext.SubmitChanges();
                  }
            }
            catch (Exception e)
            {

                const string message = "Nie udało się. Spróbuj ponownie!";
                const string caption = "Błąd";
                var result = MessageBox.Show(message, caption);

                // If the no button was pressed ...

            }
        }
        public void DeleteRoom(Room room)
        {
            try
            {
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                {
                    dataContext.Rooms.Attach(room);
                    dataContext.Rooms.DeleteOnSubmit(room);
                    dataContext.SubmitChanges();
                }
            }
            catch (Exception e)
            {

                const string message = "Nie udało się. Spróbuj ponownie!";
                const string caption = "Błąd";
                var result = MessageBox.Show(message, caption);

                // If the no button was pressed ...

            }
           
        }
  }
}
