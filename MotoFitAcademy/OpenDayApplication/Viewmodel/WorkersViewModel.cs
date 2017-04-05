#region File Header & Copyright Notice
//Copyright 2017 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Input;
using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using System.Windows;

namespace OpenDayApplication.Viewmodel
{
    public class WorkersViewModel : BaseViewModel
    {
        private readonly WorkersManager _workersManager;
        private List<Worker> _workers;
        private bool _isWorkerEditVisible;
        private Worker _editedWorker;
        private CrudOperation _selectedOperation;

        public ICommand AddWorkerCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand EditWorkerCommand { get; set; }
        public ICommand DeleteWorkerCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public Worker EditedWorker
        {
            get { return _editedWorker; }
            set
            {
                _editedWorker = value;
                OnPropertyChanged("EditedWorker");
            }
        }
        public List<Worker> Workers
        {
            get { return _workers; }
            set
            {
                _workers = value;
                OnPropertyChanged("Workers");
            }
        }
        public bool IsWorkerEditVisible
        {
            get { return _isWorkerEditVisible; }
            set
            {
                _isWorkerEditVisible = value;
                OnPropertyChanged("IsWorkerEditVisible");
            }
        }

        public WorkersViewModel()
        {
            _workersManager = GetWorkersManagerr();
            AddWorkerCommand = new BaseCommand(AddWorker);
            EditWorkerCommand = new BaseCommand(EditWorker);
            DeleteWorkerCommand = new BaseCommand(DeleteWorker);
            SaveCommand = new BaseCommand(SaveChanges);
            CancelCommand = new BaseCommand(Cancel);
            RefreshWorkers();
        }

        public void AddWorker()
        {
            try
            {
                IsWorkerEditVisible = true;
                _selectedOperation = CrudOperation.Create;
                EditedWorker = new Worker();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EditWorker()
        {
            try
            {
                if (EditedWorker != null && EditedWorker.ID != 0)
                {
                    IsWorkerEditVisible = true;
                    _selectedOperation = CrudOperation.Edit;
                }
                else
                {
                    IsWorkerEditVisible = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteWorker()
        {
            try
            {
                if (MessageBox.Show("Are you sure?",
                    "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    IsWorkerEditVisible = false;
                    if (EditedWorker != null && EditedWorker.ID != 0)
                    {
                        _workersManager.DeleteWorker(EditedWorker);
                        RefreshWorkers();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

      private void SaveChanges()
        {
          try
          {
              if (EditedWorker.PESEL.Length != 11)
              {

                  const string message = "Incorrect values in PESEL";
                  const string caption = "ERROR";
                  var result = MessageBox.Show(message, caption);

              }
              else if (EditedWorker.Salary%1 != 0)
              {
                  const string message = "Incorrect values in Salary";
                  const string caption = "ERROR";
                  var result = MessageBox.Show(message, caption);

              }
              else
              {
                  switch (_selectedOperation)
                  {
                      case CrudOperation.Create:
                          _workersManager.AddWorker(EditedWorker);
                          break;
                      case CrudOperation.Edit:
                          _workersManager.EditWorker(EditedWorker);
                          break;
                  }
              }
              IsWorkerEditVisible = false;
              RefreshWorkers();
          }
           catch (Exception e)
           {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
           }
        }

        public void Cancel()
        {
            IsWorkerEditVisible = false;
            RefreshWorkers();
        }

        private void RefreshWorkers()
        {
            Workers = new List<Worker>(_workersManager.GetWorkers());
        }
    }
}
