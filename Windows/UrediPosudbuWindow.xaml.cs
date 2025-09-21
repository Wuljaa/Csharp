using Filmovizija.Models;
using System;
using System.Windows;

namespace Filmovizija.Windows
{
    public partial class UrediPosudbuWindow : Window
    {
        public Posudba PosudbaZaUredjivanje { get; private set; }

        public UrediPosudbuWindow(Posudba posudba)
        {
            InitializeComponent();
            PosudbaZaUredjivanje = posudba;
            PopulateFields();
        }

        private void PopulateFields()
        {
            KorisnikTextBox.Text = PosudbaZaUredjivanje.Korisnik?.Ime + " " + PosudbaZaUredjivanje.Korisnik?.Prezime;
            FilmTextBox.Text = PosudbaZaUredjivanje.Film?.Naziv;
            DatumPosudbeTextBox.Text = PosudbaZaUredjivanje.DatumPosudbe.ToShortDateString();

            VracenoCheckBox.IsChecked = PosudbaZaUredjivanje.Vraceno;
            DatumPovratkaPicker.SelectedDate = PosudbaZaUredjivanje.DatumPovratka;
            DatumPovratkaPicker.IsEnabled = PosudbaZaUredjivanje.Vraceno;
        }

        private void VracenoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DatumPovratkaPicker.IsEnabled = true;
            if (DatumPovratkaPicker.SelectedDate == null)
                DatumPovratkaPicker.SelectedDate = DateTime.Now;
        }

        private void VracenoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DatumPovratkaPicker.IsEnabled = false;
            DatumPovratkaPicker.SelectedDate = null;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            PosudbaZaUredjivanje.Vraceno = VracenoCheckBox.IsChecked ?? false;
            PosudbaZaUredjivanje.DatumPovratka = DatumPovratkaPicker.SelectedDate;

            DialogResult = true;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
