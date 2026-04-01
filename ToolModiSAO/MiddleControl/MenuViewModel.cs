using GalaSoft.MvvmLight.CommandWpf;
using ToolModiSAO.PersonaleControl;
using ToolModiSAO.AttestatiControl;
using ToolModiSAO.AccontControl;
using System;
using System.Windows;
using System.Windows.Input;

namespace ToolModiSAO.MenuControl
{
    public class MenuViewModel
    {
        private PersonaleView _personaleView;
        private AttestatiView _attestatiView;
        private AccountView _accountView;

        // ── Comandi ──────────────────────────────────────────────────
        public ICommand OpenHomeCommand { get; }
        public ICommand OpenPersonaleCommand { get; }
        public ICommand OpenAttestatiCommand { get; }
        public ICommand OpenAccountCommand { get; }

        // ── Proprietà ────────────────────────────────────────────────
        public bool IsMaster { get; } = true;

        // ── Costruttore ──────────────────────────────────────────────
        public MenuViewModel()
        {
            OpenHomeCommand = new RelayCommand(OpenHome);
            OpenPersonaleCommand = new RelayCommand(OpenPersonale);
            OpenAttestatiCommand = new RelayCommand(OpenAttestati);
            OpenAccountCommand = new RelayCommand(OpenAccount);
        }

        // ── Home ─────────────────────────────────────────────────────
        private void OpenHome()
        {
            // da implementare
        }

        // ── Personale ────────────────────────────────────────────────
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

        // ── Attestati ────────────────────────────────────────────────
        private void OpenAttestati()
        {
            try
            {
                if (_attestatiView == null || !_attestatiView.IsVisible)
                {
                    _attestatiView = new AttestatiView();
                    _attestatiView.Closed += (s, e) => _attestatiView = null;
                    _attestatiView.Show();
                }
                else
                {
                    if (_attestatiView.WindowState == WindowState.Minimized)
                        _attestatiView.WindowState = WindowState.Normal;
                    _attestatiView.Activate();
                    _attestatiView.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }

        // ── Account ──────────────────────────────────────────────────
        private void OpenAccount()
        {
            try
            {
                if (_accountView == null || !_accountView.IsVisible)
                {
                    _accountView = new AccountView();
                    _accountView.Closed += (s, e) => _accountView = null;
                    _accountView.Show();
                }
                else
                {
                    if (_accountView.WindowState == WindowState.Minimized)
                        _accountView.WindowState = WindowState.Normal;
                    _accountView.Activate();
                    _accountView.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }
    }
}