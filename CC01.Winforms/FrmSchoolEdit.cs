using CC01.BLL;
using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.Winforms
{
    public partial class FrmSchoolEdit : Form
    {
        private Action callBack;
        private School oldSchool;

        public FrmSchoolEdit()
        {
            InitializeComponent();

        }
        public FrmSchoolEdit(Action callBack) : this()
        {
            this.callBack = callBack;
        }

        public FrmSchoolEdit(School school, Action callBack) : this(callBack)
        {
            this.oldSchool = school;
            txtNameSchool.Text = school.NameSchool;
            txtEmail.Text = school.Email;
            if (school.Logo != null)
                pictureBox1.Image = Image.FromStream(new MemoryStream(school.Logo));
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
                    !string.IsNullOrEmpty(pictureBox1.ImageLocation)?File.ReadAllBytes(pictureBox1.ImageLocation) : this.oldSchool?.Logo
                );

                SchoolBLO schoolBLO = new SchoolBLO(ConfigurationManager.AppSettings["DbFolder"]);
                

                if (this.oldSchool == null)
                    schoolBLO.CreateSchool(newSchool);
                else
                    schoolBLO.EditSchool(oldSchool, newSchool);

                MessageBox.Show
                (
                    "Save done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                if (callBack != null)
                    callBack();

                if (oldSchool != null)
                    Close();

                txtNameSchool.Clear();
                txtEmail.Clear();
                txtSchoolNumber.Clear();
                txtNameSchool.Focus();

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
            catch (DuplicateNameException ex)
            {
                ex.WriteToFile();
                MessageBox.Show
               (
                   ex.Message,
                   "Duplicate error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }

            catch (KeyNotFoundException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Not found error",
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

