#region File Header & Copyright Notice
//Copyright 2017 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion
using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text.RegularExpressions;


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

        static Regex ValidEmailRegex = CreateValidEmailRegex();


        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        static bool isValid;
        

    public void AddClient()
    {
      IsClientEditVisible = true;
      EditedClient = new Client();



        }

    public void DeleteClient()
    {
      IsClientEditVisible = false;
      if (EditedClient != null && EditedClient.ID != 0)
      {
        _clientsManager.DeleteClient(EditedClient);
        RefreshClients();
      }
    }

    public void SaveChanges()
    {   
      _clientsManager.AddClient(EditedClient);
            EmailIsValid(EditedClient.Address);

            if (isValid == false)
            {
                System.Windows.MessageBox.Show("Invalid E-mail, type correct one!!!!");
            }
            else
            {
                IsClientEditVisible = false;


                RefreshClients();
            }
           
    }

    public void Cancel()
    {
      IsClientEditVisible = false;
    }

    private void RefreshClients()
    {
      Clients = new List<Client>(_clientsManager.GetClients());
    }

        internal static bool EmailIsValid(string emailAddress)
        {
            isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }
    }
}
