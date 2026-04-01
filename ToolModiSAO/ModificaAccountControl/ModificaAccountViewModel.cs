using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;
using ToolModiSAO.Common;
using ToolModiSAO.Models;
using ToolModiSAO.ServiceRepository;
using ToolModiSAO.UuooControl;
using BC = BCrypt.Net.BCrypt;

namespace ToolModiSAO.AccontControl.ModificaAccountControl
{
    public class ModificaAccountViewModel : CommonBase
    {
        private readonly IServiceRepository _repository;
        private const string PASSWORD_DEFAULT = "Aquilone.000";

        private AccountUtenti _account;
        public AccountUtenti Account
        {
            get => _account;
            set { _account = value; RaisePropertyChanged(nameof(Account)); }
        }

        public ICommand SalvaCommand { get; }
        public ICommand AnnullaCommand { get; }
        public ICommand EliminaCommand { get; }
        public ICommand ResetPasswordCommand { get; }
        public ICommand ApriCodUUooCommand { get; }

        public ModificaAccountViewModel(AccountUtenti account, IServiceRepository repository)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            SalvaCommand = new RelayCommand(Salva);
            AnnullaCommand = new RelayCommand(Annulla);
            EliminaCommand = new RelayCommand(Elimina);
            ResetPasswordCommand = new RelayCommand(ResetPassword);
            ApriCodUUooCommand = new RelayCommand(ApriCodUUoo);
        }

        private void Elimina()
        {
            if (Account == null)
            {
                MessageBox.Show("Attestato è null!");
                return;
            }

            var esito = MessageBox.Show(
                $"Eliminare l'attestato di {Account.Nome} (Id={Account.Cognome})?",
                "Conferma eliminazione",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (esito == MessageBoxResult.Yes)
            {
                _repository.EliminaAccount(Account);
                ChiudiFinestra();
            }
        }

        private void Salva()
        {
            _repository.AggiornaAccount(Account);
            ChiudiFinestra();
        }

        private void Annulla()
        {
            ChiudiFinestra();
        }

        private void ResetPassword()
        {
            var risultato = MessageBox.Show(
                $"Reimposta la password di '{Account.Utente}' a '{PASSWORD_DEFAULT}'?",
                "Reset Password",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (risultato == MessageBoxResult.Yes)
            {
                Account.Password = BC.HashPassword(PASSWORD_DEFAULT);
                _repository.AggiornaAccount(Account);
                MessageBox.Show("Password reimpostata con successo.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                ChiudiFinestra();
            }
        }

        private void ApriCodUUoo()
        {
            var view = new CodUUooView();
            view.ShowDialog();
        }

        private void ChiudiFinestra()
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w.DataContext == this)
                {
                    w.Close();
                    break;
                }
            }
        }
    }
}