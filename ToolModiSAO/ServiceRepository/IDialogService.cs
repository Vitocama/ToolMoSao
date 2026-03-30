using System;
using ToolModiSAO.Models;

namespace ToolModiSAO.Services
{
    public interface IDialogService
    {
        void ApriModificaPersonale(Personale personale, Action<Personale> onSalvataggio);
        void ApriModificaAttestato(Attestati attestati, Action<Attestati> onSalvataggio);
        void ApriModificaAccount(AccountUtenti account, Action<AccountUtenti> onSalvataggio);
    }
}