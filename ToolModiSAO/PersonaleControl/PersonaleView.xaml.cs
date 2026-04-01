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

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is PersonaleViewModel vm)
                vm.ApriModificaCommand.Execute(null);
        }
    }
}