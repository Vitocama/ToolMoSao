using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToolModiSAO.Models;
using ToolModiSAO.ServiceRepository;
using ToolModiSAO.Services; // IDialogService custom

namespace ToolModiSAO.PersonaleControl
{
    public class PersonaleViewModel : ViewModelBase
    {
        private readonly IServiceRepository _repository;
        private readonly IDialogService _dialogService;

        // ── Lista personale ──────────────────────────────────────────
        private ObservableCollection<Personale> _listaPersonale;
        public ObservableCollection<Personale> ListaPersonale
        {
            get => _listaPersonale;
            set { _listaPersonale = value; RaisePropertyChanged(nameof(ListaPersonale)); }
        }

        // ── Selezione ────────────────────────────────────────────────
        private Personale _personaleSelezionato;
        public Personale PersonaleSelezionato
        {
            get => _personaleSelezionato;
            set
            {
                _personaleSelezionato = value;
                RaisePropertyChanged(nameof(PersonaleSelezionato));
                // aggiorna lo stato abilitato del comando
                ((RelayCommand)ApriModificaCommand).RaiseCanExecuteChanged();
            }
        }

        // ── Comando doppio click ─────────────────────────────────────
        public ICommand ApriModificaCommand { get; }

        // ── Costruttore unico ────────────────────────────────────────
        public PersonaleViewModel(IServiceRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            // RelayCommand MvvmLight: primo param = execute, secondo = canExecute
            ApriModificaCommand = new RelayCommand(
                execute: EseguiApriModifica,
                canExecute: () => PersonaleSelezionato != null
            );

            RicaricaPersonale();
        }

        // ── Metodi ───────────────────────────────────────────────────
        public void RicaricaPersonale()
        {
            var lista = _repository.GetAllPersonale();
            ListaPersonale = lista != null
                ? new ObservableCollection<Personale>(lista)
                : new ObservableCollection<Personale>();
        }

        private void EseguiApriModifica()
        {
            if (PersonaleSelezionato == null) return;

            _dialogService.ApriModificaPersonale(PersonaleSelezionato, aggiornato =>
            {
                _repository.AggiornaPersonale(aggiornato);
                RicaricaPersonale();
            });
        }
    }
}