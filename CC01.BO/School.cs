
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    public class School
    {
        public string NameSchool { get; set; }
        public string Email { get; set; }
        public long SchoolNumber { get; set; }
        public byte[] Logo { get; set; }

        public School()
        {

        }
        public School(string nameSchool, string email, long schoolNumber, byte[] logo)
        {
            NameSchool = nameSchool;
            Email = email;
            SchoolNumber = schoolNumber;
            Logo = logo;
        }

        public override bool Equals(object obj)
        {
            return obj is School school &&
           NameSchool.Equals(school.NameSchool, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return -251229326 + EqualityComparer<string>.Default.GetHashCode(NameSchool);
        }
    }
}
