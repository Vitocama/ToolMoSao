using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using ToolModiSAO.Dati;

namespace ToolModiSAO.UuooControl
{
    public class CodiceUUoo
    {
        public string Denominazione { get; set; }
        public int Codice { get; set; }
    }

    public class UuooViewModel : ViewModelBase
    {
        public ObservableCollection<CodiceUUoo> ListaCodici { get; }

        public UuooViewModel()
        {
            var dati = new Cod_UUOO();
            ListaCodici = new ObservableCollection<CodiceUUoo>();

            foreach (var item in dati.reparti)
            {
                ListaCodici.Add(new CodiceUUoo
                {
                    Denominazione = item.Key,
                    Codice = item.Value
                });
            }
        }
    }
}