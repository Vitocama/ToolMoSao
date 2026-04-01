using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ToolModiSAO.Models;
using ToolModiSAO.ModificaAttestatiControl;
using ToolModiSAO.ServiceRepository;

namespace ToolModiSAO.AttestatiControl
{
    public class AttestatiViewModel : ViewModelBase
    {
        private readonly IServiceRepository _repository;
        private List<Attestati> _tuttiAttestati = new List<Attestati>();

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
        public AttestatiViewModel(IServiceRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            ApriModificaCommand = new RelayCommand(
                execute: EseguiApriModifica,
                canExecute: () => AttesatoSelezionato != null
            );

            CercaCommand = new RelayCommand(EseguiRicerca);

            RicaricaAttestati();
        }

        // ── Metodi ───────────────────────────────────────────────────
        public void RicaricaAttestati()
        {
            var lista = _repository.GetAllAttestati();
            _tuttiAttestati = lista ?? new List<Attestati>();
            ListaAttestati = new ObservableCollection<Attestati>(_tuttiAttestati);
            TestoRicerca = string.Empty;
        }

        private void EseguiRicerca()
        {
            if (string.IsNullOrWhiteSpace(TestoRicerca))
            {
                ListaAttestati = new ObservableCollection<Attestati>(_tuttiAttestati);
                return;
            }

            var filtro = TestoRicerca.ToLower();
            var risultati = _tuttiAttestati.Where(a =>
                (a.MatricolaDipendente?.ToLower().Contains(filtro) ?? false) ||
                (a.Dipendente?.ToLower().Contains(filtro) ?? false) ||
                (a.TitoloCorso?.ToLower().Contains(filtro) ?? false) ||
                (a.AttivitaFormativa?.ToLower().Contains(filtro) ?? false) ||
                (a.MateriaCorso?.ToLower().Contains(filtro) ?? false) ||
                (a.TipologiaCorso?.ToLower().Contains(filtro) ?? false) ||
                (a.EnteFormatore?.ToLower().Contains(filtro) ?? false) ||
                (a.Reparto?.ToLower().Contains(filtro) ?? false) ||
                (a.Sezione?.ToLower().Contains(filtro) ?? false) ||
                (a.Nucleo?.ToLower().Contains(filtro) ?? false) ||
                (a.CodUfficio?.ToLower().Contains(filtro) ?? false) ||
                (a.Note?.ToLower().Contains(filtro) ?? false)
            );

            ListaAttestati = new ObservableCollection<Attestati>(risultati);
        }

        private void EseguiApriModifica()
        {
            if (AttesatoSelezionato == null) return;
            var vm = new ModificaAttestatiViewModel(AttesatoSelezionato, _repository);
            var view = new ModificaAttestatiView { DataContext = vm };
            view.ShowDialog();
            RicaricaAttestati();
        }
    }
}