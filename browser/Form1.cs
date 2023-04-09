using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

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
        public void CreateNewPage(string link)
        {
            TabPage newTabPage = new TabPage();
            newTabPage.Text = "Новая вкладка";

            tabControl1.TabPages.Add(newTabPage);
            tabControl1.SelectedIndex = tabControl1.TabCount - 1;

            ChromiumWebBrowser chrome;
            chrome = new ChromiumWebBrowser(link);
            // Add it to the form and fill it to the form window.
            tabControl1.SelectedTab.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
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

        }
    }
}