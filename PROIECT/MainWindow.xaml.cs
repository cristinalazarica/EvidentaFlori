using Modele;
using NivelStocareDate;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private readonly AdministrareFlori adminFlori = new("flori.txt");
        private readonly AdministrareClienti adminClienti = new("clienti.txt");
        private readonly AdministrareComenzi adminComenzi = new("comenzi.txt");

        public MainWindow()
        {
            InitializeComponent();
            RefreshAll();
        }

        private void RefreshAll()
        {
            lstClienti.ItemsSource = adminClienti.GetClienti();
            dgComenzi.ItemsSource = adminComenzi.GetComenzi();
            dgFloriCautare.ItemsSource = adminFlori.GetFlori();
        }

        // NAVIGARE
        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            panelAdmin.Visibility = Visibility.Visible;
            panelCautare.Visibility = Visibility.Collapsed;
        }

        private void BtnCauta_Click(object sender, RoutedEventArgs e)
        {
            panelAdmin.Visibility = Visibility.Collapsed;
            panelCautare.Visibility = Visibility.Visible;

            dgFloriCautare.ItemsSource = adminFlori.GetFlori();
        }

        // FLOARE
        private void BtnAdaugaFloare_Click(object sender, RoutedEventArgs e)
        {
            var floare = new Floare
            {
                Nume = txtNume.Text ?? "",
                Pret = double.TryParse(txtPret.Text, out double p) ? p : 0,
                Stoc = int.TryParse(txtStoc.Text, out int s) ? s : 0,
                Culoare = GetCuloare(),
                TipFloare = (lstTipFloare.SelectedItem as ListBoxItem)?.Content?.ToString() ?? "",
                DataAdaugare = DateTime.Now
            };

            adminFlori.AdaugaFloare(floare);
            dgFloriCautare.ItemsSource = adminFlori.GetFlori();
        }

        private Culoare GetCuloare()
        {
            if (cmbCuloareFlori.SelectedItem is ComboBoxItem item)
            {
                return item.Content.ToString() switch
                {
                    "Rosu" => Culoare.Rosu,
                    "Alb" => Culoare.Alb,
                    "Galben" => Culoare.Galben,
                    "Mov" => Culoare.Mov,
                    _ => Culoare.Rosu
                };
            }
            return Culoare.Rosu;
        }

        // CLIENTI
        private void BtnAdaugaClient_Click(object sender, RoutedEventArgs e)
        {
            var c = new Client(
                txtNumeClient.Text ?? "",
                int.TryParse(txtNrComenzi.Text, out int nr) ? nr : 0
            );

            adminClienti.AdaugaClient(c);
            lstClienti.ItemsSource = adminClienti.GetClienti();
        }

        private void BtnEditClient_Click(object sender, RoutedEventArgs e)
        {
            if (lstClienti.SelectedItem is Client c)
            {
                txtNumeClient.Text = c.Nume;
                txtNrComenzi.Text = c.NrComenzi.ToString();
            }
        }

        private void BtnStergeClient_Click(object sender, RoutedEventArgs e)
        {
            if (lstClienti.SelectedItem is Client c)
            {
                adminClienti.StergeClient(c.Nume);
                lstClienti.ItemsSource = adminClienti.GetClienti();
            }
        }

        // COMENZI
        private void BtnAdaugaComanda_Click(object sender, RoutedEventArgs e)
        {
            var comanda = new Comanda(
                txtNumeClientComanda.Text ?? "",
                txtNumeFloareComanda.Text ?? "",
                int.TryParse(txtCantitate.Text, out int c) ? c : 0
            );

            adminComenzi.AdaugaComanda(comanda);
            dgComenzi.ItemsSource = adminComenzi.GetComenzi();
        }

        // CAUTARE
        private void BtnCautareAvansata_Click(object sender, RoutedEventArgs e)
        {
            var lista = adminFlori.GetFlori();

            if (!string.IsNullOrWhiteSpace(txtCautareNume.Text))
            {
                lista = lista
                    .Where(x => x.Nume.ToLower().Contains(txtCautareNume.Text.ToLower()))
                    .ToList();
            }

            if (double.TryParse(txtCautarePret.Text, out double pret))
            {
                lista = lista.Where(x => x.Pret <= pret).ToList();
            }

            if (cmbCuloareCautare.SelectedItem is ComboBoxItem item)
            {
                string culoare = item.Content.ToString();
                lista = lista.Where(x => x.Culoare.ToString() == culoare).ToList();
            }

            dgFloriCautare.ItemsSource = lista;
        }
    }
}