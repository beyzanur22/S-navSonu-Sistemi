using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;  // SQL Server ile bağlantı kurmak için gerekli kütüphane.


namespace SınavNotKayıtt
{
    

   
    public partial class FrmÖğrenci : Form
    {
        // Öğrencinin girişte girdiği numarayı tutacak değişken.
        public string numara;

        // SQL bağlantısı oluşturuluyor. Veritabanı bilgileri burada tanımlanır.
        SqlConnection baglanti = new SqlConnection(@"Data Source=beyza\SQLEXPRESS03;Initial Catalog=DbNotKayıt;Integrated Security=True;Encrypt=False");

      
        //Data Source=beyza\SQLEXPRESS03;Initial Catalog=DbNotKayıt;Integrated Security=True;Encrypt=False
        public FrmÖğrenci() // Formun constructor metodu, form bileşenlerini başlatır.
        {
        
            
            InitializeComponent();   // Formdaki bileşenlerin başlatılması.
        }
       
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FrmÖğrenci_Load(object sender, EventArgs e)  // Form yüklendiğinde çalışır.
        {
            lblNumara.Text = numara;  // Öğrencinin girişte girdiği numara, ilgili label'a yazdırılır.
            baglanti.Open();  // Veritabanı bağlantısı açılır.
            // SQL sorgusu: Öğrenci numarasına göre veritabanındaki bilgileri getirir.
            SqlCommand komut = new SqlCommand("Select * From TblDersler where OGRNUMARA=@p3", baglanti);  // Öğrenci numarası parametre olarak sorguya eklenir.
            komut.Parameters.AddWithValue("@p3", numara);
            SqlDataReader dr = komut.ExecuteReader();  // SQL sorgusundan dönen verileri okuyacak DataReader nesnesi.
            while (dr.Read())
            {
                // SQL sorgusundan dönen veriler ilgili label'lara yazdırılır.
                lblAdSoyad.Text = dr[1].ToString() + " " + dr[2].ToString();
                lblSınav1.Text = dr[4].ToString();
                lblSınav2.Text = dr[5].ToString();
                lblSınav3.Text = dr[6].ToString();
                lblOtalama.Text = dr[7].ToString();
                lblDurum.Text = dr[8].ToString();

            }
            baglanti.Close();  // Veritabanı bağlantısı kapatılır.

        }

        private void lblAdSoyad_Click(object sender, EventArgs e)
        {

        }

        private void lblNumara_Click(object sender, EventArgs e)
        {

        }

        private void lblSınav1_Click(object sender, EventArgs e)
        {

        }

        private void lblSınav2_Click(object sender, EventArgs e)
        {

        }

        private void lblSınav3_Click(object sender, EventArgs e)
        {

        }

        private void lblOtalama_Click(object sender, EventArgs e)
        {

        }

        private void lblDurum_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
