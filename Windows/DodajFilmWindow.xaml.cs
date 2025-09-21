using Filmovizija.Models;
using System;
using System.Globalization;
using System.Windows;

namespace Filmovizija.Windows
{
    public partial class DodajFilmWindow : Window
    {
        public Film NoviFilm { get; private set; }

        public DodajFilmWindow()
        {
            InitializeComponent();
        }

        private void DodajFilm_Click(object sender, RoutedEventArgs e)
        {
            string naziv = NazivTextBox.Text.Trim();
            string zanr = ZanrTextBox.Text.Trim();
            string godinaText = GodinaIzdanjaTextBox.Text.Trim();
            string reziser = ReziserTextBox.Text.Trim();
            string ocjenaText = OcjenaTextBox.Text.Trim();

            if (string.IsNullOrEmpty(naziv))
            {
                MessageBox.Show("Naziv je obavezno polje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(zanr))
            {
                MessageBox.Show("Žanr je obavezno polje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(godinaText, out int godinaIzdanja))
            {
                MessageBox.Show("Godina izdanja mora biti broj.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (godinaIzdanja < 1888 || godinaIzdanja > DateTime.Now.Year)
            {
                MessageBox.Show("Unesite ispravnu godinu izdanja.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(ocjenaText.Replace(',', '.'),
                NumberStyles.Any, CultureInfo.InvariantCulture, out double ocjena))
            {
                MessageBox.Show("Ocjena mora biti broj (npr. 8.5).", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ocjena < 1 || ocjena > 10)
            {
                MessageBox.Show("Ocjena mora biti u rasponu od 1 do 10.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NoviFilm = new Film
            {
                Naziv = naziv,
                Zanr = zanr,
                GodinaIzdanja = godinaIzdanja,
                Reziser = reziser,
                Ocjena = ocjena
            };

            DialogResult = true;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
