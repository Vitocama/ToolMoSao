using System.Collections.Generic;
using System.Linq;
using ToolModiSAO.Models;

namespace ToolModiSAO.ServiceRepository
{
    public class Repository : IServiceRepository
    {
        public List<Personale> GetAll()
        {
            var db = new tblContext();
            return db.Personale.ToList();
        }

        public void Aggiorna(Personale personale)
        {
            var db = new tblContext();
            db.Personale.Update(personale);
            db.SaveChanges();
        }

        public void Elimina(Personale personale)
        {
            var db = new tblContext();
            db.Personale.Remove(personale);
            db.SaveChanges();
        }
    }
}