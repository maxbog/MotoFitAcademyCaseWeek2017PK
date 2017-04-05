#region File Header & Copyright Notice
//Copyright 2017 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Collections.Generic;
using System.Windows.Input;
using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using System.Windows;

namespace OpenDayApplication.Viewmodel
{
  public class ClassesViewModel : BaseViewModel
  {
    private readonly ClassesManager _classesManager;
    private List<Class> _classes;
    private bool _isClassEditVisible;
    private Class _editedClass;
    private CrudOperation _selectedOperation;

    public ICommand AddClassCommand { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand EditClassCommand { get; set; }
    public ICommand DeleteClassCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public Class EditedClass
    {
      get { return _editedClass; }
      set
      {
        _editedClass = value;
        OnPropertyChanged("EditedClass");
      }
    }
    public List<Class> Classes
    {
      get { return _classes; }
      set
      {
        _classes = value;
        OnPropertyChanged("Classes");
      }
    }
    public bool IsClassEditVisible
    {
      get { return _isClassEditVisible; }
      set
      {
        _isClassEditVisible = value;
        OnPropertyChanged("IsClassEditVisible");
      }
    }

    public ClassesViewModel()
    {
      _classesManager = GetClassesManager();
      AddClassCommand = new BaseCommand(AddClass);
      EditClassCommand = new BaseCommand(EditClass);
      DeleteClassCommand = new BaseCommand(DeleteClass);
      SaveCommand = new BaseCommand(SaveChanges);
      CancelCommand = new BaseCommand(Cancel);
      RefreshClients();
    }

    public void AddClass()
    {
                IsClassEditVisible = true;
                _selectedOperation = CrudOperation.Create;
                EditedClass = new Class();
       
    }

    public void EditClass()
    {
      if (EditedClass != null && EditedClass.ID != 0)
      {
        IsClassEditVisible = true;
        _selectedOperation = CrudOperation.Edit;
      }
      else
      {
        IsClassEditVisible = false;
      }
    }

    public void DeleteClass()
    {
      IsClassEditVisible = false;
      if (EditedClass != null && EditedClass.ID != 0)
      {
                MessageBoxResult messageBoxConfirm = MessageBox.Show("Are you sure to delete this class?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                if (messageBoxConfirm == MessageBoxResult.Yes)
                     _classesManager.DeleteClass(EditedClass);
        RefreshClients();
      }
    }

    public void SaveChanges()
    {
      switch (_selectedOperation)
      {
        case CrudOperation.Create:
          _classesManager.AddClass(EditedClass);
          break;
        case CrudOperation.Edit:
          _classesManager.EditClass(EditedClass);
          break;
      }
      IsClassEditVisible = false;
      RefreshClients();
    }

    public void Cancel()
    {
      IsClassEditVisible = false;
    }

    private void RefreshClients()
    {
      Classes = new List<Class>(_classesManager.GetClasses());
    }
  }
}
