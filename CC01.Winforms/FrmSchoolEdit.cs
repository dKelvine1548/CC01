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
    public partial class FrmSchoolEdit : Form
    {
        private SchoolBLO schoolBLO;
        private School oldSchool;
        public FrmSchoolEdit()
        {
            InitializeComponent();
            schoolBLO = new SchoolBLO(ConfigurationManager.AppSettings["DbFolder"]);
            oldSchool = schoolBLO.GetSchool();
            if (oldSchool != null)
            {
                txtNameSchool.Text = oldSchool.NameSchool;
                txtEmail.Text = oldSchool.Email;
                txtSchoolNumber.Text = oldSchool.SchoolNumber.ToString();
                pictureBox1.ImageLocation = oldSchool.Logo;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();

                School newSchool = new School
                (
                    txtNameSchool.Text.ToUpper(),
                    txtEmail.Text,
                    long.Parse(txtSchoolNumber.Text),
                    pictureBox1.ImageLocation
                );

                schoolBLO.CreateCompany(oldSchool, newSchool);

                MessageBox.Show
                (
                    "Save done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();


            }
            catch (TypingException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Typing error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
            catch (Exception ex)
            {
                ex.WriteToFile();
                MessageBox.Show
               (
                   "An error occurred! Please try again later.",
                   "Erreur",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );
            }
        }

        private void checkForm()
        {
            string text = string.Empty;
            txtNameSchool.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            if (!long.TryParse(txtSchoolNumber.Text, out _))
            {
                text += "- Please enter a good school number ! \n";
                txtNameSchool.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtNameSchool.Text))
            {
                text += "- Please enter the name ! \n";
                txtEmail.BackColor = Color.Pink;
            }

            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose a picture";
            ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;
        }
    }
}

