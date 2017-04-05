#region File Header & Copyright Notice
//Copyright 2017 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion
using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows;


namespace OpenDayApplication.Viewmodel
{
  public class RoomsViewModel : BaseViewModel
  {
    private readonly RoomsManager _roomsManager;
    private bool _isRoomEditVisible;
    private Room _editedRoom;
    private CrudOperation _selectedOperation;

    public ICommand AddRoomCommand { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand EditRoomCommand { get; set; }
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
    public bool IsRoomEditVisible
    {
      get { return _isRoomEditVisible; }
      set
      {
        _isRoomEditVisible = value;
        OnPropertyChanged("IsRoomEditVisible");
      }
    }




        static Regex ValidCapacityRegex = CreateValidCapRegex();


        private static Regex CreateValidCapRegex()
        {
            string validCapPattern = @"^[1-9][0-9]?$|^100$";
                

            return new Regex(validCapPattern, RegexOptions.IgnoreCase);
        }

        static bool isValid;

        internal static bool CapIsValid(string capacity)
        {
            isValid = ValidCapacityRegex.IsMatch(capacity);

            return isValid;
        }




        public RoomsViewModel()
    {
      _roomsManager = GetRoomsManager();
      AddRoomCommand = new BaseCommand(AddRoom);
      EditRoomCommand = new BaseCommand(EditRoom);
      SaveCommand = new BaseCommand(SaveChanges);
      CancelCommand = new BaseCommand(Cancel);
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

                    CapIsValid(System.Convert.ToString(EditedRoom.Capacity));

                    if (isValid == false)
                    {
                        System.Windows.MessageBox.Show("Invalid Capacity, type correct one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else   _roomsManager.AddRoom(EditedRoom);
                    break;

        case CrudOperation.Edit:

                    CapIsValid(System.Convert.ToString(EditedRoom.Capacity));

                    if (isValid == false)
                    {
                        System.Windows.MessageBox.Show("Invalid Capacity, type correct one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else _roomsManager.EditRoom(EditedRoom);
                    break;
      }
      IsRoomEditVisible = false;
    }

    public void Cancel()
    {
      IsRoomEditVisible = false;
    }
  }
}
