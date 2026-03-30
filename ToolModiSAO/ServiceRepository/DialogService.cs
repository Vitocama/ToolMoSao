using System;
using ToolModiSAO.AccontControl.ModificaAccountControl;
using ToolModiSAO.Models;
using ToolModiSAO.ModificaAccountControl;
using ToolModiSAO.ModificaAttestatiControl;
using ToolModiSAO.ModificaPersonaleControl;
using ToolModiSAO.Services;

namespace ToolModiSAO.ModificaAccountControl
{

    public class DialogService : IDialogService
    {
        public void ApriModificaPersonale(Personale personale, Action<Personale> onSalvataggio)
        {
            var vm = new ModificaPersonaleViewModel(personale, onSalvataggio);
            var view = new ModificaPersonaleView { DataContext = vm };
            view.ShowDialog();
        }

        public void ApriModificaAttestato(Attestati attestati, Action<Attestati> onSalvataggio)
        {
            var vm = new ModificaAttestatiViewModel(attestati, onSalvataggio);
            var view = new ModificaAttestatiView { DataContext = vm };
            view.ShowDialog();
        }

        public void ApriModificaAccount(AccountUtenti account, Action<AccountUtenti> onSalvataggio)
        {
            var vm = new ModificaAccountViewModel(account, onSalvataggio);
            var view = new ModificaAccountView { DataContext = vm };
            view.ShowDialog();
        }
    }
}