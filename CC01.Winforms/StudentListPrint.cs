using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.Winforms
{
   public class StudentListPrint
   { 
        public string RegisterNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NameSchool { get; set; }
        public byte[] Picture { get; set; }

        public StudentListPrint(string registerNumber, string name, string surname, 
            string nameSchool, byte[] picture)
        {
            RegisterNumber = registerNumber;
            Name = name;
            Surname = surname;
            NameSchool = nameSchool;
            Picture = picture;
        }
    }
}
