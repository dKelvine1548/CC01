using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    public class Student
    {
        public string RegisterNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string PlaceBirth { get; set; }
        public long Telephone { get; set; }
        public string Email { get; set; }
        public string NameSchool{ get; set; }
        public byte[] Picture { get; set; }

        public Student()
        {

        }

        public Student(string registerNumber, string name, string surname, DateTime birthDay, string placeBirth, long telephone, string email, string nameSchool, byte[] picture)
        {
            RegisterNumber = registerNumber;
            Name = name;
            Surname = surname;
            BirthDay = birthDay;
            PlaceBirth = placeBirth;
            Telephone = telephone;
            Email = email;
            NameSchool = nameSchool;
            Picture = picture;
        }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   RegisterNumber == student.RegisterNumber;
        }

        public override int GetHashCode()
        {
            return 73200889 + EqualityComparer<string>.Default.GetHashCode(RegisterNumber);
        }
    }
}
