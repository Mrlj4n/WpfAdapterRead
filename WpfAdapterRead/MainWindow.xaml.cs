using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace WpfAdapterRead
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Proizvod ORDER BY NazivProizvoda", Konekcija.cnnMagacin);

            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds,"Proizvod");
                DataTable tbl = ds.Tables[0];

                foreach (DataRow red in tbl.Rows)
                {
                    Proizvod p = new Proizvod {
                        ProizvodId = (int)red[0],
                        KategorijaId = (int)red[1],
                        NazivProizvoda = red[2].ToString(),
                        Cena = (decimal)red[3],
                        KolicinaNaLageru = (int)red[4]
                    };
                    ComboBox1.Items.Add(p);
                }
            }
            catch (Exception xcp)
            {
                MessageBox.Show(xcp.Message);
            }
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Proizvod p = ComboBox1.SelectedItem as Proizvod;

            TextBlock1.Text = p.Cena.ToString();
        }
    }
}
