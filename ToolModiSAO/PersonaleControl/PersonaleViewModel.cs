using System.Collections.ObjectModel;
using ToolModiSAO.Common;
using ToolModiSAO.Models;
using ToolModiSAO.ServiceRepository;

namespace ToolModiSAO.PersonaleControl
{
    public class PersonaleViewModel : CommonBase
    {
        private readonly IServiceRepository _repository;

        private ObservableCollection<Personale> _listaPersonale;
        public ObservableCollection<Personale> ListaPersonale
        {
            get => _listaPersonale;
            set
            {
                _listaPersonale = value;
                RaisePropertyChanged(nameof(ListaPersonale));
            }
        }

        private Personale _personaleSelezionato;
        public Personale PersonaleSelezionato
        {
            get => _personaleSelezionato;
            set
            {
                _personaleSelezionato = value;
                RaisePropertyChanged(nameof(PersonaleSelezionato));
            }
        }

        public PersonaleViewModel(IServiceRepository serviceRepository)
        {
            _repository = serviceRepository;
           
             ListaPersonale = new ObservableCollection<Personale>(_repository.GetAll());
        }

        public void RicaricaPersonale()
        {
            var lista = _repository.GetAll();
            ListaPersonale = lista != null
                ? new ObservableCollection<Personale>(lista)
                : new ObservableCollection<Personale>();
        }


    }
}