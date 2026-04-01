using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ToolModiSAO.Models;
using ToolModiSAO.ServiceRepository;
using ToolModiSAO.ModificaPersonaleControl;

namespace ToolModiSAO.PersonaleControl
{
    public class PersonaleViewModel : ViewModelBase
    {
        private readonly IServiceRepository _repository;
        private List<Personale> _tuttiPersonale = new List<Personale>();

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
        public PersonaleViewModel(IServiceRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            ApriModificaCommand = new RelayCommand(
                execute: EseguiApriModifica,
                canExecute: () => PersonaleSelezionato != null
            );

            CercaCommand = new RelayCommand(EseguiRicerca);

            RicaricaPersonale();
        }

        // ── Metodi ───────────────────────────────────────────────────
        public void RicaricaPersonale()
        {
            var lista = _repository.GetAllPersonale();
            _tuttiPersonale = lista ?? new List<Personale>();
            ListaPersonale = new ObservableCollection<Personale>(_tuttiPersonale);
            TestoRicerca = string.Empty;
        }

        private void EseguiRicerca()
        {
            if (string.IsNullOrWhiteSpace(TestoRicerca))
            {
                ListaPersonale = new ObservableCollection<Personale>(_tuttiPersonale);
                return;
            }

            var filtro = TestoRicerca.ToLower();
            var risultati = _tuttiPersonale.Where(p =>
                (p.Cognome?.ToLower().Contains(filtro) ?? false) ||
                (p.Nome?.ToLower().Contains(filtro) ?? false) ||
                (p.Matricola?.ToLower().Contains(filtro) ?? false) ||
                (p.GradoQualifica?.ToLower().Contains(filtro) ?? false) ||
                (p.Incarico?.ToLower().Contains(filtro) ?? false) ||
                (p.CodReparto?.ToLower().Contains(filtro) ?? false) ||
                (p.CodSezione?.ToLower().Contains(filtro) ?? false) ||
                (p.CodNucleo?.ToLower().Contains(filtro) ?? false) ||
                (p.CodUfficio?.ToLower().Contains(filtro) ?? false) ||
                (p.StatoServizio?.ToLower().Contains(filtro) ?? false)
            );

            ListaPersonale = new ObservableCollection<Personale>(risultati);
        }

        private void EseguiApriModifica()
        {
            if (PersonaleSelezionato == null) return;
            var vm = new ModificaPersonaleViewModel(PersonaleSelezionato, _repository);
            var view = new ModificaPersonaleView { DataContext = vm };
            view.ShowDialog();
            RicaricaPersonale();
        }
    }
}