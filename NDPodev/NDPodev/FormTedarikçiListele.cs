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
    public partial class FormTedarikçiListele : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-AA53TK2;Initial Catalog=StokTakip;Integrated Security=True");
        DataSet daset = new DataSet();
        public FormTedarikçiListele()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FormTedarikçiListele_Load(object sender, EventArgs e)
        {
            Kayıt_Göster();
        }

        private void Kayıt_Göster()
        {
            baglanti.Open();

            SqlDataAdapter adtr = new SqlDataAdapter("select * from tedarikçi", baglanti);
            adtr.Fill(daset, "tedarikçi");

            dataGridView1.DataSource = daset.Tables["tedarikçi"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNo.Text = dataGridView1.CurrentRow.Cells["no"].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells["ad"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
        }

        private void txtNoAra_TextChanged(object sender, EventArgs e)
        {

            baglanti.Open();
            DataTable tablo = new DataTable();

            SqlDataAdapter adtr = new SqlDataAdapter("select * from tedarikçi where no like '%" + txtNoAra.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tedarikçi set ad=@ad, telefon=@telefon, adres=@adres where no=@no", baglanti);

            komut.Parameters.AddWithValue("@no", txtNo.Text);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);

            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();


            MessageBox.Show("Tedarikçi Kaydı Güncellendi.");
            daset.Tables["tedarikçi"].Clear();
            Kayıt_Göster();

            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from tedarikçi where no='" + dataGridView1.CurrentRow.Cells["no"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();


            MessageBox.Show("Kayıt Silindi.");

            daset.Tables["tedarikçi"].Clear();
            Kayıt_Göster();
        }
    }
}
