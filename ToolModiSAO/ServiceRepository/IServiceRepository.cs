using System.Collections.Generic;
using ToolModiSAO.Models;

namespace ToolModiSAO.ServiceRepository
{
    public interface IServiceRepository
    {
        // ── Personale ────────────────────────────────────────────────
        List<Personale> GetAllPersonale();
        void AggiornaPersonale(Personale personale);
        void EliminaPersonale(Personale personale);

        // ── Attestati ────────────────────────────────────────────────
        List<Attestati> GetAllAttestati();
        void AggiornaAttestati(Attestati attestato);
        void EliminaAttestati(Attestati attestato);

        // ── Account ──────────────────────────────────────────────────
        List<AccountUtenti> GetAllAccount();
        void AggiornaAccount(AccountUtenti account);
        void EliminaAccount(AccountUtenti account);
    }
}