using System.Windows.Controls;
using ToolModiSAO.MenuControl;

namespace ToolModiSAO.MenuControl
{
    public partial class MenuView : UserControl
{
    public MenuView()
    {
        InitializeComponent();
        DataContext = new MenuViewModel(); // deve essere MenuViewModel, non altro
    }
}
    }