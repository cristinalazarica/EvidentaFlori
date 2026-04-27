using Modele;
using NivelStocareDate;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private readonly AdministrareFlori adminFlori = new AdministrareFlori("flori.txt");
        private readonly AdministrareClienti adminClienti = new AdministrareClienti("clienti.txt");
        private readonly AdministrareComenzi adminComenzi = new AdministrareComenzi("comenzi.txt");

        private const int MAX_LUNGIME = 15;
        private const int MAX_CANTITATE = 10000;

        public MainWindow()
        {
            InitializeComponent();
        }

        // ============================================================
        //  FLORI
        // ============================================================
        private void BtnAdaugaFloare_Click(object sender, RoutedEventArgs e)
        {
            ResetEroriFloare();
            if (ValideazaFloare() != 0) return;

            double.TryParse(txtPret.Text, out double pret);
            int.TryParse(txtStoc.Text, out int stoc);

            Culoare culoare = cmbCuloare.SelectedIndex >= 0
                ? (Culoare)cmbCuloare.SelectedIndex
                : Culoare.Rosu;

            Optiuni optiuni = Optiuni.Nimic;
            if (chkParfumata.IsChecked == true) optiuni |= Optiuni.Parfumata;
            if (chkDecorativa.IsChecked == true) optiuni |= Optiuni.Decorativa;

            Floare f = new Floare(txtNume.Text.Trim(), pret, stoc, culoare, optiuni);
            adminFlori.AdaugaFloare(f);

            ArataSucees(txtEroareFloare, "✔  Floare salvata cu succes!");
        }

        private void BtnResetFloare_Click(object sender, RoutedEventArgs e)
        {
            txtNume.Text = "";
            txtPret.Text = "";
            txtStoc.Text = "";
            cmbCuloare.SelectedIndex = -1;
            chkParfumata.IsChecked = false;
            chkDecorativa.IsChecked = false;
            txtEroareFloare.Text = "";
            ResetEroriFloare();
        }

        private int ValideazaFloare()
        {
            int cod = 0;
            txtEroareFloare.Text = "";
            txtEroareFloare.Foreground = Brushes.Red;

            // Nume
            if (string.IsNullOrWhiteSpace(txtNume.Text))
            {
                lblNume.Foreground = Brushes.Red;
                txtEroareFloare.Text += "• Numele este obligatoriu!\n";
                cod = 1;
            }
            else if (txtNume.Text.Length > MAX_LUNGIME)
            {
                lblNume.Foreground = Brushes.Red;
                txtEroareFloare.Text += $"• Numele poate avea max {MAX_LUNGIME} caractere!\n";
                cod = 1;
            }

            // Pret
            if (!double.TryParse(txtPret.Text, out double pret) || txtPret.Text.Trim() == "")
            {
                lblPret.Foreground = Brushes.Red;
                txtEroareFloare.Text += "• Pretul trebuie sa fie un numar valid!\n";
                cod = 1;
            }
            else if (pret <= 0)
            {
                lblPret.Foreground = Brushes.Red;
                txtEroareFloare.Text += "• Pretul trebuie sa fie mai mare decat 0!\n";
                cod = 1;
            }

            // Stoc
            if (!int.TryParse(txtStoc.Text, out int stoc) || txtStoc.Text.Trim() == "")
            {
                lblStoc.Foreground = Brushes.Red;
                txtEroareFloare.Text += "• Stocul trebuie sa fie un numar intreg!\n";
                cod = 1;
            }
            else if (stoc < 0)
            {
                lblStoc.Foreground = Brushes.Red;
                txtEroareFloare.Text += "• Stocul nu poate fi negativ!\n";
                cod = 1;
            }

            // Culoare
            if (cmbCuloare.SelectedIndex < 0)
            {
                lblCuloare.Foreground = Brushes.Red;
                txtEroareFloare.Text += "• Selectati o culoare!\n";
                cod = 1;
            }

            return cod;
        }

        private void ResetEroriFloare()
        {
            var negru = Brushes.Black;
            lblNume.Foreground = negru;
            lblPret.Foreground = negru;
            lblStoc.Foreground = negru;
            lblCuloare.Foreground = negru;
        }

        // ============================================================
        //  CLIENTI
        // ============================================================
        private void BtnAdaugaClient_Click(object sender, RoutedEventArgs e)
        {
            ResetEroriClient();
            if (ValideazaClient() != 0) return;

            int.TryParse(txtNrComenzi.Text, out int nrComenzi);
            Client c = new Client(txtNumeClient.Text.Trim(), nrComenzi);
            adminClienti.AdaugaClient(c);

            ArataSucees(txtEroareClient, "✔  Client salvat cu succes!");
        }

        private void BtnResetClient_Click(object sender, RoutedEventArgs e)
        {
            txtNumeClient.Text = "";
            txtNrComenzi.Text = "";
            txtEroareClient.Text = "";
            ResetEroriClient();
        }

        private int ValideazaClient()
        {
            int cod = 0;
            txtEroareClient.Text = "";
            txtEroareClient.Foreground = Brushes.Red;

            // Nume
            if (string.IsNullOrWhiteSpace(txtNumeClient.Text))
            {
                lblNumeClient.Foreground = Brushes.Red;
                txtEroareClient.Text += "• Numele clientului este obligatoriu!\n";
                cod = 1;
            }
            else if (txtNumeClient.Text.Length > MAX_LUNGIME)
            {
                lblNumeClient.Foreground = Brushes.Red;
                txtEroareClient.Text += $"• Numele poate avea max {MAX_LUNGIME} caractere!\n";
                cod = 1;
            }

            // Nr Comenzi
            if (!int.TryParse(txtNrComenzi.Text, out int nr) || txtNrComenzi.Text.Trim() == "")
            {
                lblNrComenzi.Foreground = Brushes.Red;
                txtEroareClient.Text += "• Nr. comenzi trebuie sa fie un numar intreg!\n";
                cod = 1;
            }
            else if (nr < 0)
            {
                lblNrComenzi.Foreground = Brushes.Red;
                txtEroareClient.Text += "• Nr. comenzi nu poate fi negativ!\n";
                cod = 1;
            }

            return cod;
        }

        private void ResetEroriClient()
        {
            lblNumeClient.Foreground = Brushes.Black;
            lblNrComenzi.Foreground = Brushes.Black;
        }

        // ============================================================
        //  COMENZI
        // ============================================================
        private void BtnAdaugaComanda_Click(object sender, RoutedEventArgs e)
        {
            ResetEroriComanda();
            if (ValideazaComanda() != 0) return;

            int.TryParse(txtCantitate.Text, out int cantitate);
            Comanda comanda = new Comanda(
                txtNumeClientComanda.Text.Trim(),
                txtNumeFloareComanda.Text.Trim(),
                cantitate);
            adminComenzi.AdaugaComanda(comanda);

            ArataSucees(txtEroareComanda, "✔  Comanda salvata cu succes!");
        }

        private void BtnResetComanda_Click(object sender, RoutedEventArgs e)
        {
            txtNumeClientComanda.Text = "";
            txtNumeFloareComanda.Text = "";
            txtCantitate.Text = "";
            txtEroareComanda.Text = "";
            ResetEroriComanda();
        }

        private int ValideazaComanda()
        {
            int cod = 0;
            txtEroareComanda.Text = "";
            txtEroareComanda.Foreground = Brushes.Red;

            // Nume client
            if (string.IsNullOrWhiteSpace(txtNumeClientComanda.Text))
            {
                lblNumeClientComanda.Foreground = Brushes.Red;
                txtEroareComanda.Text += "• Numele clientului este obligatoriu!\n";
                cod = 1;
            }
            else if (txtNumeClientComanda.Text.Length > MAX_LUNGIME)
            {
                lblNumeClientComanda.Foreground = Brushes.Red;
                txtEroareComanda.Text += $"• Numele clientului max {MAX_LUNGIME} caractere!\n";
                cod = 1;
            }

            // Nume floare
            if (string.IsNullOrWhiteSpace(txtNumeFloareComanda.Text))
            {
                lblNumeFloareComanda.Foreground = Brushes.Red;
                txtEroareComanda.Text += "• Numele florii este obligatoriu!\n";
                cod = 1;
            }
            else if (txtNumeFloareComanda.Text.Length > MAX_LUNGIME)
            {
                lblNumeFloareComanda.Foreground = Brushes.Red;
                txtEroareComanda.Text += $"• Numele florii max {MAX_LUNGIME} caractere!\n";
                cod = 1;
            }

            // Cantitate
            if (!int.TryParse(txtCantitate.Text, out int cant) || txtCantitate.Text.Trim() == "")
            {
                lblCantitate.Foreground = Brushes.Red;
                txtEroareComanda.Text += "• Cantitatea trebuie sa fie un numar intreg!\n";
                cod = 1;
            }
            else if (cant <= 0)
            {
                lblCantitate.Foreground = Brushes.Red;
                txtEroareComanda.Text += "• Cantitatea trebuie sa fie mai mare decat 0!\n";
                cod = 1;
            }
            else if (cant > MAX_CANTITATE)
            {
                lblCantitate.Foreground = Brushes.Red;
                txtEroareComanda.Text += $"• Cantitatea nu poate depasi {MAX_CANTITATE}!\n";
                cod = 1;
            }

            return cod;
        }

        private void ResetEroriComanda()
        {
            lblNumeClientComanda.Foreground = Brushes.Black;
            lblNumeFloareComanda.Foreground = Brushes.Black;
            lblCantitate.Foreground = Brushes.Black;
        }

        // ============================================================
        //  HELPER
        // ============================================================
        private void ArataSucees(System.Windows.Controls.TextBlock tb, string mesaj)
        {
            tb.Text = mesaj;
            tb.Foreground = new SolidColorBrush(Color.FromRgb(76, 175, 80));
        }
    }
}