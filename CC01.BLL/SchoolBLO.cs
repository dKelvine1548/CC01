using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BLL
{
    public class SchoolBLO
    {
        private SchoolDAO schoolRepo;

        private string dbFolder;
        public SchoolBLO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            schoolRepo = new SchoolDAO(dbFolder);
        }
        public void CreateCompany(School oldSchool, School newSchool)
        {
            string filename = null;
            if (!string.IsNullOrEmpty(newSchool.Logo))
            {
                string ext = Path.GetExtension(newSchool.Logo);
                filename = Guid.NewGuid().ToString() + ext;
                FileInfo fileSource = new FileInfo(newSchool.Logo);
                string filePath = Path.Combine(dbFolder, "logo", filename);
                FileInfo fileDest = new FileInfo(filePath);
                if (!fileDest.Directory.Exists)
                    fileDest.Directory.Create();
                fileSource.CopyTo(fileDest.FullName);
            }
            newSchool.Logo = filename;
            schoolRepo.Add(newSchool);

            if (!string.IsNullOrEmpty(oldSchool.Logo))
                File.Delete(oldSchool.Logo);
        }

        public School GetSchool()
        {
            School school = schoolRepo.Get();
            if (school != null)
                if (!string.IsNullOrEmpty(school.Logo))
                    school.Logo = Path.Combine(dbFolder, "logo", school.Logo);
            return school;
        }

        //public void EditSchool(School oldSchool, School newSchool)
        //{
        //    schoolRepo.Set(oldSchool, newSchool);
        //}

        //public void DeleteSchool(School school)
        //{
        //    schoolRepo.Remove(school);
        //}


        //public IEnumerable<School> GetAllSchool()
        //{
        //    return schoolRepo.Find();
        //}


        //public IEnumerable<Student> GetByRegisterNumber(string registerNumber)
        //{
        //    return schoolRepo.Find(x => x.RegisterNumber == registerNumber);
        //}

        //public IEnumerable<Student> GetBy(Func<Student, bool> predicate)
        //{
        //    return schoolRepo.Find(predicate);
        //}

       
    }
}
