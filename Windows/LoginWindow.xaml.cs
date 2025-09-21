using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Filmovizija.Models;

namespace Filmovizija.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            try
            {
                if (ProvjeriLogin(username, password))
                {
                    var homeWindow = new HomeWindow(username);
                    homeWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Netočno korisničko ime ili lozinka.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške: {ex.Message}");
                Debug.WriteLine(ex.ToString());
            }
        }

        private static bool ProvjeriLogin(string username, string password)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var admin = context.Admini.FirstOrDefault(a => a.Username == username);

                    if (admin == null)
                    {
                        Debug.WriteLine("Korisnik nije pronađen");
                        return false;
                    }

                    if (admin.Lozinka == password)
                    {
                        Debug.WriteLine("Lozinka točna");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Pogrešna lozinka");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Došlo je do greške prilikom provjere prijave: {ex.Message}");
                throw new Exception("Greška prilikom pristupa bazi podataka.", ex);
            }
        }
    }
}
