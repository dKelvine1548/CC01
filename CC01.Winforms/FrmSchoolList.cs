using CC01.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.Winforms
{
    public partial class FrmSchoolList : Form
    {

        private SchoolBLO schoolBLO;
        
        public FrmSchoolList()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            schoolBLO = new SchoolBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        //private void loadData()
        //{
        //    string value = txtSearch.Text.ToLower();
        //    var schools = schoolBLO.GetSchool
        //    (
        //        x =>
        //        x.Reference.ToLower().Contains(value) ||
        //        x.NameSchool.ToLower().Contains(value)
        //    ).OrderBy(x => x.Reference).ToArray();
        //    dataGridView1.DataSource = null;
        //    dataGridView1.DataSource = schools;
        //    lblRowCount.Text = $"{dataGridView1.RowCount} rows";
        //    dataGridView1.ClearSelection();
        //}
    }
}
