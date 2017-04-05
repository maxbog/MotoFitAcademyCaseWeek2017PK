﻿#region File Header & Copyright Notice
//Copyright 2017 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion
using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace OpenDayApplication.Viewmodel
{
  public class ClientsViewModel : BaseViewModel
  {
    private readonly ClientsManager _clientsManager;
    private List<Client> _clients;
    private bool _isClientEditVisible;
    private Client _editedClient;

    public ICommand AddClientCommand { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand DeleteClientCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public Client EditedClient
    {
      get { return _editedClient; }
      set
      {
        _editedClient = value;
        OnPropertyChanged("EditedClient");
      }
    }
    public List<Client> Clients
    {
      get { return _clients; }
      set
      {
        _clients = value;
        OnPropertyChanged("Clients");
      }
    }
    public bool IsClientEditVisible
    {
      get { return _isClientEditVisible; }
      set
      {
        _isClientEditVisible = value;
        OnPropertyChanged("IsClientEditVisible");
      }
    }

    public ClientsViewModel()
    {
      _clientsManager = GetClientsManager();
      AddClientCommand = new BaseCommand(AddClient);
      DeleteClientCommand = new BaseCommand(DeleteClient);
      SaveCommand = new BaseCommand(SaveChanges);
      CancelCommand = new BaseCommand(Cancel);
      RefreshClients();
    }

    public void AddClient()
    {
      IsClientEditVisible = true;
      EditedClient = new Client();
    }

    public void DeleteClient()
        {
            try
            {
                IsClientEditVisible = false;
                if (EditedClient != null && EditedClient.ID != 0)
                {
                    _clientsManager.DeleteClient(EditedClient);
                    RefreshClients();
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Can't delete client", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }

        }

    public void SaveChanges()
    {
      _clientsManager.AddClient(EditedClient);
      IsClientEditVisible = false;
      RefreshClients();
    }

    public void Cancel()
    {
      IsClientEditVisible = false;
    }

    private void RefreshClients()
    {
            try
            {
                Clients = new List<Client>(_clientsManager.GetClients());
            }
            catch(Exception)
            {
                System.Windows.MessageBox.Show("Can't get client list", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
    }
  }
}
