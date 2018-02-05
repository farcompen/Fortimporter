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

        private void button1_Click(object sender, EventArgs e)
        {
            DosyaSec();
            txt_dosya_getir();
            button2.Visible = true;
        }
     
        
        //Creating fqdn script for fortigate 
        public void script_olustur()

        {
            try
            {
                richTextBox2.Text = " config firewall address \n               edit ";
                StreamReader SR = new StreamReader(dosyaYolu);
                string yazi = SR.ReadLine();
                while (yazi != null)
                {
                    richTextBox2.Text = richTextBox2.Text + '"' + yazi + '"' + "\n    " + "                          set type fqdn   \n                              set fqdn " + '"' + yazi + '"' + " \n               next      \n               edit ";
                    yazi = SR.ReadLine();
                }

                SR.Close();
                richTextBox2.Text = richTextBox2.Text + "\n               end \n                 \n ";
            }
            catch (Exception eror)
            {
                throw eror;
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
                while (yazi != null)
                {
                    richTextBox1.Text = richTextBox1.Text + yazi + "\n";
                    yazi = SR.ReadLine();
                }

                SR.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
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
                string yazi = SR.ReadLine();
                while (yazi != null)
                {
                    richTextBox3.Text = richTextBox3.Text + " " + '"' + yazi + '"' + " ";
                    yazi = SR.ReadLine();
                }
                fs.Close();
                SR.Close();
                richTextBox3.Text = richTextBox3.Text + "\n      next\n      end";
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }

        }

        //choose url list file (txt) 
        public void DosyaSec()
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

        private void button2_Click(object sender, EventArgs e)
        {
            script_olustur();
            button2.Visible = false;
            button3.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            button4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
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
    }
}
