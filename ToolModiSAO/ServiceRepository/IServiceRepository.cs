using System.Collections.Generic;
using ToolModiSAO.Models;

namespace ToolModiSAO.ServiceRepository
{
    public interface IServiceRepository
    {
        List<Personale> GetAll();
        void Aggiorna(Personale personale);
        void Elimina(Personale personale);
    }
}