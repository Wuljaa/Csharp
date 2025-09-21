using System;
using System.Windows;
using Filmovizija.Models;
using Filmovizija.Views;

namespace Filmovizija.Windows
{
    public partial class HomeWindow : Window
    {
        private string username;
        public HomeWindow(string username)
        {
            InitializeComponent();
            this.username = username;
            PrikaziKorisnickoIme();
        }

        private void PrikaziKorisnickoIme()
        {
            UsernameTextBlock.Text = $"Logiran kao: {username}!";
        }
        private void BtnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            var korisniciView = new KorisniciView();
            korisniciView.GotovoClicked += GotovoClicked;
            ContentArea.Content = korisniciView;
        }

        private void BtnFilmovi_Click(object sender, RoutedEventArgs e)
        {
            var filmoviView = new FilmoviView();
            filmoviView.GotovoClicked += GotovoClicked;
            ContentArea.Content = filmoviView;
        }

        private void BtnPosudbe_Click(object sender, RoutedEventArgs e)
        {
             var posudbeView = new PosudbeView();
             posudbeView.GotovoClicked += GotovoClicked;
             ContentArea.Content = posudbeView;
        }

        private void GotovoClicked(object sender, EventArgs e)
        {
            ContentArea.Content = null;
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Jeste li sigurni da se želite odjaviti?", "Odjava", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

    }
}
