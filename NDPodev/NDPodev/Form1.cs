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
    public partial class AnaForm : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-AA53TK2;Initial Catalog=StokTakip;Integrated Security=True");
        DataSet daset = new DataSet();
        bool durum;
        public AnaForm()
        {
            InitializeComponent();
        }
        private void Sepet_Listele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from sepet",baglanti);
            adtr.Fill(daset, "sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            baglanti.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormMüşteri ekle = new FormMüşteri();

            ekle.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormMüşteriListele listele = new FormMüşteriListele();
            listele.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormTedarikçiEkle ekle = new FormTedarikçiEkle();
            ekle.ShowDialog();
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            Sepet_Listele();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormTedarikçiListele listele = new FormTedarikçiListele();
            listele.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FormKategori kategori = new FormKategori();
            kategori.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FormMarka marka = new FormMarka();
            marka.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormSipariş siparis = new FormSipariş();
            siparis.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormÜrünListele listele = new FormÜrünListele();
            listele.ShowDialog();
        }

        private void txtNo_TextChanged(object sender, EventArgs e)
        {
            if(txtNo.Text == "")
            {
                txtAdSoyad.Text = "";
                txtTelefon.Text = "";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from müşteri where no like '"+txtNo.Text+"'",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtTelefon.Text = read["telefon"].ToString();
            }
            baglanti.Close();
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            Temizle();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from siparişbase where barkodno like '" + txtBarkodNo.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtUrunAdi.Text = read["urunadi"].ToString();
                txtSatisFiyati.Text = read["satisfiyati"].ToString();
            }
            baglanti.Close();
        }

        private void Temizle()
        {
            if (txtBarkodNo.Text == "")
            {
                foreach (Control kontrol in groupBox2.Controls)
                {
                    if (kontrol is TextBox)
                    {
                        if (kontrol != txtMiktar)
                        {
                            kontrol.Text = "";
                        }
                    }

                }
            }
        }
        private void Barkod_Kontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sepet",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if(txtBarkodNo.Text == read["barkodno"].ToString())
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void Hesapla()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyati) from sepet", baglanti);
                lblGenelToplam.Text = komut.ExecuteScalar() + "  TL";
                baglanti.Close();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Barkod_Kontrol();
            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into sepet(no,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyati,tarih) values(@no,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyati,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@no", txtNo.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@urunadi", txtUrunAdi.Text);
                komut.Parameters.AddWithValue("@miktari", int.Parse(txtMiktar.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatisFiyati.Text));
                komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(txtToplamFiyat.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {
                baglanti.Open();

                SqlCommand komutupdate = new SqlCommand("update sepet set miktari=miktari + '"+int.Parse(txtMiktar.Text)+"' where barkodno='"+txtBarkodNo.Text+"'   ",baglanti);
                komutupdate.ExecuteNonQuery();
                SqlCommand komutupdate2 = new SqlCommand("update sepet set toplamfiyati=miktari*satisfiyati where barkodno= '"+txtBarkodNo.Text+"'", baglanti);
                komutupdate2.ExecuteNonQuery();
                baglanti.Close();
            }
            txtMiktar.Text = "1";

            daset.Tables["sepet"].Clear();
            Sepet_Listele();

            Hesapla();

            
           
            
            /////////////////
            foreach (Control kontrol in groupBox2.Controls)
            {
                if (kontrol is TextBox)
                {
                    if (kontrol != txtMiktar)
                    {
                        kontrol.Text = "";
                    }
                }

            }
            ////////////////
            ///
            
        }

        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToplamFiyat.Text = (double.Parse(txtMiktar.Text) * double.Parse(txtSatisFiyati.Text)).ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno='"+dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString()+"'  ",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("Ürün sepetten çıkarıldı.");
            daset.Tables["sepet"].Clear();
            Sepet_Listele();
            Hesapla();
        }

        private void btnSatisİptal_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet  ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Sepet seçimi iptal edildi. ");
            daset.Tables["sepet"].Clear();
            Sepet_Listele();
            Hesapla();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormSatışListele listele = new FormSatışListele();
            listele.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into satis(no,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyati,tarih) values(@no,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyati,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@no", txtNo.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                komut.Parameters.AddWithValue("@urunadi", dataGridView1.Rows[i].Cells["urunadi"].Value.ToString());
                komut.Parameters.AddWithValue("@miktari", int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(dataGridView1.Rows[i].Cells["satisfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(dataGridView1.Rows[i].Cells["toplamfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                
                
                SqlCommand komut2 = new SqlCommand("update siparişbase set miktari=miktari-'" + int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()) + "' where barkodno='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "' ", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();


            }
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("delete from sepet  ", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["sepet"].Clear();
            Sepet_Listele();

            Hesapla();
            MessageBox.Show("Satış Başarılı.");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FormGiderOluştur ekle = new FormGiderOluştur();
            ekle.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FormGiderListele listele = new FormGiderListele();
            listele.ShowDialog();
        }

        private void btnGelirGider_Click(object sender, EventArgs e)
        {
            FormGelirGider ekle = new FormGelirGider();
            ekle.ShowDialog();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
