using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProgramonLauncher
{
    public partial class frmLauncher : Form
    {
        public bool isFirstOpen;
        XmlDocument xDoc;
        string path;

        public frmLauncher()
        {
            InitializeComponent();

            path = @"C:\Users\Ricky van den Berg\Documents\Programming\Programon\Project\Programon\Programon\Programon\config.xml";
            xDoc = new XmlDocument();
            xDoc.Load(path);
            string temp;
            temp = xDoc.SelectSingleNode("Config/Resolution/width").InnerText;
            temp = temp + "x" + xDoc.SelectSingleNode("Config/Resolution/height").InnerText;
            cbResolution.Text = temp;

            cbFullscreen.Text = xDoc.SelectSingleNode("Config/Fullscreen").InnerText;
            
            temp = xDoc.SelectSingleNode("Config/Mastervolume").InnerText;
            tbMasterVolume.Value = Convert.ToInt32(temp);
            lblVolume.Text = Convert.ToString(tbMasterVolume.Value);

            temp = xDoc.SelectSingleNode("Config/launcher/KeepOpen").InnerText;
            cbClose.Checked = Convert.ToBoolean(temp);
        }

        private void tbMasterVolume_Scroll(object sender, EventArgs e)
        {
            lblVolume.Text = Convert.ToString(tbMasterVolume.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbClose.Checked)
            {
                xDoc.SelectSingleNode("Config/Resolution/width").InnerText = cbResolution.Text.Split('x')[0];
                xDoc.SelectSingleNode("Config/Resolution/height").InnerText = cbResolution.Text.Split('x')[1];


                xDoc.SelectSingleNode("Config/Fullscreen").InnerText = cbFullscreen.Text;

                xDoc.SelectSingleNode("Config/Mastervolume").InnerText = Convert.ToString(tbMasterVolume.Value);

                xDoc.SelectSingleNode("Config/launcher/KeepOpen").InnerText = Convert.ToString(cbClose.Checked);
                xDoc.Save(path);


                this.Close();

                //launch exe
            }
            else
            {
                xDoc.SelectSingleNode("Config/Resolution/width").InnerText = cbResolution.Text.Split('x')[0];
                xDoc.SelectSingleNode("Config/Resolution/height").InnerText = cbResolution.Text.Split('x')[1];


                xDoc.SelectSingleNode("Config/Fullscreen").InnerText = cbFullscreen.Text;

                xDoc.SelectSingleNode("Config/Mastervolume").InnerText = Convert.ToString(tbMasterVolume.Value);

                xDoc.SelectSingleNode("Config/launcher/KeepOpen").InnerText = Convert.ToString(cbClose.Checked);
                xDoc.Save(path);

                //launch exe
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}

