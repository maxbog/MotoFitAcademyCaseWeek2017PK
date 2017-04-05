#region File Header & Copyright Notice
//Copyright 2017 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion
using System.Collections.Generic;
using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using System.Windows.Input;
using System.Windows;

namespace OpenDayApplication.Viewmodel
{
  public class RoomsViewModel : BaseViewModel
  {
    private readonly RoomsManager _roomsManager;
    private bool _isRoomEditVisible;
    private List<Room> _rooms;
    private Room _editedRoom;
    private CrudOperation _selectedOperation;

    public ICommand AddRoomCommand { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand EditRoomCommand { get; set; }
    public ICommand DeleteRoomCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public Room EditedRoom
    {
      get { return _editedRoom; }
      set
      {
        _editedRoom = value;
        OnPropertyChanged("EditedRoom");
      }
    }

        public List<Room> Rooms
        {
            get { return _rooms; }
            set
            {
                _rooms = value;
                OnPropertyChanged("Rooms");
            }
        }


        public bool IsRoomEditVisible
    {
      get { return _isRoomEditVisible; }
      set
      {
        _isRoomEditVisible = value;
        OnPropertyChanged("IsRoomEditVisible");
      }
    }

    public RoomsViewModel()
    {
      _roomsManager = GetRoomsManager();
      AddRoomCommand = new BaseCommand(AddRoom);
      EditRoomCommand = new BaseCommand(EditRoom);
      SaveCommand = new BaseCommand(SaveChanges);
      CancelCommand = new BaseCommand(Cancel);
      DeleteRoomCommand = new BaseCommand(DeleteRoom);
            RefreshRooms();
        }

    public void AddRoom()
    {
      IsRoomEditVisible = true;
      _selectedOperation = CrudOperation.Create;
      EditedRoom = new Room();
    }

    public void EditRoom()
    {
      if (EditedRoom != null && EditedRoom.ID != 0)
      {
        IsRoomEditVisible = true;
        _selectedOperation = CrudOperation.Edit;
      }
      else
      {
        IsRoomEditVisible = false;
      }
    }

    public void SaveChanges()
    {
      switch (_selectedOperation)
      {
        case CrudOperation.Create:
          _roomsManager.AddRoom(EditedRoom);
          break;
        case CrudOperation.Edit:
          _roomsManager.EditRoom(EditedRoom);
          break;
      }
      IsRoomEditVisible = false;
            RefreshRooms();
    }

        public void DeleteRoom()
        {
            if (MessageBox.Show("Are you sure?",
                "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                IsRoomEditVisible = false;
                if (EditedRoom != null && EditedRoom.ID != 0)
                {
                    _roomsManager.DeleteRoom(EditedRoom);
                    RefreshRooms();
                }
            }
        }

    public void Cancel()
    {
      IsRoomEditVisible = false;
    }

        private void RefreshRooms()
        {
            Rooms = new List<Room>(_roomsManager.GetRooms());
        }
    }
}
