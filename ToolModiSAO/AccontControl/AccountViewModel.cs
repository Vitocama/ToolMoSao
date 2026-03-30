using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToolModiSAO.Models;
using ToolModiSAO.ServiceRepository;
using ToolModiSAO.Services;

namespace ToolModiSAO.AccontControl
{
    public class AccountViewModel : ViewModelBase
    {
        private readonly IServiceRepository _repository;
        private readonly IDialogService _dialogService;

        // ── Lista ────────────────────────────────────────────────────
        private ObservableCollection<AccountUtenti> _listaAccount;
        public ObservableCollection<AccountUtenti> ListaAccount
        {
            get => _listaAccount;
            set { _listaAccount = value; RaisePropertyChanged(nameof(ListaAccount)); }
        }

        // ── Selezione ────────────────────────────────────────────────
        private AccountUtenti _accountSelezionato;
        public AccountUtenti AccountSelezionato
        {
            get => _accountSelezionato;
            set
            {
                _accountSelezionato = value;
                RaisePropertyChanged(nameof(AccountSelezionato));
                ((RelayCommand)ApriModificaCommand).RaiseCanExecuteChanged();
            }
        }

        // ── Comando ──────────────────────────────────────────────────
        public ICommand ApriModificaCommand { get; }

        // ── Costruttore ──────────────────────────────────────────────
        public AccountViewModel(IServiceRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            ApriModificaCommand = new RelayCommand(
                execute: EseguiApriModifica,
                canExecute: () => AccountSelezionato != null
            );

            RicaricaAccount();
        }

        // ── Metodi ───────────────────────────────────────────────────
        public void RicaricaAccount()
        {
            var lista = _repository.GetAllAccount();
            ListaAccount = lista != null
                ? new ObservableCollection<AccountUtenti>(lista)
                : new ObservableCollection<AccountUtenti>();
        }

        private void EseguiApriModifica()
        {
            if (AccountSelezionato == null) return;

            _dialogService.ApriModificaAccount(AccountSelezionato, aggiornato =>
            {
                _repository.AggiornaAccount(aggiornato);
                RicaricaAccount();
            });
        }
    }
}