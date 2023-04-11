using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Resources;
using browser.Properties;
using System.IO;
using CefSharp.DevTools.Debugger;

namespace browser
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            InitializeChromium();

            CreateNewPage("https://www.google.com");

        }
        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.Initialize(settings);


            // SelectedTab
        }
        ChromiumWebBrowser chrome;

        public void CreateNewPage(string link)
        {
            TabPage newTabPage = new TabPage();
            newTabPage.Text = "Новая вкладка";

            tabControl1.TabPages.Add(newTabPage);
            tabControl1.SelectedIndex = tabControl1.TabCount - 1;

            
            chrome = new ChromiumWebBrowser(link);
            // Add it to the form and fill it to the form window.
            tabControl1.SelectedTab.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
            
            chrome.AddressChanged += new System.EventHandler<CefSharp.AddressChangedEventArgs>(this.chromiumWebBrowser_AddressChanged);
        }
        private void btnNewPage_Click(object sender, EventArgs e)
        {
            CreateNewPage("https://www.google.com");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser rtb = (ChromiumWebBrowser)tabControl1.SelectedTab.Controls.Cast<Control>().FirstOrDefault(x => x is ChromiumWebBrowser);
            rtb.Back();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser rtb = (ChromiumWebBrowser)tabControl1.SelectedTab.Controls.Cast<Control>().FirstOrDefault(x => x is ChromiumWebBrowser);
            rtb.Reload();
        }

        private void btnFront_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser rtb = (ChromiumWebBrowser)tabControl1.SelectedTab.Controls.Cast<Control>().FirstOrDefault(x => x is ChromiumWebBrowser);
            rtb.Forward();
        }

        private void btnDeletePage_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //https://www.google.com/search?q=123
            if (textBoxSearch.Text.Contains("http://") || textBoxSearch.Text.Contains("https://"))
            {
                CreateNewPage(textBoxSearch.Text);
            }
            else
            {
                CreateNewPage($"https://www.google.com/search?q={textBoxSearch.Text}");
            }
            textBoxSearch.Text = string.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyData != Keys.Enter)
            {
                return;
            }
            if (textBoxSearch.Text.Contains("http://") || textBoxSearch.Text.Contains("https://"))
            {
                CreateNewPage(textBoxSearch.Text);
            }
            else
            {
                CreateNewPage($"https://www.google.com/search?q={textBoxSearch.Text}");
            }
            textBoxSearch.Text = string.Empty;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
             ProfileForm profileForm = new ProfileForm();
            profileForm.ShowDialog();
        }

        private void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            string path = "browserMarkers.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                ChromiumWebBrowser rtb = (ChromiumWebBrowser)tabControl1.SelectedTab.Controls.Cast<Control>().FirstOrDefault(x => x is ChromiumWebBrowser);
                writer.WriteLine(rtb.Address);
            }
        }

        private void chromiumWebBrowser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            string path = "browserHistory.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string s = "";
                string v = "";
                while ((v = reader.ReadLine()) != null)
                {
                    s = v;
                }
                if (s.Equals(e.Address))
                {
                    return;
                }
                reader.Close();
            }
            // добавление в файл
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(e.Address);
                writer.Close();
            }
        }
    }
}