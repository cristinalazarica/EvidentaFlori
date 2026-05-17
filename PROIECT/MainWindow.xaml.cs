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
        private AdministrareFlori adminFlori = new AdministrareFlori("flori.txt");
        private AdministrareClienti adminClienti = new AdministrareClienti("clienti.txt");
        private AdministrareComenzi adminComenzi = new AdministrareComenzi("comenzi.txt");

        private string? floareEdit = null;

        public MainWindow()
        {
            InitializeComponent();

            dpDataAdaugare.SelectedDate = DateTime.Today;
            RefreshGrid();
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            panelAdmin.Visibility = Visibility.Visible;
            panelCautare.Visibility = Visibility.Collapsed;
        }

        private void BtnCauta_Click(object sender, RoutedEventArgs e)
        {
            panelAdmin.Visibility = Visibility.Collapsed;
            panelCautare.Visibility = Visibility.Visible;
            RefreshGrid();
        }

        private void BtnAdaugaFloare_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNume.Text))
            {
                txtEroareFloare.Text = "Introdu numele florii!";
                return;
            }

            if (!double.TryParse(txtPret.Text, out double pret))
            {
                txtEroareFloare.Text = "Pret invalid!";
                return;
            }

            if (!int.TryParse(txtStoc.Text, out int stoc))
            {
                txtEroareFloare.Text = "Stoc invalid!";
                return;
            }

            if (cmbCuloare.SelectedIndex < 0)
            {
                txtEroareFloare.Text = "Selecteaza culoare!";
                return;
            }

            if (lstTipFloare.SelectedItem == null)
            {
                txtEroareFloare.Text = "Selecteaza tipul florii!";
                return;
            }

            Culoare culoare = (Culoare)cmbCuloare.SelectedIndex;

            Optiuni opt = Optiuni.Nimic;

            if (chkParfumata.IsChecked == true)
            {
                opt |= Optiuni.Parfumata;
            }

            if (chkDecorativa.IsChecked == true)
            {
                opt |= Optiuni.Decorativa;
            }

            ListBoxItem itemSelectat = (ListBoxItem)lstTipFloare.SelectedItem;
            string tipFloare = itemSelectat.Content?.ToString() ?? string.Empty;

            DateTime dataAdaugare = dpDataAdaugare.SelectedDate ?? DateTime.Today;

            Floare floare = new Floare(
                txtNume.Text,
                pret,
                stoc,
                culoare,
                opt,
                tipFloare,
                dataAdaugare
            );

            if (floareEdit != null)
            {
                adminFlori.ModificaFloare(floareEdit, floare);
                txtEroareFloare.Text = "Floare modificata!";
                floareEdit = null;
            }
            else
            {
                adminFlori.AdaugaFloare(floare);
                txtEroareFloare.Text = "Floare adaugata!";
            }

            CurataCampuriFloare();
            RefreshGrid();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;
            if (btn == null)
            {
                return;
            }

            Floare? floare = btn.Tag as Floare;
            if (floare == null)
            {
                return;
            }

            panelAdmin.Visibility = Visibility.Visible;
            panelCautare.Visibility = Visibility.Collapsed;
            panelAdmin.SelectedIndex = 0;

            txtNume.Text = floare.Nume;
            txtPret.Text = floare.Pret.ToString();
            txtStoc.Text = floare.Stoc.ToString();

            cmbCuloare.SelectedIndex = (int)floare.Culoare;

            chkParfumata.IsChecked = floare.Optiuni.HasFlag(Optiuni.Parfumata);
            chkDecorativa.IsChecked = floare.Optiuni.HasFlag(Optiuni.Decorativa);

            dpDataAdaugare.SelectedDate = floare.DataAdaugare;

            lstTipFloare.SelectedIndex = -1;

            foreach (object obj in lstTipFloare.Items)
            {
                ListBoxItem? item = obj as ListBoxItem;

                if (item != null && item.Content != null && item.Content.ToString() == floare.TipFloare)
                {
                    lstTipFloare.SelectedItem = item;
                    break;
                }
            }

            floareEdit = floare.Nume;
            txtEroareFloare.Text = "Modifici floarea selectata.";
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;
            if (btn == null)
            {
                return;
            }

            Floare? floare = btn.Tag as Floare;
            if (floare == null)
            {
                return;
            }

            MessageBoxResult rezultat = MessageBox.Show(
                "Stergi floarea?",
                "Confirmare",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (rezultat == MessageBoxResult.Yes)
            {
                adminFlori.StergeFloare(floare.Nume);
                RefreshGrid();
            }
        }

        private void BtnCautaFloare_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtCautare.Text?.Trim() ?? string.Empty;

            var lista = adminFlori.GetFlori()
                .Where(f => f.Nume.ToLower().Contains(nume.ToLower()))
                .ToList();

            dgFlori.ItemsSource = null;
            dgFlori.ItemsSource = lista;
        }

        private void RefreshGrid()
        {
            dgFlori.ItemsSource = null;
            dgFlori.ItemsSource = adminFlori.GetFlori();
        }

        private void CurataCampuriFloare()
        {
            txtNume.Clear();
            txtPret.Clear();
            txtStoc.Clear();

            cmbCuloare.SelectedIndex = -1;
            lstTipFloare.SelectedIndex = -1;

            chkParfumata.IsChecked = false;
            chkDecorativa.IsChecked = false;

            dpDataAdaugare.SelectedDate = DateTime.Today;
        }

        private void BtnAdaugaClient_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtNrComenzi.Text, out int nrComenzi);

            Client client = new Client(txtNumeClient.Text, nrComenzi);
            adminClienti.AdaugaClient(client);

            txtEroareClient.Text = "Client adaugat!";
        }

        private void BtnAdaugaComanda_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtCantitate.Text, out int cantitate);

            Comanda comanda = new Comanda(
                txtNumeClientComanda.Text,
                txtNumeFloareComanda.Text,
                cantitate
            );

            adminComenzi.AdaugaComanda(comanda);

            txtEroareComanda.Text = "Comanda adaugata!";
        }
    }
}