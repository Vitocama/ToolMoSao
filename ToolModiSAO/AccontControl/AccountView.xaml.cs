using System.Windows;
using ToolModiSAO.ServiceRepository;


namespace ToolModiSAO.AccontControl
{
    public partial class AccountView : Window
    {
        public AccountView()
        {
            InitializeComponent();
            DataContext = new AccountViewModel(new Repository());
        }
    }
}