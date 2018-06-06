using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortigateUrlImporter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public   string dosyaYolu { get; set; }
        public   string dosyaAdi { get; set; } 

        private void btnVeriAL_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true || radioButton4.Checked == true)
            {
                DosyaSec();
                txt_dosya_getir();
                if (radioButton3.Checked == true)
                {
                    btnUrlScript.Visible = true;
                    btnSubnet.Visible = false;
                }
                else if (radioButton4.Checked == true)
                {
                    btnSubnet.Visible = true;
                    btnUrlScript.Visible = false;
                }

            }
            else
                MessageBox.Show("Lütfen nesne türü seçiniz");    
           
        }
     
        
        //Creating fqdn script for fortigate 
        public void script_olustur()

        {
            try
            {
                richTextBox2.Text = " config firewall address \n               edit ";
                StreamReader SR = new StreamReader(dosyaYolu);
                string okunan= SR.ReadLine();
                string yazi = okunan.Trim();
                while (yazi.Length != 0)
                {
                    richTextBox2.Text = richTextBox2.Text + '"' + yazi + '"' + "\n    " + "                          set type fqdn   \n                              set fqdn " + '"' + yazi + '"' + " \n               next      \n               edit ";
                    okunan = SR.ReadLine();
                    if (okunan == null)
                    {
                        break;
                    }
                        yazi = okunan.Trim();

                }

                SR.Close();
                richTextBox2.Text = richTextBox2.Text + "\n               end \n                 \n ";
            }
            catch (Exception eror)
            {
                MessageBox.Show("hata oluştu");
            }

        }

        //reading data from txt file then fill the richtextbox1 
        public void txt_dosya_getir()
        {
            try
            {
                richTextBox1.Text = " ";
                StreamReader SR = new StreamReader(dosyaYolu);
                string yazi = SR.ReadLine();
                if (yazi.Length == 0)
                {
                    MessageBox.Show("Dosyada veri yok. Lütfen yeni dosya seçiniz ");

                }

                while (yazi.Length != 0)
                {
                    richTextBox1.Text = richTextBox1.Text + yazi+ "\n";
                   
                    yazi = SR.ReadLine();
                }

                SR.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("hata oluştu");
            }

        }

        //Save script output as txt file 
        public void kaydet()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Metin Dosyası|*.txt";
            save.OverwritePrompt = true;
            save.CreatePrompt = true;
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter yazici = new StreamWriter(save.FileName);
                if (radioButton1.Checked == true)
                    yazici.WriteLine(richTextBox2.Text);
                else if (radioButton2.Checked == true)
                    yazici.WriteLine(richTextBox3.Text);
                yazici.Flush();

                yazici.Close();
                MessageBox.Show("Başarılı şekilde kaydedildi  ");
            }

        }
        // Adress Group creating for fortigate 
        public void AdresGroupCreate()
        {
            try
            {

                string groupName = "PhishingUrl";
                richTextBox3.Text = " config firewall addrgrp \n      edit " + '"' + groupName + '"' + "\n          set member ";
                FileStream fs = new FileStream(dosyaYolu, FileMode.Open, FileAccess.Read);
                StreamReader SR = new StreamReader(fs);
                string okunan = SR.ReadLine();
                string yazi = okunan.Trim();
                while (yazi.Length !=0)
                {
                    richTextBox3.Text = richTextBox3.Text + " " + '"' + yazi + '"' + " ";

                    okunan = SR.ReadLine();
                    if (okunan == null)
                    {
                        break;
                    }
                    yazi = okunan.Trim();
                }
                fs.Close();
                SR.Close();
                richTextBox3.Text = richTextBox3.Text + "\n      next\n      end";
            }
            catch (Exception error)
            {
                MessageBox.Show("hata oluştu");
            }

        }

        //choose url list file (txt) 
        public void DosyaSec()
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Metin Dosyası |*.txt";
                file.RestoreDirectory = true;
                file.Title = "dosya seçiniz";

                if (file.ShowDialog() == DialogResult.OK)
                {
                    dosyaAdi = file.SafeFileName;
                    dosyaYolu = file.FileName;

                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        
          
      //  Creating Subnet Script  
        
        public void  IPNetmaskOlustur()
        {

            var subnet = "/32";
                try
                {
                    richTextBox2.Text = " config firewall address \n               edit ";
                    StreamReader SR = new StreamReader(dosyaYolu);
                    string okunan = SR.ReadLine();
                    string yazi = okunan.Trim();
                    while (yazi.Length != 0)
                    {
                        richTextBox2.Text = richTextBox2.Text + '"' + yazi + '"' + "\n    " + "                             set subnet "+'"'+yazi+subnet+'"'+ "   \n               next      \n               edit ";
                        okunan = SR.ReadLine();
                        if (okunan == null)
                        {
                            break;
                        }
                        yazi = okunan.Trim();


                    }

                    SR.Close();
                    richTextBox2.Text = richTextBox2.Text + "\n               end \n                 \n ";
                }
                catch (Exception eror)
                {
                MessageBox.Show("hata oluştu");
                }

            

        }

        private void btnUrlScript_Click(object sender, EventArgs e)
        {
            script_olustur();
            btnUrlScript.Visible = false;
            btnAddGrp.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            button4.Visible = true;
        }

        private void btnAddGrp_Click(object sender, EventArgs e)
        {
            AdresGroupCreate();
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            button4.Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            kaydet();
        }

        private void btnSubnet_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            IPNetmaskOlustur();
            btnUrlScript.Visible = false;
            btnAddGrp.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            button4.Visible = true;
        }

      
      

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {
            Yardim yrd = new Yardim();
            yrd.Show();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Hakkinda hk = new Hakkinda();
            hk.Show();
        }
    }
}
