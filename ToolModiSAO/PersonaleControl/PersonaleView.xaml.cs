using GalaSoft.MvvmLight.Views;
using System.Windows;
using System.Windows.Input;
using ToolModiSAO.ModificaAccountControl;
using ToolModiSAO.ServiceRepository;


namespace ToolModiSAO.PersonaleControl
{
    public partial class PersonaleView : Window
    {
        public PersonaleView()
        {
            InitializeComponent();
            DataContext = new PersonaleViewModel(new Repository(), new DialogService());
        }

       
    }
}