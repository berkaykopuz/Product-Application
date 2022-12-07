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
    public partial class FormMarka : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-AA53TK2;Initial Catalog=StokTakip;Integrated Security=True");
        bool durum;
        public FormMarka()
        {
            InitializeComponent();
        }

        private void FormMarka_Load(object sender, EventArgs e)
        {
            //Aşağıda kategoriyi getiriyor comboboxa.
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategori", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBoxKategori.Items.Add(read["kategori"].ToString());


            }
            baglanti.Close();
        }

        private void Marka_Engelle()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from marka", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                if ( comboBoxKategori.Text == read["kategori"].ToString() && txtMarka.Text == read["marka"].ToString() || txtMarka.Text == "" || comboBoxKategori.Text == "")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Marka_Engelle();
            Marka_Ekle();
            

        }

        private void Marka_Ekle()
        {
            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into marka(kategori, marka) values('" + comboBoxKategori.Text + "','" + txtMarka.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                
                MessageBox.Show("Marka Eklendi.");
            }
            else
            {
                MessageBox.Show("Böyle bir marka zaten bulunmaktadır!");
            }
            txtMarka.Text = "";
            comboBoxKategori.Text = "";
        }
       
    }
}
