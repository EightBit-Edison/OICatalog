using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OICatalog3
{
    public partial class Form1 : Form
    {
        private string db;
        private int category;
        private SqliteConnector Connector;
        public Form1()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.URL = @"Content\video\1.m4v";
            ConnectToDatabase(@"DB\Ruslang\CEE-RusLang.sqlite");
            
        }

        private void ConnectToDatabase(string db)
        {
            this.db = db;
            Connector = new SqliteConnector(db);
            label2.Text = Connector.GetRegistryValue("category1");
            //label3.Text = Connector.GetRegistryValue("category2");
            label4.Text = Connector.GetRegistryValue("category3");
            label13.Text = Connector.GetRegistryValue("main1");
            label5.Text = Connector.GetRegistryValue("main4");
            label7.Text = Connector.GetRegistryValue("main3");
            label8.Text = Connector.GetRegistryValue("main2");
            label9.Text = Connector.GetRegistryValue("main1");
            button1.Text = Connector.GetRegistryValue("search");
            label1.Text = Connector.GetRegistryValue("tomain");
            label14.Text = Connector.GetRegistryValue("ministry");           
            List<Participant> Participants = Connector.GetAllParticipants();
            listBox1.Items.Clear();
            for (int i = 0; i < Participants.Count; i++)
            {
                listBox1.Items.Add(Participants[i].Name);
            }
           
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                HTMLGenerator page = new HTMLGenerator(db);
                string source = page.PageGenerator(listBox1.SelectedItems[0].ToString(),this.category);
                webBrowser1.DocumentText = source;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            ConnectToDatabase(@"DB\ChiLang\CEE-ChineeseLang.sqlite");
            //panel1.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            ConnectToDatabase(@"DB\Ruslang\CEE-RusLang.sqlite");
            //panel1.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            ConnectToDatabase(@"DB\ChiLang\CEE-ChineeseLang.sqlite");
            //panel1.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.category = 1;
            button3_Click(sender, e);
            listBox1.SelectedIndices.Clear();
            listBox1.SelectedIndices.Add(0);
           // panel4.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.category = 2;
            button3_Click(sender, e);
            listBox1.SelectedIndices.Clear();
            listBox1.SelectedIndices.Add(0);
            //panel4.Visible = false;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.category = 1;
            button3_Click(sender, e);
            listBox1.SelectedIndices.Clear();
            listBox1.SelectedIndices.Add(0);
           // panel4.Visible = false;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.category = 2;
            button3_Click(sender, e);
            listBox1.SelectedIndices.Clear();
            listBox1.SelectedIndices.Add(0);
            //panel4.Visible = false;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.category = 3;
            button3_Click(sender, e);
            listBox1.SelectedIndices.Clear();
            listBox1.SelectedIndices.Add(0);
            //panel4.Visible = false;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            //panel4.Visible = true;
            webBrowser1.DocumentText = "";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.category = 3;
            button3_Click(sender, e);
            listBox1.SelectedIndices.Clear();
            listBox1.SelectedIndices.Add(0);
            //panel4.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
           // panel4.Visible = true;
            webBrowser1.DocumentText = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1_SelectedIndexChanged(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            panel1.Visible = false;
            ConnectToDatabase(db);
        }


        private void label1_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List <string> list = Connector.Search(textBox1.Text);
            
            foreach (var item in list)
            {
                listBox1.Items.Add(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //category = 1;
            panel1.Visible = false;
            ConnectToDatabase(db);
            listBox1.Items.Clear();
            List<string> list = Connector.GetProjects(category);

            foreach (var item in list)
            {
                listBox1.Items.Add(item);
            }
            listBox1.SelectedIndices.Add(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            ConnectToDatabase(db);
            if (db == @"DB\Ruslang\CEE-RusLang.sqlite")
            {
                webBrowser1.Url = new Uri(Directory.GetCurrentDirectory() + "/Content/html/minobr1.htm");
            }
            else
            {
                webBrowser1.Url = new Uri(Directory.GetCurrentDirectory() + "/Content/html/minobr.htm");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            ConnectToDatabase(db);
            if (db == @"DB\Ruslang\CEE-RusLang.sqlite")
            {
                webBrowser1.Url = new Uri(Directory.GetCurrentDirectory() + "/Content/html/cont1.htm");
            }
            else
            {
                webBrowser1.Url = new Uri(Directory.GetCurrentDirectory() + "/Content/html/cont.htm");
            }
            
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush my = Brushes.Black;
            listBox1.HorizontalExtent = 560;
            // listBox1.HorizontalScrollbar = true;
            if (e.Index != -1)
            {
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, my, e.Bounds);
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsMediaEnded)
            {

                axWindowsMediaPlayer1.Visible = false;
            }
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
            ConnectToDatabase(db);
            if (db == @"DB\Ruslang\CEE-RusLang.sqlite")
            {
                webBrowser1.Url = new Uri(Directory.GetCurrentDirectory() + "/Content/html/cont1.htm");
            }
            else
            {
                webBrowser1.Url = new Uri(Directory.GetCurrentDirectory() + "/Content/html/cont.htm");
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            category = 1;
            panel1.Visible = false;
            ConnectToDatabase(db);
            listBox1.Items.Clear();
            List<string> list = Connector.GetProjects(category);

            foreach (var item in list)
            {
                listBox1.Items.Add(item);
            }
            listBox1.SelectedIndices.Add(0);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            ConnectToDatabase(db);
            if (db == @"DB\Ruslang\CEE-RusLang.sqlite")
            {
                webBrowser1.Url = new Uri(Directory.GetCurrentDirectory() + "/Content/html/minobr1.htm");
            }
            else
            {
                webBrowser1.Url = new Uri(Directory.GetCurrentDirectory() + "/Content/html/minobr.htm");
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

            panel1.Visible = false;
            ConnectToDatabase(db);
            listBox1.SelectedIndices.Add(0);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            ConnectToDatabase(@"DB\Englang\CEE-EngLang.sqlite");
            //panel1.Visible = false;
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            ConnectToDatabase(@"DB\Ruslang\CEE-RusLang.sqlite");
            //panel1.Visible = false;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            ConnectToDatabase(@"DB\Englang\CEE-EngLang.sqlite");
            //panel1.Visible = false;
        }

        private void label11_Click(object sender, EventArgs e)
        {

            webBrowser1.DocumentText = "";
            ConnectToDatabase(@"DB\Ruslang\CEE-RusLang.sqlite");
            //panel1.Visible = false;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            ConnectToDatabase(@"DB\ChiLang\CEE-ChineeseLang.sqlite");
            //panel1.Visible = false;
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

            panel1.Visible = false;
            ConnectToDatabase(db);
            listBox1.SelectedIndices.Add(0);
        }

        private void label13_Click(object sender, EventArgs e)
        {

            panel1.Visible = false;
            ConnectToDatabase(db);
            listBox1.SelectedIndices.Add(0);
        }
    }
}
