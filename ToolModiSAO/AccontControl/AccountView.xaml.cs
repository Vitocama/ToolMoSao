using System.Windows;
using ToolModiSAO.ModificaAccountControl;
using ToolModiSAO.ServiceRepository;
using ToolModiSAO.Services;

namespace ToolModiSAO.AccontControl
{
    public partial class AccountView : Window
    {
        public AccountView()
        {
            InitializeComponent();
            DataContext = new AccountViewModel(new Repository(), new DialogService());
        }
    }
}