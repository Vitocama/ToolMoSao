using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ToolModiSAO.AccontControl.ModificaAccountControl;
using ToolModiSAO.Models;
using ToolModiSAO.ServiceRepository;

namespace ToolModiSAO.AccontControl
{
    public class AccountViewModel : ViewModelBase
    {
        private readonly IServiceRepository _repository;
        private List<AccountUtenti> _tuttiAccount = new List<AccountUtenti>();

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

        // ── Testo ricerca ────────────────────────────────────────────
        private string _testoRicerca;
        public string TestoRicerca
        {
            get => _testoRicerca;
            set { _testoRicerca = value; RaisePropertyChanged(nameof(TestoRicerca)); }
        }

        // ── Comandi ──────────────────────────────────────────────────
        public ICommand ApriModificaCommand { get; }
        public ICommand CercaCommand { get; }

        // ── Costruttore ──────────────────────────────────────────────
        public AccountViewModel(IServiceRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            ApriModificaCommand = new RelayCommand(
                execute: EseguiApriModifica,
                canExecute: () => AccountSelezionato != null
            );

            CercaCommand = new RelayCommand(EseguiRicerca);

            RicaricaAccount();
        }

        // ── Metodi ───────────────────────────────────────────────────
        public void RicaricaAccount()
        {
            var lista = _repository.GetAllAccount();
            _tuttiAccount = lista ?? new List<AccountUtenti>();
            ListaAccount = new ObservableCollection<AccountUtenti>(_tuttiAccount);
            TestoRicerca = string.Empty;
        }

        private void EseguiRicerca()
        {
            if (string.IsNullOrWhiteSpace(TestoRicerca))
            {
                ListaAccount = new ObservableCollection<AccountUtenti>(_tuttiAccount);
                return;
            }

            var filtro = TestoRicerca.ToLower();
            var risultati = _tuttiAccount.Where(a =>
                (a.Utente?.ToLower().Contains(filtro) ?? false) ||
                (a.Cognome?.ToLower().Contains(filtro) ?? false) ||
                (a.Nome?.ToLower().Contains(filtro) ?? false) ||
                (a.Matricola?.ToLower().Contains(filtro) ?? false) ||
                (a.Incarico?.ToLower().Contains(filtro) ?? false) ||
                (a.Ruolo?.ToLower().Contains(filtro) ?? false) ||
                (a.Reparto?.ToLower().Contains(filtro) ?? false) ||
                (a.Sezione?.ToLower().Contains(filtro) ?? false) ||
                (a.Nucleo?.ToLower().Contains(filtro) ?? false)
            );

            ListaAccount = new ObservableCollection<AccountUtenti>(risultati);
        }

        private void EseguiApriModifica()
        {
            if (AccountSelezionato == null) return;
            var vm = new ModificaAccountViewModel(AccountSelezionato, _repository);
            var view = new ModificaAccountView { DataContext = vm };
            view.ShowDialog();
            RicaricaAccount();
        }
    }
}