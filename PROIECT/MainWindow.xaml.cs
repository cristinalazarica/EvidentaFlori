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

            dpDataAdaugare.SelectedDate = DateTime.Today;

            RefreshFlori();
            RefreshClienti();
            RefreshComenzi();
        }

        // ================= NAVIGARE =================

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

        // ================= FLOARE =================

        private Culoare GetCuloare()
        {
            if (cmbCuloareFlori.SelectedItem is ComboBoxItem item)
            {
                string val = item.Content?.ToString() ?? "Rosu";

                return val switch
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

        private void BtnAdaugaFloare_Click(object sender, RoutedEventArgs e)
        {
            var opt = Optiuni.Nimic;

            if (chkParfumata.IsChecked == true)
                opt |= Optiuni.Parfumata;

            if (chkDecorativa.IsChecked == true)
                opt |= Optiuni.Decorativa;

            var floare = new Floare
            {
                Nume = txtNume.Text ?? "",
                Pret = double.TryParse(txtPret.Text, out var p) ? p : 0,
                Stoc = int.TryParse(txtStoc.Text, out var s) ? s : 0,
                Culoare = GetCuloare(),
                TipFloare = (lstTipFloare.SelectedItem as ListBoxItem)?.Content?.ToString() ?? "",
                DataAdaugare = dpDataAdaugare.SelectedDate ?? DateTime.Today,
                Optiuni = opt
            };

            adminFlori.AdaugaFloare(floare);
            RefreshFlori();
        }

        private void RefreshFlori()
        {
            dgFlori.ItemsSource = null;
            dgFlori.ItemsSource = adminFlori.GetFlori();
        }

        // ================= CLIENTI =================

        private void BtnAdaugaClient_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtNrComenzi.Text, out int nr))
                return;

            var client = new Client(txtNumeClient.Text ?? "", nr);

            adminClienti.AdaugaClient(client);
            RefreshClienti();
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
                RefreshClienti();
            }
        }

        private void RefreshClienti()
        {
            lstClienti.ItemsSource = null;
            lstClienti.ItemsSource = adminClienti.GetClienti();
        }

        // ================= COMENZI =================

        private void BtnAdaugaComanda_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtCantitate.Text, out int cant);

            var comanda = new Comanda(
                txtNumeClientComanda.Text ?? "",
                txtNumeFloareComanda.Text ?? "",
                cant
            );

            adminComenzi.AdaugaComanda(comanda);
            RefreshComenzi();
        }

        private void RefreshComenzi()
        {
            dgComenzi.ItemsSource = null;
            dgComenzi.ItemsSource = adminComenzi.GetComenzi();
        }

        // ================= CAUTARE =================

        private void BtnCautareAvansata_Click(object sender, RoutedEventArgs e)
        {
            var lista = adminFlori.GetFlori();

            string nume = txtCautareNume.Text ?? "";

            if (!string.IsNullOrWhiteSpace(nume))
                lista = lista.Where(x => x.Nume.ToLower().Contains(nume.ToLower())).ToList();

            if (double.TryParse(txtCautarePret.Text, out double pret))
                lista = lista.Where(x => x.Pret <= pret).ToList();

            if (cmbCuloareCautare.SelectedItem is ComboBoxItem item)
            {
                string culoare = item.Content?.ToString() ?? "";
                lista = lista.Where(x => x.Culoare.ToString() == culoare).ToList();
            }

            dgFlori.ItemsSource = lista;
        }

   
    }
}