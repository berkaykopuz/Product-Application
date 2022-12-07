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
    public partial class FormGelirGider : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-AA53TK2;Initial Catalog=StokTakip;Integrated Security=True");
        DataSet daset = new DataSet();
        public FormGelirGider()
        {
            InitializeComponent();
        }

        private void FormGelirGider_Load(object sender, EventArgs e)
        {
            Gelir_Goster();
            Gider_Goster();
            Fatura_Goster();
        }
        private void Gelir_Goster()
        {
            baglanti.Open();

            SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, urunadi, toplamfiyati from satis", baglanti);
            adtr.Fill(daset, "satis");

            dataGridView1.DataSource = daset.Tables["satis"];

            SqlCommand komut = new SqlCommand("select sum(toplamfiyati) from satis", baglanti);
            txtNetGelir.Text = komut.ExecuteScalar().ToString();


            baglanti.Close();


        }
        private void Gider_Goster()
        {
            double toplamfiyati = 0;
            baglanti.Open();

            SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, urunadi, miktari*alisfiyati as toplamfiyati from siparişbase", baglanti);
            adtr.Fill(daset, "siparişbase");

            dataGridView2.DataSource = daset.Tables["siparişbase"];


            SqlCommand komut = new SqlCommand("select alisfiyati, miktari from siparişbase", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtAlisFiyati.Text = read["alisfiyati"].ToString();
                txtMiktari.Text = read["miktari"].ToString();

                toplamfiyati += double.Parse(txtAlisFiyati.Text) * double.Parse(txtMiktari.Text);

            }
            read.Close();
            SqlCommand komut2 = new SqlCommand("select elektrik, su, dogalgaz, elemanmaasi, yiyecekler from gider", baglanti);
            SqlDataReader read2 = komut2.ExecuteReader();
            while (read2.Read())
            {
                txtElektrik.Text = read2["elektrik"].ToString();
                txtSu.Text = read2["su"].ToString();
                txtDogalgaz.Text = read2["dogalgaz"].ToString();
                txtElemanMaası.Text = read2["elemanmaasi"].ToString();
                txtYiyecek.Text = read2["yiyecekler"].ToString();

                toplamfiyati += double.Parse(txtElektrik.Text) + double.Parse(txtSu.Text) + double.Parse(txtDogalgaz.Text) + double.Parse(txtElemanMaası.Text) + double.Parse(txtYiyecek.Text);
            }
            txtGider.Text = toplamfiyati.ToString();

            txtBilanco.Text = (double.Parse(txtNetGelir.Text) - double.Parse(txtGider.Text)).ToString();


            baglanti.Close();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Fatura_Goster()
        {
            baglanti.Open();

            SqlDataAdapter adtr = new SqlDataAdapter("select * from gider", baglanti);
            adtr.Fill(daset, "gider");

            dataGridView3.DataSource = daset.Tables["gider"];
            baglanti.Close();
        }
    }
}
