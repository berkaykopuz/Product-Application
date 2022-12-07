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
    public partial class FormÜrünListele : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-AA53TK2;Initial Catalog=StokTakip;Integrated Security=True");
        DataSet daset = new DataSet();
        public FormÜrünListele()
        {
            InitializeComponent();
        }
        private void Kategori_Getir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategori", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox3.Items.Add(read["kategori"].ToString());


            }
            baglanti.Close();

        }

        private void FormÜrünListele_Load(object sender, EventArgs e)
        {
            Ürün_Listele();
            Kategori_Getir();
            Tedarikci_Getir();
            comboBox1.Items.Add("Raf");
            comboBox1.Items.Add("Depo");
            comboBoxDepoRafAra.Items.Add("Depo");
            comboBoxDepoRafAra.Items.Add("Raf");



        }

        private void Ürün_Listele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from siparişbase", baglanti);
            adtr.Fill(daset, "siparişbase");
            dataGridView1.DataSource = daset.Tables["siparişbase"];
            baglanti.Close();
        }
        private void Tedarikci_Getir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from tedarikçi", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox2.Items.Add(read["ad"].ToString());


            }
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            

            txtBarkodNo.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            txtKategori.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            txtMarka.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            txtUrunAdi.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            txtMiktar.Text = dataGridView1.CurrentRow.Cells["miktari"].Value.ToString();
            txtAlisFiyati.Text = dataGridView1.CurrentRow.Cells["alisfiyati"].Value.ToString();
            txtSatisFiyati.Text = dataGridView1.CurrentRow.Cells["satisfiyati"].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells["tedarikcisi"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["teminyeri"].Value.ToString();


        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update siparişbase set urunadi=@urunadi, miktari=@miktari,alisfiyati=@alisfiyati,satisfiyati=@satisfiyati,tedarikcisi=@tedarikcisi,teminyeri=@teminyeri where barkodno=@barkodno ",baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@urunadi", txtUrunAdi.Text);
            komut.Parameters.AddWithValue("@miktari", int.Parse(txtMiktar.Text));
            komut.Parameters.AddWithValue("@alisfiyati", double.Parse(txtAlisFiyati.Text));
            komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatisFiyati.Text));
            komut.Parameters.AddWithValue("@tedarikcisi", comboBox2.Text);
            komut.Parameters.AddWithValue("@teminyeri", comboBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme Başarıyla Yapıldı.");

            daset.Tables["siparişbase"].Clear();
            Ürün_Listele();

            foreach(Control kontrol in this.Controls)
            {
                if(kontrol is TextBox)
                {
                    kontrol.Text = "";
                }
                if(kontrol is ComboBox)
                {
                    kontrol.Text = "";
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            comboBox4.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from marka where kategori='" + comboBox3.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox4.Items.Add(read["marka"].ToString());


            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtBarkodNo.Text != "")
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update siparişbase set kategori=@kategori, marka=@marka where barkodno=@barkodno ", baglanti);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@kategori", comboBox3.Text);
                komut.Parameters.AddWithValue("@marka", comboBox4.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme Başarıyla Yapıldı.");
            }
            else
            {
                MessageBox.Show("Barkod No Yazılı Değil.");
            }
            

            daset.Tables["siparişbase"].Clear();
            Ürün_Listele();

            foreach (Control kontrol in this.Controls)
            {
                
                if (kontrol is ComboBox)
                {
                    kontrol.Text = "";
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from siparişbase where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();


            MessageBox.Show("Kayıt Silindi.");

            daset.Tables["siparişbase"].Clear();
            Ürün_Listele();
        }

        private void txtTeminAra_TextChanged(object sender, EventArgs e)
        {
            
        }



        private void txtBarkodNoAra_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            DataTable tablo = new DataTable();

            SqlDataAdapter adtr = new SqlDataAdapter("select * from siparişbase where barkodno like '%" + txtBarkodNoAra.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void comboBoxDepoRafAra_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            DataTable tablo = new DataTable();

            SqlDataAdapter adtr = new SqlDataAdapter("select * from siparişbase where teminyeri like '%" + comboBoxDepoRafAra.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
    }
}
