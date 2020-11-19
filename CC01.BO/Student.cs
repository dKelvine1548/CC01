using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    public class Student
    {
        public string RegisterNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string PlaceBirth { get; set; }
        public long Telephone { get; set; }
        public string Email { get; set; }
        public byte[]Picture { get; set; }
        public string NameSchool{ get; set; }

        public Student()
        {

        }
        public Student(string registerNumber, string name, string surname,
            DateTime birthDay, string placeBirth, long telephone, string email, byte[] picture, string nameSchool)
        {
            RegisterNumber = registerNumber;
            Name = name;
            Surname = surname;
            BirthDay = birthDay;
            PlaceBirth = placeBirth;
            Telephone = telephone;
            Email = email;
            Picture = picture;
            NameSchool = nameSchool;
        }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                 RegisterNumber.Equals(student.RegisterNumber, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return 73200889 + EqualityComparer<string>.Default.GetHashCode(RegisterNumber);
        }
    }
}
