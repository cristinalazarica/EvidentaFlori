using Modele;
using NivelStocareDate;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private AdministrareFlori adminFlori = new AdministrareFlori("flori.txt");
        private AdministrareClienti adminClienti = new AdministrareClienti("clienti.txt");
        private AdministrareComenzi adminComenzi = new AdministrareComenzi("comenzi.txt");

        private string? floareEdit = null;

        public MainWindow()
        {
            InitializeComponent();
            RefreshGrid();
        }

        // ================= MENIU =================
        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            panelAdmin.Visibility = Visibility.Visible;
            panelCautare.Visibility = Visibility.Collapsed;
        }

        private void BtnCauta_Click(object sender, RoutedEventArgs e)
        {
            panelAdmin.Visibility = Visibility.Collapsed;
            panelCautare.Visibility = Visibility.Visible;
        }

        // ================= FLOARE ADD / EDIT =================
        private void BtnAdaugaFloare_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCuloare.SelectedIndex < 0)
            {
                txtEroareFloare.Text = "Selecteaza culoare!";
                return;
            }

            double.TryParse(txtPret.Text, out double pret);
            int.TryParse(txtStoc.Text, out int stoc);

            Culoare culoare = (Culoare)cmbCuloare.SelectedIndex;

            Optiuni opt = Optiuni.Nimic;
            if (chkParfumata.IsChecked == true) opt |= Optiuni.Parfumata;
            if (chkDecorativa.IsChecked == true) opt |= Optiuni.Decorativa;

            Floare f = new Floare(txtNume.Text, pret, stoc, culoare, opt);

            if (floareEdit != null)
            {
                adminFlori.ModificaFloare(floareEdit, f);
                txtEroareFloare.Text = "Adaugat!";
                floareEdit = null;
            }
         
            RefreshGrid();
        }

        // ================= EDIT =================
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as System.Windows.Controls.Button;
            if (btn == null) return;

            Floare? f = btn.Tag as Floare;
            if (f == null) return;

            txtNume.Text = f.Nume;
            txtPret.Text = f.Pret.ToString();
            txtStoc.Text = f.Stoc.ToString();
            cmbCuloare.SelectedIndex = (int)f.Culoare;

            chkParfumata.IsChecked = f.Optiuni.HasFlag(Optiuni.Parfumata);
            chkDecorativa.IsChecked = f.Optiuni.HasFlag(Optiuni.Decorativa);

            floareEdit = f.Nume;
        }

        // ================= DELETE =================
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as System.Windows.Controls.Button;
            if (btn == null) return;

            Floare? f = btn.Tag as Floare;
            if (f == null) return;

            var res = MessageBox.Show(
                "Stergi floarea?",
                "Confirmare",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (res == MessageBoxResult.Yes)
            {
                adminFlori.StergeFloare(f.Nume);
                RefreshGrid();
            }
        }

        // ================= CAUTARE =================
        private void BtnCautaFloare_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtCautare.Text?.Trim() ?? "";

            var lista = adminFlori.GetFlori()
                .Where(x => x.Nume.ToLower().Contains(nume.ToLower()))
                .ToList();

            dgFlori.ItemsSource = lista;
        }

        // ================= REFRESH =================
        private void RefreshGrid()
        {
            dgFlori.ItemsSource = adminFlori.GetFlori();
        }

        // ================= CLIENT =================
        private void BtnAdaugaClient_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtNrComenzi.Text, out int nr);
            adminClienti.AdaugaClient(new Client(txtNumeClient.Text, nr));
        }

        // ================= COMANDA =================
        private void BtnAdaugaComanda_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtCantitate.Text, out int cant);

            adminComenzi.AdaugaComanda(
                new Comanda(txtNumeClientComanda.Text,
                            txtNumeFloareComanda.Text,
                            cant));
        }
    }
}