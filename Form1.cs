using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using MessagingToolkit.QRCode.Codec;
using Microsoft.VisualBasic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Threading;
using SimpleTCP;
using System.Security.Cryptography;
using System.Diagnostics;

namespace WİNDOWS_QR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Form frm2 = new Form2();
        private bool durum = false;
        private Point noktalar = Point.Empty;
        int sayac = 120;
        string securitycode;
        bool starter = true;

        SimpleTcpServer server;

        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }


        [Obsolete]
        public void Server()
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += server_DataReceived;

            string ipAdresi = Dns.GetHostAddresses(Dns.GetHostName())[3].ToString();
            IPAddress ipAddress = IPAddress.Parse(ipAdresi);
            server.Start(ipAddress, 8080);
          
        }
            
        private void server_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtmessage.Invoke((MethodInvoker)delegate ()
           {
               txtmessage.Text += e.MessageString;
               e.ReplyLine(string.Format(e.MessageString));
           });
        }

        [Obsolete]
        private void Form1_Load(object sender, EventArgs e)
        {                       
                Server();
                              
                Random rnd = new Random();
                int code = rnd.Next(10000, 100000);
                securitycode = code.ToString();
                label5.Text = "Güvenlik Kodu: " + code.ToString();
                Properties.Settings.Default.secuirtycode = code.ToString();
                txtmessage.Visible = false;

                label5.Height = 120;
                label5.Width = 30;
               
                label3.ForeColor = Color.Red;

                timer1.Interval = 1000;
                timer1.Start();
                label3.Text = "Doğrulanmadı";

                if (Properties.Settings.Default.checkboxayar == true & starter == true)
                {
                    checkBox1.Checked = true;
                    starter = false;
                }
                else if (Properties.Settings.Default.checkboxayar == false & starter == true)
                {
                    checkBox1.Checked = true;
                    starter = false;
                }
           
                               
           }
      
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Properties.Settings.Default.password == string.Empty)
            {
                frm2.ShowDialog();
            }
            else
            {
                string password = Convert.ToString(Interaction.InputBox("Lütfen Şifenizi Girin",
              "Project Windows QR"));

                if(password==Properties.Settings.Default.password)
                    {
                    frm2.ShowDialog();
                }
                else
                {
                    if(password!=Properties.Settings.Default.password)
                    {
                        MessageBox.Show("Hatalı Giriş Yaptınız", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                  
                   
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string veri = EncodeTo64(Properties.Settings.Default.servername + Properties.Settings.Default.serverpassword
                + Properties.Settings.Default.secuirtycode);

            QRCodeEncoder encod = new QRCodeEncoder();
            pictureBox1.Image = encod.Encode(veri);

        }      

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string password = Convert.ToString(Interaction.InputBox("Lütfen Şifenizi Girin",
             "Project Windows QR"));
            if (password == Properties.Settings.Default.password)
            {
                label3.Text = "Doğrulandı";
                Application.Exit();

            }
            else if(password != Properties.Settings.Default.password & password != string.Empty)
            {
                MessageBox.Show("Hatalı Giriş Yaptınız", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Boş Değer Girilemez!", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false & starter == false)
            {
                Properties.Settings.Default.checkboxayar = false;
                Properties.Settings.Default.Save();
            }
            else if (checkBox1.Checked == true & starter == false)
            {
                Properties.Settings.Default.checkboxayar = true;
                Properties.Settings.Default.Save();
            }           


            if (checkBox1.Checked == true & starter == false)
            {
                if (Properties.Settings.Default.password == string.Empty)
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    key.SetValue("Project Windows QR", "\"" + Application.ExecutablePath + "\"");
                    key.Close();
                }
                else if (Properties.Settings.Default.password != string.Empty & starter == false)
                {
             string password = Convert.ToString(Interaction.InputBox("Lütfen Şifenizi Girin",
            "Project Windows QR"));
                    if (password == Properties.Settings.Default.password)
                    {
                        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                        key.SetValue("Project Windows QR", "\"" + Application.ExecutablePath + "\"");

                    }
                    if (password != Properties.Settings.Default.password)
                    {
                        MessageBox.Show("Hatalı Giriş Yaptınız", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
               
                if (checkBox1.Checked == false & starter == false)
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run",RegistryKeyPermissionCheck.ReadWriteSubTree,System.Security.AccessControl.RegistryRights.SetValue);
                    key.DeleteValue("Project Windows QR");
                    key.Close();                  
                }
                                                                  
                
                Properties.Settings.Default.checkboxayar = checkBox1.Checked;
                Properties.Settings.Default.Save();
            }
        }

        [Obsolete]
        private void button2_Click(object sender, EventArgs e)
        {
                Thread.Sleep(TimeSpan.FromSeconds(2));               
            try
            {                            
                    if (txtmessage.Text.Contains("true") == true)
                    {
                        label3.Text = "Doğrulandı";
                        timer1.Stop();
                        time.Start();
                        this.Hide();
                        MessageBox.Show("QR Kod Başarıyla Doğrulandı", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (txtmessage.Text.Contains("true") == false & txtmessage.Text != string.Empty)
                    {
                        label3.Text = "Doğrulanmadı";
                        MessageBox.Show("QR Kod Yanlış Lütfen Tekrar Deneyin", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (txtmessage.Text == string.Empty)
                    {
                        MessageBox.Show("Telefonla Bağlantı Kurulamıyor!", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                               
            }
            catch
            (Exception s)
            {
                MessageBox.Show(s.ToString());
            }

            }
            
                 
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac -= 1;
            if (sayac == 0)
            {
                System.Diagnostics.Process.Start("shutdown", "-f -s -t 1");
                timer1.Stop();
            }
            label2.Text = sayac.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(label3.Text=="Doğrulandı")
            {
                timer1.Stop();
                time.Start();
                this.Hide();
                
            }
            else
            {
                if(label3.Text=="Doğrulanmadı")
                {
                 MessageBox.Show("KarekodKod Doğrulanamdı!", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Form frm = sender as Form;
            durum = true;
            frm.Cursor = Cursors.SizeAll;
            noktalar = e.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Form frm = sender as Form;
            durum = false;
            frm.Cursor = Cursors.Default;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (durum)
            {
                Form frm = sender as Form;
                frm.Left = frm.Left + (e.X - noktalar.X);
                frm.Top = frm.Top + (e.Y - noktalar.Y);
            }
        }
       

        private void time_Tick(object sender, EventArgs e)
        {
            int saat = 0, dakika = 0, saniye = 0;

            if ((saniye == 59))
            {
                saniye = 0;
                dakika = dakika + 1;
                if (dakika == 60)
                {
                    saniye = 0;
                    dakika = 0;
                    saat = saat + 1;
                }
            }
            saniye = saniye + 1;
                     

            if (Properties.Settings.Default.timehr == saat & Properties.Settings.Default.timemt == dakika)
            {
                System.Diagnostics.Process.Start("shutdown", "-f -s -t 1");
            }
            else if (Properties.Settings.Default.timehr == saat & Properties.Settings.Default.timemt == dakika-5 & Properties.Settings.Default.timemt!= 0)
            {
                MessageBox.Show("Son 5 Dakikanız Kaldı!","Project Windows QR",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if (Properties.Settings.Default.timehr == saat & Properties.Settings.Default.timemt == dakika - 1 & Properties.Settings.Default.timemt != 0)
            {
                MessageBox.Show("Son 1 Dakikanız Kaldı!", "Project Windows QR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                return;


        }

        }
    }
