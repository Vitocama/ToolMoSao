using System.Collections.Generic;
using System.Linq;
using ToolModiSAO.Models;

namespace ToolModiSAO.ServiceRepository
{
    public class Repository : IServiceRepository
    {
        #region Personale
        public List<Personale> GetAllPersonale()
        {
            var db = new tblContext();
            return db.Personale.ToList();
        }
        public void AggiornaPersonale(Personale personale)
        {
            var db = new tblContext();
            db.Personale.Update(personale);
            db.SaveChanges();
        }
        public void EliminaPersonale(Personale personale)
        {
            var db = new tblContext();
            db.Personale.Remove(personale);
            db.SaveChanges();
        }
        #endregion

        public void EliminaAttestati(Attestati attestato)
        {
            var db = new tblContext();
            db.Attestati.Remove(attestato);
            db.SaveChanges();
        }

        #region Attestati
        public List<Attestati> GetAllAttestati()
        {
            var db = new tblContext();
            return db.Attestati.OrderBy(x => x.Dipendente).ToList();
        }
        public void AggiornaAttestati(Attestati attestato)
        {
            var db = new tblContext();
            db.Attestati.Update(attestato);
            db.SaveChanges();
        }
       
        #endregion

        #region Account
        public List<AccountUtenti> GetAllAccount()
        {
            var db = new tblContext();
            return db.AccountUtenti.OrderBy(x => x.Cognome).ToList();
        }
        public void AggiornaAccount(AccountUtenti account)
        {
            var db = new tblContext();
            db.AccountUtenti.Update(account);
            db.SaveChanges();
        }
        public void EliminaAccount(AccountUtenti account)
        {
            var db = new tblContext();
            db.AccountUtenti.Remove(account);
            db.SaveChanges();
        }
        #endregion
    }
}