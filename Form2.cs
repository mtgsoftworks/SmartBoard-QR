using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WİNDOWS_QR
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text== string.Empty | textBox3.Text==string.Empty)
            {
                MessageBox.Show("Lütfen Tüm Bilgileri Doldurun", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(textBox1.Text != string.Empty & textBox3.Text != string.Empty)
            {
                Properties.Settings.Default.servername = textBox1.Text;
                Properties.Settings.Default.serverpassword = textBox3.Text;
                Properties.Settings.Default.Save();
                MessageBox.Show("Sunucu Bilgileri Kaydedildi", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        [Obsolete]
        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;

            string ipAdresi = Dns.GetHostAddresses(Dns.GetHostName())[3].ToString();

            textBox1.Text = ipAdresi;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text != string.Empty & textBox4.Text != string.Empty & textBox7.Text != string.Empty & textBox4.Text == textBox7.Text)
                {
                    Properties.Settings.Default.user = textBox5.Text;
                    Properties.Settings.Default.password = textBox4.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Bilgiler Kaydedildi", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (textBox5.Text != string.Empty | textBox4.Text != string.Empty | textBox7.Text != string.Empty)
                {
                    MessageBox.Show("Lütfen Tüm Boşlukları Doğru Bir Şekilde Doldurduğunuza Emin Olun", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textBox4.Text != textBox7.Text)
                {
                    MessageBox.Show("Şifreler Aynı Değil Lütfen Bir Daha Kontrol Ediniz", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception s)
            {
                MessageBox.Show(s.ToString(), "Project Windows QR");
            }

           
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AboutBox1 abtbox = new AboutBox1();
            abtbox.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.timehr = Convert.ToInt16(textBox2.Text);
            Properties.Settings.Default.timemt= Convert.ToInt16(textBox6.Text);
            Properties.Settings.Default.Save();
            MessageBox.Show("Bilgiler Kaydedildi", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
