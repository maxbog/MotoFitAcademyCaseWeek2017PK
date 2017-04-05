﻿#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using OpenDayApplication.Model.Database;

namespace OpenDayApplication.Model.Managers
{
    public class WorkersManager
    {
        public List<Worker> GetWorkers()
        {
            var _workers = new List<Worker>();
            try
            {

                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                {
                    _workers = dataContext.Workers.ToList();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return _workers;
        }
        public void AddWorker(Worker worker)
        {
            try
            {
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                {
                    dataContext.Workers.InsertOnSubmit(worker);
                    dataContext.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteWorker(Worker worker)
        {
            try
            {
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                {
                    dataContext.Workers.Attach(worker);
                    dataContext.Workers.DeleteOnSubmit(worker);
                    dataContext.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void EditWorker(Worker worker)
        {
            try
            {
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                {
                    var workerToEdit = dataContext.Workers.FirstOrDefault(w => w.ID == worker.ID);
                    workerToEdit.Name = worker.Name;
                    workerToEdit.Surname = worker.Surname;
                    dataContext.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
