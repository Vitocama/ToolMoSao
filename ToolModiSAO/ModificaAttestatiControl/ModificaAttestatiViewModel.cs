using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows;
using System.Windows.Input;
using ToolModiSAO.Common;
using ToolModiSAO.Models;

namespace ToolModiSAO.ModificaAttestatiControl
{
    public class ModificaAttestatiViewModel : CommonBase
    {
        private readonly Action<Attestati> _onSalva;

        private Attestati _attestato;
        public Attestati Attestato
        {
            get => _attestato;
            set { _attestato = value; RaisePropertyChanged(nameof(Attestato)); }
        }

        public ICommand SalvaCommand { get; }
        public ICommand AnnullaCommand { get; }

        public ModificaAttestatiViewModel(Attestati attestato, Action<Attestati> onSalva)
        {
            _onSalva = onSalva;
            Attestato = attestato;

            SalvaCommand = new RelayCommand<object>(_ => Salva());
            AnnullaCommand = new RelayCommand<object>(_ => Annulla());
        }

        private void Salva()
        {
            _onSalva(Attestato);
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }

        private void Annulla()
        {
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }
    }
}