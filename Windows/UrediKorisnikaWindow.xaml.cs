using Filmovizija.Models;
using System;
using System.Windows;

namespace Filmovizija.Windows
{
    public partial class UrediKorisnikaWindow : Window
    {
        public Korisnik Korisnik;

        public UrediKorisnikaWindow(Korisnik korisnik)
        {
            InitializeComponent();
            this.Korisnik = korisnik;
            PopulateFields();
        }

        private void PopulateFields()
        {
            ImeTextBox.Text = Korisnik.Ime;
            PrezimeTextBox.Text = Korisnik.Prezime;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ImeTextBox.Text))
            {
                MessageBox.Show("Ime je obavezno polje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(PrezimeTextBox.Text))
            {
                MessageBox.Show("Prezime je obavezno polje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Korisnik.Ime = ImeTextBox.Text;
            Korisnik.Prezime = PrezimeTextBox.Text;

            DialogResult = true;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}