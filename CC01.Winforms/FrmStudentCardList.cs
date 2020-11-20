using CC01.BLL;
using CC01.BO;
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
    public partial class FrmStudentCardList : Form
    {
        private StudentBLO studentBLO;

        public FrmStudentCardList()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            studentBLO = new StudentBLO(ConfigurationManager.AppSettings["DbFolder"]);
          
        }
        private void loadData()
        {

            string value = txtSearch.Text.ToLower();
            var students = studentBLO.GetBy
            (
                x =>
                x.RegisterNumber.ToLower().Contains(value) ||
                x.Name.ToLower().Contains(value)
            ).OrderBy(x => x.RegisterNumber).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;
            lblRowCount.Text = $"{dataGridView1.RowCount} rows";
            dataGridView1.ClearSelection();

        }
       
        
        private void FrmStudentCardList_Load(object sender, EventArgs e)
        {
            loadData();
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click_1(sender, e);
        }

        
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            Form f = new FrmStudentEdit(loadData);
            f.Show();
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    Form f = new FrmStudentEdit
                    (
                        dataGridView1.SelectedRows[i].DataBoundItem as Student,
                        loadData
                    );
                    f.ShowDialog();
                }
            }
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            List<StudentListPrint> items = new List<StudentListPrint>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Student s = dataGridView1.Rows[i].DataBoundItem as Student;

                items.Add
                (
                   new StudentListPrint
                   (
                       s.RegisterNumber,
                       s.Name,
                       s.Surname,
                       s.NameSchool,
                       s.Picture
                    )
                );
            }
            Form f = new FrmStudentPreview("StudentListRpt.rdlc", items);
            f.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (
                    MessageBox.Show
                    (
                        "Do you really want to delete this student(s)?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes
                )
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        studentBLO.DeleteStudent(dataGridView1.SelectedRows[i].DataBoundItem as Student);
                    }
                    loadData();
                }
            }
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
