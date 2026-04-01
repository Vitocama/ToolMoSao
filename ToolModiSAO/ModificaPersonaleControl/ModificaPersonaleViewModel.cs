using System;
using ToolModiSAO.Common;
using ToolModiSAO.Models;
using System.Windows.Input;
using System.Windows;
using ToolModiSAO.ServiceRepository;
using GalaSoft.MvvmLight.CommandWpf;

namespace ToolModiSAO.ModificaPersonaleControl
{
    public class ModificaPersonaleViewModel : CommonBase
    {
        private readonly IServiceRepository _serviceRepository;

        private Personale _personale;
        public Personale Personale
        {
            get => _personale;
            set { _personale = value; RaisePropertyChanged(nameof(Personale)); }
        }

        public ICommand SalvaCommand { get; }
        public ICommand AnnullaCommand { get; }
        public ICommand EliminaCommand { get; }

        public ModificaPersonaleViewModel(Personale personale, IServiceRepository serviceRepository)
        {
            Personale = personale ?? throw new ArgumentNullException(nameof(personale));
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));

            SalvaCommand = new RelayCommand(Salva);
            AnnullaCommand = new RelayCommand(Annulla);
            EliminaCommand = new RelayCommand(Elimina);
        }

        private void Elimina()
        {
            var esito = MessageBox.Show(
                $"Eliminare {Personale.Cognome} {Personale.Nome}?",
                "Conferma eliminazione",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (esito == MessageBoxResult.Yes)
            {
                _serviceRepository.EliminaPersonale(Personale); // ← non Personale.Id
                ChiudiFinestra();
            }
        }

        private void Salva()
        {
            _serviceRepository.AggiornaPersonale(Personale);
            ChiudiFinestra();
        }

        private void Annulla()
        {
            ChiudiFinestra();
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