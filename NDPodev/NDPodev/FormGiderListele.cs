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
    public partial class FormGiderListele : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-AA53TK2;Initial Catalog=StokTakip;Integrated Security=True");
        DataSet daset = new DataSet();
        public FormGiderListele()
        {
            InitializeComponent();
        }

        private void FormGiderListele_Load(object sender, EventArgs e)
        {
            Kayıt_Göster();
        }
        private void Kayıt_Göster()
        {
            baglanti.Open();

            SqlDataAdapter adtr = new SqlDataAdapter("select * from gider", baglanti);
            adtr.Fill(daset, "gider");

            dataGridView1.DataSource = daset.Tables["gider"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtYiyecek.Text = dataGridView1.CurrentRow.Cells["yiyecekler"].Value.ToString();
            txtDogalgaz.Text = dataGridView1.CurrentRow.Cells["dogalgaz"].Value.ToString();
            txtElektrik.Text = dataGridView1.CurrentRow.Cells["elektrik"].Value.ToString();
            txtElemanMaası.Text = dataGridView1.CurrentRow.Cells["elemanmaasi"].Value.ToString();
            txtSu.Text = dataGridView1.CurrentRow.Cells["su"].Value.ToString();
            txtTarih.Text = dataGridView1.CurrentRow.Cells["tarih"].Value.ToString();


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update gider set elektrik=@elektrik, dogalgaz=@dogalgaz, su=@su, elemanmaasi=@elemanmaasi, yiyecekler=@yiyecekler where tarih=@tarih", baglanti);

            komut.Parameters.AddWithValue("@elektrik", txtElektrik.Text);
            komut.Parameters.AddWithValue("@dogalgaz", txtDogalgaz.Text);

            komut.Parameters.AddWithValue("@yiyecekler", txtYiyecek.Text);
            komut.Parameters.AddWithValue("@su", txtSu.Text);
            komut.Parameters.AddWithValue("@elemanmaasi", txtElemanMaası.Text);
            komut.Parameters.AddWithValue("@tarih", txtTarih.Text);


            komut.ExecuteNonQuery();
            baglanti.Close();


            MessageBox.Show("Gider Kaydı Güncellendi.");
            daset.Tables["gider"].Clear();
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
            SqlCommand komut = new SqlCommand("delete from gider where tarih='" + dataGridView1.CurrentRow.Cells["tarih"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();


            MessageBox.Show("Kayıt Silindi.");

            daset.Tables["gider"].Clear();
            Kayıt_Göster();
        }

        
    }
}
