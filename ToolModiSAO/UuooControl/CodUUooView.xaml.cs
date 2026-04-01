using System.Windows;
using ToolModiSAO.UuooControl;

namespace ToolModiSAO.UuooControl
{
    public partial class CodUUooView : Window
    {
        public CodUUooView()
        {
            InitializeComponent();
            DataContext = new UuooViewModel();
        }
    }
}