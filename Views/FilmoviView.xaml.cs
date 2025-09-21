using Filmovizija.Models;
using Filmovizija.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace Filmovizija.Views
{
    public partial class FilmoviView : UserControl
    {
        private AppDbContext _context;
        public event EventHandler GotovoClicked;
        public FilmoviView()
        {
            InitializeComponent();
            _context = new AppDbContext();
            LoadData();
        }

        private void LoadData()
        {
            FilmoviDataGrid.ItemsSource = _context.Filmovi.ToList();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            var dodajFilmWindow = new DodajFilmWindow();

            if (dodajFilmWindow.ShowDialog() == true)
            {
                var noviFilm = dodajFilmWindow.NoviFilm;
                _context.Filmovi.Add(noviFilm);
                _context.SaveChanges();
                LoadData();
            }
        }

        private void Uredi_Click(object sender, RoutedEventArgs e)
        {
            if (FilmoviDataGrid.SelectedItem is Film selectedFilm)
            {
                var urediFilmWindow = new UrediFilmWindow(selectedFilm);

                if (urediFilmWindow.ShowDialog() == true)
                {
                    var film = urediFilmWindow.Film;
                    _context.Filmovi.Update(film);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Odaberite film za uređivanje.");
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Želite li ukloniti film?", "Brisanje filma", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (FilmoviDataGrid.SelectedItem is Film selectedFilm && result == MessageBoxResult.Yes)
            {
                _context.Filmovi.Remove(selectedFilm);
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