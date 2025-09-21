using Filmovizija.Models;
using System.Windows;

namespace Filmovizija.Windows
{
    public partial class DodajPosudbuWindow : Window
    {
        private readonly AppDbContext _context;
        public Posudba NovaPosudba { get; private set; }

        public DodajPosudbuWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
            LoadData();
        }

        private void LoadData()
        {
            KorisnikComboBox.ItemsSource = _context.Korisnici.ToList();
            FilmComboBox.ItemsSource = _context.Filmovi.ToList();
            DatumPosudbePicker.SelectedDate = DateTime.Now.Date;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (KorisnikComboBox.SelectedItem == null)
            {
                MessageBox.Show("Odaberite korisnika.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (FilmComboBox.SelectedItem == null)
            {
                MessageBox.Show("Odaberite film.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatumPosudbePicker.SelectedDate == null)
            {
                MessageBox.Show("Odaberite datum posudbe.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var korisnik = (Korisnik)KorisnikComboBox.SelectedItem;
            var film = (Film)FilmComboBox.SelectedItem;
            var datum = DatumPosudbePicker.SelectedDate.Value;

            NovaPosudba = new Posudba
            {
                KorisnikId = korisnik.ID,
                FilmId = film.ID,
                DatumPosudbe = datum,
                Vraceno = false
            };

            DialogResult = true;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
