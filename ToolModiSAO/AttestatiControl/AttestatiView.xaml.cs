using System.Windows;
using ToolModiSAO.ServiceRepository;

namespace ToolModiSAO.AttestatiControl
{
    public partial class AttestatiView : Window
    {
        public AttestatiView()
        {
            InitializeComponent();
            DataContext = new AttestatiViewModel(new Repository());
        }
    }
}