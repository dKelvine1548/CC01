using CC01.BLL;
using CC01.BO;
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
    public partial class FrmStudentEdit : Form
    {

        private Action callBack;
        private Student oldStudent;
        public FrmStudentEdit()
        {
            InitializeComponent();
        }

        public FrmStudentEdit(Action callBack) : this()
        {
            this.callBack = callBack;
        }

        public FrmStudentEdit(Student student, Action callBack) : this(callBack)
        {
            this.oldStudent = student;
            txtName.Text = student.Name;
            txtSurname.Text = student.Surname;
            txtRegisterNumber.Text = student.RegisterNumber;
            dtpBirthday.Text = student.BirthDay.ToString();
            txtPlaceBirth.Text = student.PlaceBirth;
            txtNameSchool.Text = student.NameSchool;
            txtEmail.Text = student.Email;
            if (student.Picture != null)
                pictureBox1.Image = Image.FromStream(new MemoryStream(student.Picture));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();
                Student newStudent = new Student
                (
                    txtRegisterNumber.Text.ToUpper(),
                    txtName.Text,
                    txtSurname.Text,
                    DateTime.Parse(dtpBirthday.Text),
                    txtPlaceBirth.Text,
                    long.Parse(txtTelephone.Text),
                    txtEmail.Text,
                    txtNameSchool.Text,
                    !string.IsNullOrEmpty(pictureBox1.ImageLocation) ? File.ReadAllBytes(pictureBox1.ImageLocation) : this.oldStudent?.Picture
                );
                StudentBLO studentBLO1 = new StudentBLO(ConfigurationManager.AppSettings["DbFolder"]);
                StudentBLO studentBLO = studentBLO1;

                if (this.oldStudent == null)
                    studentBLO.CreateStudent(newStudent);
                else
                    studentBLO.EditStudent(oldStudent, newStudent);

                MessageBox.Show
                (
                    "Save done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                if (callBack != null)
                    callBack();

                if (oldStudent != null)
                    Close();

                txtRegisterNumber.Clear();
                txtName.Clear();
                txtSurname.Clear();
                txtPlaceBirth.Clear();
                txtTelephone.Clear();
                txtEmail.Clear();
                txtNameSchool.Clear();
                txtRegisterNumber.Focus();

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
            txtRegisterNumber.BackColor = Color.White;
            txtName.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(txtRegisterNumber.Text))
            {
                text += "- Please enter the reference ! \n";
                txtRegisterNumber.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                text += "- Please enter the name ! \n";
                txtName.BackColor = Color.Pink;
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

