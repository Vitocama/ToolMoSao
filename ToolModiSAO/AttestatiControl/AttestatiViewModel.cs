using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToolModiSAO.Models;
using ToolModiSAO.ServiceRepository;
using ToolModiSAO.Services;

namespace ToolModiSAO.AttestatiControl
{
    public class AttestatiViewModel : ViewModelBase
    {
        private readonly IServiceRepository _repository;
        private readonly IDialogService _dialogService;

        // ── Lista ────────────────────────────────────────────────────
        private ObservableCollection<Attestati> _listaAttestati;
        public ObservableCollection<Attestati> ListaAttestati
        {
            get => _listaAttestati;
            set { _listaAttestati = value; RaisePropertyChanged(nameof(ListaAttestati)); }
        }

        // ── Selezione ────────────────────────────────────────────────
        private Attestati _attestatoSelezionato;
        public Attestati AttesatoSelezionato
        {
            get => _attestatoSelezionato;
            set
            {
                _attestatoSelezionato = value;
                RaisePropertyChanged(nameof(AttesatoSelezionato));
                ((RelayCommand)ApriModificaCommand).RaiseCanExecuteChanged();
            }
        }

        // ── Comando doppio click ─────────────────────────────────────
        public ICommand ApriModificaCommand { get; }

        // ── Costruttore ──────────────────────────────────────────────
        public AttestatiViewModel(IServiceRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            ApriModificaCommand = new RelayCommand(
                execute: EseguiApriModifica,
                canExecute: () => AttesatoSelezionato != null
            );

            RicaricaAttestati();
        }

        // ── Metodi ───────────────────────────────────────────────────
        public void RicaricaAttestati()
        {
            var lista = _repository.GetAllAttestati();
            ListaAttestati = lista != null
                ? new ObservableCollection<Attestati>(lista)
                : new ObservableCollection<Attestati>();
        }

        private void EseguiApriModifica()
        {


            if (AttesatoSelezionato == null) return;

            _dialogService.ApriModificaAttestato(AttesatoSelezionato, aggiornato =>
            {
                _repository.AggiornaAttestati(aggiornato);
                RicaricaAttestati();
            });
        }

    }
}
