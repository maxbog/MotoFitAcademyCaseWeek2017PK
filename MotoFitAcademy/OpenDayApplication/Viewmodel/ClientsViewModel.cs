#region File Header & Copyright Notice
//Copyright 2017 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion
using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

        static Regex ValidImieRegex = CreateValidImieRegex();


        private static Regex CreateValidImieRegex()
        {
            string validImiePattern = "^[a-z]{2,30}[A-Z]{2,30}$";

            return new Regex(validImiePattern, RegexOptions.IgnoreCase);
        }

        static bool isValid2;

        static Regex ValidNazwiskoRegex = CreateValidNazwiskoRegex();


        private static Regex CreateValidNazwiskoRegex()
        {
            string validNazwiskoPattern = "^[a-z]{2,30}[A-Z]{2,30}$";

            return new Regex(validNazwiskoPattern, RegexOptions.IgnoreCase);
        }

        static bool isValid3;

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
                    if (MessageBox.Show("Are you sure to delete this user?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.No)
                        return;
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

            EmailIsValid(EditedClient.Address);

            if (isValid == false)
            {
                System.Windows.MessageBox.Show("Invalid E-mail, type correct one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    _clientsManager.AddClient(EditedClient);
                }


                catch (Exception)
                {
                    MessageBox.Show("Cannot save changes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                    ImieIsValid(EditedClient.Name);
                }
                if (isValid2 == false)
                {
                    System.Windows.MessageBox.Show("Invalid Imie, type correct one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        _clientsManager.AddClient(EditedClient);
                    }

                    catch (Exception)
                    {
                        MessageBox.Show("Cannot save changes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    NazwiskoIsValid(EditedClient.Surname);

                    if (isValid3 == false)
                    {
                        System.Windows.MessageBox.Show("Invalid Nazwisko, type correct one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        try
                        {
                            _clientsManager.AddClient(EditedClient);
                        }

                        catch (Exception)
                        {
                            MessageBox.Show("Cannot save changes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        IsClientEditVisible = false;


                        RefreshClients();
                    }

                }
            }
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
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Can't get client list", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        internal bool EmailIsValid(string emailAddress)
        {
            isValid = ValidEmailRegex.IsMatch(emailAddress) &&
                      _clientsManager.GetClients().All(client => client.Address != emailAddress);

            return isValid;
        }
        internal static bool ImieIsValid(string imieAddress)
        {
            isValid2 = ValidImieRegex.IsMatch(imieAddress);

            return isValid2;
        }
         internal static bool NazwiskoIsValid(string nazwiskoAddress)
        {
            isValid2 = ValidNazwiskoRegex.IsMatch(nazwiskoAddress);

            return isValid3;
        }
    }
}