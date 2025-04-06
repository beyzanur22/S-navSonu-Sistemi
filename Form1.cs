using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SınavNotKayıtt
{                           
    public partial class FrmGiriş : Form
    {
        public FrmGiriş()
        {
            InitializeComponent(); // Form bileşenlerini başlatır.
        }
        // Öğrenci giriş butonuna tıklandığında çalışır.
        private void button1_Click(object sender, EventArgs e)
        {
            FrmÖğrenci frm = new FrmÖğrenci();  // Öğrenci formunu oluşturur.
            frm.numara = maskedTextBox1.Text; // MaskedTextBox1'e girilen numarayı öğrenci formuna aktarır.
            frm.Show();  // Öğrenci formunu ekranda gösterir.

        }



        // MaskedTextBox1'deki metin değiştiğinde çalışır.
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Eğer girilen metin "0000" ise öğretmen giriş paneli açılır.
            if (maskedTextBox1.Text == "0000")
            {
                FrmÖğretmenDetay fr = new FrmÖğretmenDetay(); // Öğretmen detay formunu oluşturur.
                fr.Show();  // Öğretmen detay formunu ekranda gösterir.

            }
        }
        // Öğretmen giriş butonuna tıklandığında çalışır.
        private void button2_Click(object sender, EventArgs e)
        {
            FrmÖğretmenDetay frm = new FrmÖğretmenDetay();
            frm.ıd = maskedTextBox2.Text;   // MaskedTextBox2'ye girilen ID'yi öğretmen detay formuna aktarır.
            frm.Show();

        }
        // Label3'e tıklandığında çalışır (şu anda herhangi bir işlem yapmıyor).
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmGiriş_Load(object sender, EventArgs e)
        {

        }
    }
}
