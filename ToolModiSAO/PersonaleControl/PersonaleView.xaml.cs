using System.Windows;
using System.Windows.Input;
using ToolModiSAO.ServiceRepository;

namespace ToolModiSAO.PersonaleControl
{
    public partial class PersonaleView : Window
    {
        public PersonaleView()
        {
            InitializeComponent();
            DataContext = new PersonaleViewModel(new Repository());
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as PersonaleViewModel;
            if (vm?.PersonaleSelezionato == null) return;

            var modificaView = new ModificaPersonaleControl.ModificaPersonaleView
            {
                DataContext = new ModificaPersonaleControl.ModificaPersonaleViewModel(
                    vm.PersonaleSelezionato,
                    personaleAggiornato =>
                    {
                        new Repository().Aggiorna(personaleAggiornato);
                        vm.RicaricaPersonale(); // ✅ parentesi aggiunte
                    })
            };
            modificaView.ShowDialog();
        }
    }
}