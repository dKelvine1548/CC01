using CC01.BO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.DAL
{
    public class SchoolDAO
    {
        private School school;
        private const string FILE_NAME = @"school.json";
        private readonly string dbFolder;
        private FileInfo file;
        public SchoolDAO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            file = new FileInfo(Path.Combine(this.dbFolder, FILE_NAME));
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            if (!file.Exists)
            {
                file.Create().Close();
                file.Refresh();
            }
            if (file.Length > 0)
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    string json = sr.ReadToEnd();
                    school = JsonConvert.DeserializeObject<School>(json);
                }
            }
        }

        public void Add(School school)
        {
            using (StreamWriter sw = new StreamWriter(file.FullName, false))
            {
                string json = JsonConvert.SerializeObject(this.school);
                sw.WriteLine(json);
            }
        }

        public School Get()
        {
            return school;
        }
    }
}
