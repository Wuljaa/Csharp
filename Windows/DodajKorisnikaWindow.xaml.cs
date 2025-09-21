using Filmovizija.Models;
using System;
using System.Windows;

namespace Filmovizija.Windows
{
    public partial class DodajKorisnikaWindow : Window
    {
        public Korisnik NoviKorisnik { get; private set; }

        public DodajKorisnikaWindow()
        {
            InitializeComponent();
        }

        private void DodajKorisnika_Click(object sender, RoutedEventArgs e)
        {
            string ime = ImeTextBox.Text;
            string prezime = PrezimeTextBox.Text;
            
            if (string.IsNullOrEmpty(ime))
            {
                MessageBox.Show("Ime je obavezno polje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(prezime))
            {
                MessageBox.Show("Prezime je obavezno polje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            NoviKorisnik = new Korisnik
            {
                Ime = ime,
                Prezime = prezime
            };

            DialogResult = true;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}