using System.Windows;
using ToolModiSAO.ServiceRepository;
using ToolModiSAO.Services;        // ✅ DialogService
using ToolModiSAO.ModificaAttestatiControl;
using ToolModiSAO.ModificaAccountControl; // ✅ ModificaAttestatiViewModel e ModificaAttestatiView

namespace ToolModiSAO.AttestatiControl
{
    public partial class AttestatiView : Window
    {
        public AttestatiView()
        {
            InitializeComponent();
            DataContext = new AttestatiViewModel(new Repository(), new DialogService());
        }
    }
}