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

namespace browser
{
    public partial class ProfileForm : Form
    {
        public ProfileForm()
        {
            InitializeComponent();
            GetHistory();
        }
        public void GetHistory()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns[0].HeaderText = "История";
            string path = "browserHistory.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string s = "";
                while ((s = reader.ReadLine()) != null)
                {
                    int rowNumber = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowNumber].Cells["column"].Value = s;
                }
            }
        }
        public void GetMarks()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns[0].HeaderText = "Избранное";
            string path = "browserMarkers.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string s = "";
                while ((s = reader.ReadLine()) != null)
                {
                    int rowNumber = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowNumber].Cells["column"].Value = s;
                }
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            GetHistory();
        }


        private void btnMarkers_Click(object sender, EventArgs e)
        {
            GetMarks();
        }
    }
}
