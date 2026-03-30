using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;
using ToolModiSAO.Common;
using ToolModiSAO.Models;
using BC = BCrypt.Net.BCrypt;

namespace ToolModiSAO.AccontControl.ModificaAccountControl
{
    public class ModificaAccountViewModel : CommonBase
    {
        private readonly Action<AccountUtenti> _onSalva;
        private const string PASSWORD_DEFAULT = "Aquilone.000";

        private AccountUtenti _account;
        public AccountUtenti Account
        {
            get => _account;
            set { _account = value; RaisePropertyChanged(nameof(Account)); }
        }

        public ICommand SalvaCommand { get; }
        public ICommand AnnullaCommand { get; }
        public ICommand ResetPasswordCommand { get; }
        public ICommand ApriCodUUooCommand { get; }

        public ModificaAccountViewModel(AccountUtenti account, Action<AccountUtenti> onSalva)
        {
            _onSalva = onSalva;
            Account = account;

            SalvaCommand = new RelayCommand<object>(_ => Salva());
            AnnullaCommand = new RelayCommand<object>(_ => Annulla());
            ResetPasswordCommand = new RelayCommand<object>(_ => ResetPassword());
            ApriCodUUooCommand = new RelayCommand<object>(_ => ApriCodUUoo());
        }

        private void Salva()
        {
            _onSalva(Account);
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }

        private void Annulla()
        {
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
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
                _onSalva(Account);
                MessageBox.Show("Password reimpostata con successo.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
            }
        }

        private void ApriCodUUoo()
        {
            var view = new CodUUooView();
            view.ShowDialog();
        }
    }
}