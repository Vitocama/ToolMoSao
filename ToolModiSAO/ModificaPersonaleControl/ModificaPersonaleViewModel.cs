using System;
using ToolModiSAO.Common;
using ToolModiSAO.Models;
using System.Windows.Input;
using ToolModiSAO.informazioneControl;
using System.Windows;

namespace ToolModiSAO.ModificaPersonaleControl
{
    public class ModificaPersonaleViewModel : CommonBase
    {
        private readonly Action<Personale> _onSalva;

        private Personale _personale;
        public Personale Personale
        {
            get => _personale;
            set { _personale = value; RaisePropertyChanged(nameof(Personale)); }
        }

        public ICommand SalvaCommand { get; }
        public ICommand AnnullaCommand { get; }

        public ModificaPersonaleViewModel(Personale personale, Action<Personale> onSalva)
        {
            _onSalva = onSalva;
            Personale = personale;

            SalvaCommand = new RelayCommand<object>(_ => Salva());
            AnnullaCommand = new RelayCommand<object>(_ => Annulla());
        }

        private void Salva()
        {
            _onSalva(Personale);
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }

        private void Annulla()
        {
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }
    }
}