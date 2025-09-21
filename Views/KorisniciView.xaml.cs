using Filmovizija.Models;
using Filmovizija.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace Filmovizija.Views
{
    public partial class KorisniciView : UserControl
    {
        private AppDbContext _context;
        public event EventHandler GotovoClicked;
        public KorisniciView()
        {
            InitializeComponent();
            _context = new AppDbContext();
            LoadData();
        }

        private void LoadData()
        {
            KorisniciDataGrid.ItemsSource = _context.Korisnici.ToList();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            var dodajKorisnikaWindow = new DodajKorisnikaWindow();

            if (dodajKorisnikaWindow.ShowDialog() == true)
            {
                var noviKorisnik = dodajKorisnikaWindow.NoviKorisnik;
                _context.Korisnici.Add(noviKorisnik);
                _context.SaveChanges();
                LoadData();
            }
        }

        private void Uredi_Click(object sender, RoutedEventArgs e)
        {
            if (KorisniciDataGrid.SelectedItem is Korisnik selectedKorisnik)
            {
                var urediKorisnikaWindow = new UrediKorisnikaWindow(selectedKorisnik);

                if (urediKorisnikaWindow.ShowDialog() == true)
                {
                    var korisnik = urediKorisnikaWindow.Korisnik;
                    _context.Korisnici.Update(korisnik);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Odaberite korisnika za uređivanje.");
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Želite li ukloniti korisnika?", "Brisanje korisnika", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (KorisniciDataGrid.SelectedItem is Korisnik selectedKorisnik && result == MessageBoxResult.Yes)
            {
                _context.Korisnici.Remove(selectedKorisnik);
                _context.SaveChanges();
                LoadData();
            }

        }

        private void Gotovo_Click(object sender, RoutedEventArgs e)
        {
            GotovoClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}