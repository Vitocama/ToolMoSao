using GalaSoft.MvvmLight.CommandWpf;
using ToolModiSAO.PersonaleControl;
using System;
using System.Windows;
using System.Windows.Input;
using RelayCommand = GalaSoft.MvvmLight.CommandWpf.RelayCommand;

namespace ToolModiSAO.MenuControl
{
    public class MenuViewModel
    {
        private PersonaleView _personaleView;
        public ICommand OpenPersonaleCommand { get; }

        public MenuViewModel()
        {
            OpenPersonaleCommand = new RelayCommand(OpenPersonale);
        }

        private void OpenPersonale()
        {
            try
            {
                if (_personaleView == null || !_personaleView.IsVisible)
                {
                    _personaleView = new PersonaleView();
                    _personaleView.Closed += (s, e) => _personaleView = null;
                    _personaleView.Show();
                }
                else
                {
                    if (_personaleView.WindowState == WindowState.Minimized)
                        _personaleView.WindowState = WindowState.Normal;
                    _personaleView.Activate();
                    _personaleView.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }
    }
}