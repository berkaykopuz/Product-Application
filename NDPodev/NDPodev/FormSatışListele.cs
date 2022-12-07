using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDPodev
{
    public partial class FormSatışListele : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-AA53TK2;Initial Catalog=StokTakip;Integrated Security=True");
        DataSet daset = new DataSet();
        public FormSatışListele()
        {
            InitializeComponent();
        }

        private void Satis_Listele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis", baglanti);
            adtr.Fill(daset, "satis");
            dataGridView1.DataSource = daset.Tables["satis"];
            
            baglanti.Close();
        }

        private void FormSatışListele_Load(object sender, EventArgs e)
        {
            Satis_Listele();
        }
    }
}
