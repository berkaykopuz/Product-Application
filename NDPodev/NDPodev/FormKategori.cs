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
    public partial class FormKategori : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-DRF9IU3;Initial Catalog=StokTakip;Integrated Security=True");
        bool durum;

        private void Kategori_Engelle()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategori",baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while(read.Read())
            {
                if(txtKategori.Text==read["kategori"].ToString() || txtKategori.Text == "")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        public FormKategori()
        {
            InitializeComponent();
        }

        private void FormKategori_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kategori_Engelle();

            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into kategori(kategori) values('" + txtKategori.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                
                MessageBox.Show("Kategori Eklendi.");
            }
            else
            {
                MessageBox.Show("Böyle bir kategori zaten var!");
            }
            txtKategori.Text = "";

        }
    }
}
