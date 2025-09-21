using Filmovizija.Models;
using Filmovizija.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Filmovizija.Views
{
    public partial class PosudbeView : UserControl
    {
        private AppDbContext _context;
        public event EventHandler GotovoClicked;

        public PosudbeView()
        {
            InitializeComponent();
            _context = new AppDbContext();
            PrikaziSvePosudbe();
        }

        private void PrikaziSvePosudbe()
        {
            PosudbeDataGrid.Columns.Clear();

            var korisnikColumn = new DataGridTemplateColumn { Header = "Korisnik" };
            var template = new DataTemplate();
            var factory = new FrameworkElementFactory(typeof(TextBlock));
            var binding = new MultiBinding { StringFormat = "{0} {1}" };
            binding.Bindings.Add(new System.Windows.Data.Binding("Korisnik.Ime"));
            binding.Bindings.Add(new System.Windows.Data.Binding("Korisnik.Prezime"));
            factory.SetBinding(TextBlock.TextProperty, binding);
            template.VisualTree = factory;
            korisnikColumn.CellTemplate = template;
            PosudbeDataGrid.Columns.Add(korisnikColumn);

            PosudbeDataGrid.Columns.Add(new DataGridTextColumn { Header = "Film", Binding = new System.Windows.Data.Binding("Film.Naziv") });
            PosudbeDataGrid.Columns.Add(new DataGridTextColumn { Header = "Datum posudbe", Binding = new System.Windows.Data.Binding("DatumPosudbe") { StringFormat = "d" } });
            PosudbeDataGrid.Columns.Add(new DataGridTextColumn { Header = "Datum povratka", Binding = new System.Windows.Data.Binding("DatumPovratka") { StringFormat = "d" } });
            PosudbeDataGrid.Columns.Add(new DataGridCheckBoxColumn { Header = "Vraćeno", Binding = new System.Windows.Data.Binding("Vraceno") });

            PosudbeDataGrid.ItemsSource = _context.Posudbe
                .Include(p => p.Korisnik)
                .Include(p => p.Film)
                .ToList();
        }

        private void PrikaziNepovracenePosudbe()
        {
            PosudbeDataGrid.Columns.Clear();
            PrikaziSvePosudbe();
            PosudbeDataGrid.ItemsSource = _context.Posudbe
                .Include(p => p.Korisnik)
                .Include(p => p.Film)
                .Where(p => !p.Vraceno)
                .ToList();
        }

        private void PrikaziNajposudjivanijeFilmove()
        {
            var najposudjivaniji = _context.Posudbe
                .Include(p => p.Film)
                .GroupBy(p => p.Film.Naziv)
                .Select(g => new
                {
                    NazivFilma = g.Key,
                    BrojPosudbi = g.Count()
                })
                .OrderByDescending(x => x.BrojPosudbi)
                .ToList();

            PosudbeDataGrid.Columns.Clear();

            PosudbeDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Film",
                Binding = new System.Windows.Data.Binding("NazivFilma"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });

            PosudbeDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Broj posudbi",
                Binding = new System.Windows.Data.Binding("BrojPosudbi"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });

            PosudbeDataGrid.ItemsSource = najposudjivaniji;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            var dodajPosudbuWindow = new DodajPosudbuWindow();

            if (dodajPosudbuWindow.ShowDialog() == true)
            {
                _context.Posudbe.Add(dodajPosudbuWindow.NovaPosudba);
                _context.SaveChanges();
                PrikaziSvePosudbe();
            }
        }

        private void Uredi_Click(object sender, RoutedEventArgs e)
        {
            if (PosudbeDataGrid.SelectedItem is Posudba selectedPosudba)
            {
                var urediPosudbuWindow = new UrediPosudbuWindow(selectedPosudba);

                if (urediPosudbuWindow.ShowDialog() == true)
                {
                    _context.Posudbe.Update(urediPosudbuWindow.PosudbaZaUredjivanje);
                    _context.SaveChanges();
                    PrikaziSvePosudbe();
                }
            }
            else
            {
                MessageBox.Show("Odaberite posudbu za uređivanje.");
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (PosudbeDataGrid.SelectedItem is Posudba selectedPosudba)
            {
                var result = MessageBox.Show("Želite li ukloniti posudbu?", "Brisanje posudbe", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Posudbe.Remove(selectedPosudba);
                    _context.SaveChanges();
                    PrikaziSvePosudbe();
                }
            }
        }

        private void Najposudjivaniji_Click(object sender, RoutedEventArgs e)
        {
            PrikaziNajposudjivanijeFilmove();
        }

        private void Nepovracene_Click(object sender, RoutedEventArgs e)
        {
            PrikaziNepovracenePosudbe();
        }

        private void PrikaziSve_Click(object sender, RoutedEventArgs e)
        {
            PrikaziSvePosudbe();
        }


        private void Gotovo_Click(object sender, RoutedEventArgs e)
        {
            GotovoClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
