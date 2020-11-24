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

        SchoolDAO schoolRepo;
     
        public SchoolBLO(string dbFolder)
        {
            schoolRepo = new SchoolDAO(dbFolder);
        }

        public void CreateSchool(School school)
        {
            schoolRepo.Add(school);
        }

        public IEnumerable<School> GetAllSchools()
        {
            return schoolRepo.Find(); 
        }

        public void EditSchool(School oldSchool, School newSchool)
        {
            schoolRepo.Set(oldSchool, newSchool);
        }

        public void DeleteSchool(School school)
        {
            schoolRepo.Remove(school);
        }

        public IEnumerable<School> GetByName(string nameSchool)
        {
            return schoolRepo.Find(x => x.NameSchool == nameSchool);
        }

       public IEnumerable<School> GetBy(Func<School, bool> predicate)
        {
            return schoolRepo.Find(predicate);
        }

    }
}
