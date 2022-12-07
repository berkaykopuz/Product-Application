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
    public partial class FormGiderOluştur : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-AA53TK2;Initial Catalog=StokTakip;Integrated Security=True");
        public FormGiderOluştur()
        {
            InitializeComponent();
        }

        private void btnGiderEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into gider(elektrik,dogalgaz,su,elemanmaasi,yiyecekler,tarih) values(@elektrik,@dogalgaz,@su,@elemanmaasi,@yiyecekler,@tarih)", baglanti);
            komut.Parameters.AddWithValue("@elektrik", txtElektrik.Text);
            komut.Parameters.AddWithValue("@dogalgaz", txtDogalgaz.Text);
            komut.Parameters.AddWithValue("@elemanmaasi", txtElemanMaası.Text);
            komut.Parameters.AddWithValue("@yiyecekler", txtYiyecek.Text);
            komut.Parameters.AddWithValue("@su", txtSu.Text);
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Gider kaydı eklendi.");

            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
    }
}
