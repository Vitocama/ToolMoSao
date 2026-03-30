using System.Windows;
using ToolModiSAO.UuooControl;

namespace ToolModiSAO.UuooControl
{
    public partial class UuooView : Window
    {
        public UuooView()
        {
            InitializeComponent();
            DataContext = new UuooViewModel();
        }
    }
}