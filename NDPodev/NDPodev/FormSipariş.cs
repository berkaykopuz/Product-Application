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
    public partial class FormSipariş : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-AA53TK2;Initial Catalog=StokTakip;Integrated Security=True");
        bool durum;
        public FormSipariş()
        {
            InitializeComponent();
        }
        private void Barkod_Kontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from siparişbase", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                if (txtBarkodNo.Text == read["barkodno"].ToString() || txtBarkodNo.Text == "" )
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void Kategori_Getir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategori", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBoxKategori.Items.Add(read["kategori"].ToString());


            }
            baglanti.Close();

        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void FormSipariş_Load(object sender, EventArgs e)
        {
            comboBoxTeminYeri.Items.Add("Raf");
            comboBoxTeminYeri.Items.Add("Depo");


            Kategori_Getir();
            Tedarikci_Getir();
        }

        private void comboBoxKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMarka.Items.Clear();
            comboBoxMarka.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from marka where kategori='"+comboBoxKategori.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBoxMarka.Items.Add(read["marka"].ToString());


            }
            baglanti.Close();
        }
        private void Tedarikci_Getir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from tedarikçi", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBoxTedarikcisi.Items.Add(read["ad"].ToString());


            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Barkod_Kontrol();
            if(durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into siparişbase(barkodno,kategori,marka,urunadi,miktari,alisfiyati,satisfiyati,tedarikcisi,tarih,teminyeri) values(@barkodno,@kategori,@marka,@urunadi,@miktari,@alisfiyati,@satisfiyati,@tedarikcisi,@tarih,@teminyeri)", baglanti);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@kategori", comboBoxKategori.Text);
                komut.Parameters.AddWithValue("@marka", comboBoxMarka.Text);
                komut.Parameters.AddWithValue("@urunadi", txtUrunAdi.Text);
                komut.Parameters.AddWithValue("@miktari", int.Parse(txtMiktar.Text));
                komut.Parameters.AddWithValue("@alisfiyati", double.Parse(txtAlisFiyati.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatisFiyati.Text));
                komut.Parameters.AddWithValue("@tedarikcisi", comboBoxTedarikcisi.Text);
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.Parameters.AddWithValue("@teminyeri", comboBoxTeminYeri.Text);


                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yeni Ürün Başarıyla Sipariş Edildi.");
            }
            else
            {
                MessageBox.Show("Bu ürün zaten bulunmaktadır. Sağ taraftan mevcut ürüne ekleme yapabilirsiniz :)");
            }
            foreach(Control kontrol in groupBox1.Controls)
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

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (textBox12.Text == "")
            {
                lblMiktari.Text = "";
                foreach(Control kontrol in groupBox2.Controls)
                {
                    if(kontrol is TextBox)
                    {
                        kontrol.Text = "";
                    }
                }
            }


            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from siparişbase where barkodno like '"+textBox12.Text+"'",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox1.Text = read["kategori"].ToString();
                textBox3.Text = read["marka"].ToString();
                textBox11.Text = read["urunadi"].ToString();
                lblMiktari.Text = "Mevcut Miktar: ";
                lblMiktari.Text += read["miktari"].ToString();
                textBox9.Text = read["alisfiyati"].ToString();
                textBox8.Text = read["satisfiyati"].ToString();
                textBox2.Text = read["tedarikcisi"].ToString();
                textBox4.Text = read["teminyeri"].ToString();
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update siparişbase set miktari=miktari+'" + int.Parse(textBox10.Text) + "'where barkodno='"+textBox12.Text+"' ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Var Olan Ürünün Yeni Siparişi Başarıyla Yapıldı.");
            foreach (Control kontrol in groupBox2.Controls)
            {
                if (kontrol is TextBox)
                {
                    kontrol.Text = "";
                }
            }

        }

        private void comboBoxTedarikcisi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
