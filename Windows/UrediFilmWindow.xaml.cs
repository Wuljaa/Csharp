using Filmovizija.Models;
using System;
using System.Windows;

namespace Filmovizija.Windows
{
    public partial class UrediFilmWindow : Window
    {
        public Film Film { get; private set; }

        public UrediFilmWindow(Film film)
        {
            InitializeComponent();
            Film = film;
            PopulateFields();
        }

        private void PopulateFields()
        {
            NazivTextBox.Text = Film.Naziv;
            ZanrTextBox.Text = Film.Zanr;
            GodinaTextBox.Text = Film.GodinaIzdanja.ToString();
            ReziserTextBox.Text = Film.Reziser;
            OcjenaTextBox.Text = Film.Ocjena.ToString();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NazivTextBox.Text))
            {
                MessageBox.Show("Naziv je obavezno polje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(ZanrTextBox.Text))
            {
                MessageBox.Show("Žanr je obavezno polje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(GodinaTextBox.Text, out int godina) || godina < 1888 || godina > DateTime.Now.Year)
            {
                MessageBox.Show("Unesite ispravnu godinu izdanja.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!double.TryParse(OcjenaTextBox.Text, out double ocjena) || ocjena < 1 || ocjena > 10)
            {
                MessageBox.Show("Ocjena mora biti broj između 1 i 10.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Film.Naziv = NazivTextBox.Text.Trim();
            Film.Zanr = ZanrTextBox.Text.Trim();
            Film.GodinaIzdanja = godina;
            Film.Reziser = ReziserTextBox.Text.Trim();
            Film.Ocjena = ocjena;

            DialogResult = true;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
