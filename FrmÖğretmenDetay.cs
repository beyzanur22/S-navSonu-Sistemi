using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // SQL Server bağlantısı için gerekli kütüphane


namespace SınavNotKayıtt
{
    public partial class FrmÖğretmenDetay : Form
    {
        public string ıd = "0000";   // Öğretmen ID'sini tutan değişken. Varsayılan olarak "0000" atanmıştır


        public FrmÖğretmenDetay()
        {
            InitializeComponent();  // Form bileşenlerini başlatır. 

        }

        // Veritabanı bağlantısını sağlamak için kullanılan bağlantı stringi. 
        SqlConnection baglanti = new SqlConnection(@"Data Source=beyza\SQLEXPRESS03;Initial Catalog=DbNotKayıt;Integrated Security=True;Encrypt=False");
        private SqlConnection connection;

        private void ÖğretmenDetay_Load(object sender, EventArgs e)   // Veritabanındaki "TblDersler" tablosunu doldurur ve form üzerindeki grid'e (DataGridView) yansıtır.
        {
            // TODO: This line of code loads data into the 'dbNotKayıtDataSet.TblDersler' table. You can move, or remove it, as needed.
            this.tblDerslerTableAdapter.Fill(this.dbNotKayıtDataSet.TblDersler);

        }

        private void button1_Click(object sender, EventArgs e)   // "öğrenci kaydet" butonuna tıklandığında çalışır. Yeni öğrenci ekler.
        {
            baglanti.Open();

            // Yeni öğrenci eklemek için SQL komutu oluşturulur.
            SqlCommand cmd = new SqlCommand("insert into TblDersler ( OGRAD,OGRSOYAD,OGRNUMARA ) values ( @P1,@P2,@P3 )", baglanti);
            cmd.Parameters.AddWithValue("@P1", txtİsim.Text);
            cmd.Parameters.AddWithValue("@P2", txtSoyİsim.Text);
            cmd.Parameters.AddWithValue("@P3", mskNumara.Text);
            cmd.ExecuteNonQuery();   // SQL komutu çalıştırılır.

            baglanti.Close(); // Veritabanı bağlantısı kapatılır. 
            MessageBox.Show("Öğrenci Sisteme Eklendi..");   // Kullanıcıya başarılı bir şekilde eklendiğini bildirir.
            this.tblDerslerTableAdapter.Fill(this.dbNotKayıtDataSet.TblDersler);   // DataGridView'i yeniler, eklenen öğrenci listeye yansır.



        }
        // DataGridView'deki bir hücreye tıklandığında çalışır. Tıklanan satırdaki veriler form üzerindeki alanlara doldurulur.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)  
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            // Seçilen satırdaki veriler form üzerindeki ilgili alanlara doldurulur.
            mskNumara.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtİsim.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyİsim.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSınav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSınav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtSınav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();


        }

        private void button3_Click(object sender, EventArgs e)   // "Güncelle" butonuna tıklandığında çalışır. Öğrenci bilgilerini ve notlarını günceller.
        {
            double ortalama, s1, s2, s3; // Sınav notları ve ortalama için değişkenler tanımlanır.
            string sonuc;
            string gecenSayisi, kalanSayisi;
            // Sınav notları metin kutularından alınır ve double tipine dönüştürülür.
            s1 = Convert.ToDouble(txtSınav1.Text);
            s1 = Convert.ToDouble(txtSınav1.Text);
            s2 = Convert.ToDouble(txtSınav2.Text);
            s3 = Convert.ToDouble(txtSınav3.Text);

            ortalama = (s1 + s2 + s3) / 3;  // Ortalama hesaplanır.
            lblOrtalama.Text = ortalama.ToString();  // Ortalama, form üzerindeki label'a yazdırılır.

            // Ortalama 50 veya üzerindeyse sonuç "True", değilse "False" olarak atanır.
            if (ortalama>=50)
            {
                sonuc = "True";
            }
            else
            {
                sonuc = "False";

            }
            // Veritabanında öğrencinin notları ve durumu güncellenir.

            baglanti.Open(); // Veritabanı bağlantısını açar.
            SqlCommand cmd = new SqlCommand("update TblDersler set OGRSINAV1=@P1, OGRSINAV2=@P2,OGRSINAV3=@P3,ORTALAMA=@P4, SONUÇ=@P5 where OGRNUMARA=@P6 ",baglanti);
            // SQL komutu çalıştırılır.
            cmd.Parameters.AddWithValue("@P1", txtSınav1.Text);
            cmd.Parameters.AddWithValue("@P2", txtSınav2.Text);
            cmd.Parameters.AddWithValue("@P3", txtSınav3.Text);
            cmd.Parameters.AddWithValue("@P4", decimal.Parse(lblOrtalama.Text));
            cmd.Parameters.AddWithValue("@P5",  sonuc);
            cmd.Parameters.AddWithValue("@P6", mskNumara.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close(); // Bağlantı kapatılır.
            MessageBox.Show("Öğrenci Notları Güncellenmiştir.");   // Kullanıcıya başarılı bir şekilde güncellendiğini bildirir.
            this.tblDerslerTableAdapter.Fill(this.dbNotKayıtDataSet.TblDersler);    // DataGridView'i yeniler, güncellenen veriler listeye yansır.




        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Önce kullanıcının gerçekten silmek isteyip istemediğini doğrulamak için bir uyarı gösterebilirsiniz.
            DialogResult dialogResult = MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open(); // Veritabanı bağlantısını aç
                    SqlCommand cmd = new SqlCommand("DELETE FROM TblDersler WHERE OGRNUMARA = @P1", baglanti);
                    cmd.Parameters.AddWithValue("@P1", mskNumara.Text); // Silmek için öğrenci numarasını parametre olarak gönder
                    int rowsAffected = cmd.ExecuteNonQuery(); // Komutu çalıştır ve silinen satır sayısını al
                    baglanti.Close(); // Bağlantıyı kapat

                    // Silme işlemi başarılıysa kullanıcıya bilgi ver
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Öğrenci kaydı başarıyla silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Belirtilen öğrenci numarasıyla eşleşen kayıt bulunamadı.");
                    }

                    // DataGridView'i güncelle
                    this.tblDerslerTableAdapter.Fill(this.dbNotKayıtDataSet.TblDersler);
                }
                catch (Exception ex)
                {
                    baglanti.Close(); // Hata durumunda bağlantıyı kapat
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }

        }
    }
}
