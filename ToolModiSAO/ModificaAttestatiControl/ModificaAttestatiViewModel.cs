using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows;
using System.Windows.Input;
using ToolModiSAO.Common;
using ToolModiSAO.Models;
using ToolModiSAO.ServiceRepository;

namespace ToolModiSAO.ModificaAttestatiControl
{
    public class ModificaAttestatiViewModel : CommonBase
    {
        private readonly IServiceRepository _repository;

        private Attestati _attestato;
        public Attestati Attestato
        {
            get => _attestato;
            set { _attestato = value; RaisePropertyChanged(nameof(Attestato)); }
        }

        public ICommand SalvaCommand { get; }
        public ICommand AnnullaCommand { get; }
        public ICommand EliminaCommand { get; }

        public ModificaAttestatiViewModel(Attestati attestato, IServiceRepository repository)
        {
            Attestato = attestato ?? throw new ArgumentNullException(nameof(attestato));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            SalvaCommand = new RelayCommand(Salva);
            AnnullaCommand = new RelayCommand(Annulla);
            EliminaCommand = new RelayCommand(Elimina);
        }

        private void Salva()
        {
            _repository.AggiornaAttestati(Attestato);
            ChiudiFinestra();
        }

        private void Annulla()
        {
            ChiudiFinestra();
        }

        private void Elimina()
        {
            if (Attestato == null)
            {
                MessageBox.Show("Attestato è null!");
                return;
            }

            var esito = MessageBox.Show(
                $"Eliminare l'attestato di {Attestato.Dipendente} (Id={Attestato.Id})?",
                "Conferma eliminazione",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (esito == MessageBoxResult.Yes)
            {
                _repository.EliminaAttestati(Attestato);
                ChiudiFinestra();
            }
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