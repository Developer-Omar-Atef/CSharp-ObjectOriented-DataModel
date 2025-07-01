using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace UMS
{
    public static class DataManager
    {
        private static List<University> universities = new List<University>();
        private static List<College> colleges = new List<College>();
        private static List<Department> departments = new List<Department>();
        private static List<Subject> subjects = new List<Subject>();
        private static List<Student> students = new List<Student>();

        private const string DataFilePath = @"..\..\..\ums_data.xml";

        public static List<University> Universities => universities;
        public static List<College> Colleges => colleges;
        public static List<Department> Departments => departments;
        public static List<Subject> Subjects => subjects;
        public static List<Student> Students => students;

        public static void SaveData()
        {
            var data = new UMSData
            {
                Universities = universities,
                Colleges = colleges,
                Departments = departments,
                Subjects = subjects,
                Students = students
            };

            var serializer = new XmlSerializer(typeof(UMSData));
            using (var writer = new StreamWriter(DataFilePath))
            {
                serializer.Serialize(writer, data);
            }
        }

        public static void LoadData()
        {
            var serializer = new XmlSerializer(typeof(UMSData));
            using (var reader = new StreamReader(DataFilePath))
            {
                var data = (UMSData)serializer.Deserialize(reader);
                universities = data.Universities;
                colleges = data.Colleges;
                departments = data.Departments;
                subjects = data.Subjects;
                students = data.Students;
            }
        }

        public static int GetNextId<T>(List<T> list) where T : class
        {
            if (list.Count == 0) return 1;

            var type = typeof(T);
            if (type == typeof(University))
                return ((List<University>)(object)list).Max(u => u.Uni_ID) + 1;
            else if (type == typeof(College))
                return ((List<College>)(object)list).Max(c => c.College_ID) + 1;
            else if (type == typeof(Department))
                return ((List<Department>)(object)list).Max(d => d.Department_ID) + 1;
            else if (type == typeof(Subject))
                return ((List<Subject>)(object)list).Max(s => s.Subject_ID) + 1;
            else if (type == typeof(Student))
                return ((List<Student>)(object)list).Max(s => s.Student_ID) + 1;

            return 1;
        }

        public static void RelinkReferences()
        {
            // Relink departments inside colleges
            foreach (var college in colleges)
            {
                college.Affiliated_Departments = departments
                    .Where(d => d.Affiliated_College?.College_ID == college.College_ID).ToList();
            }

            // Relink universities inside colleges
            foreach (var college in colleges)
            {
                college.Affiliated_Universities = universities
                    .Where(u => u.Affiliated_Colleges.Any(c => c.College_ID == college.College_ID)).ToList();
            }

            // Relink colleges inside universities
            foreach (var university in universities)
            {
                university.Affiliated_Colleges = colleges
                    .Where(c => c.Affiliated_Universities.Any(u => u.Uni_ID == university.Uni_ID)).ToList();
            }

            // Relink subjects to departments
            foreach (var subject in subjects)
            {
                subject.Affiliated_Department = departments
                    .FirstOrDefault(d => d.Affiliated_Subjects.Any(s => s.Subject_ID == subject.Subject_ID));
            }

            // Relink students
            foreach (var student in students)
            {
                student.Affiliated_University = universities
                    .FirstOrDefault(u => u.Uni_ID == student.Affiliated_University?.Uni_ID);

                student.Affiliated_College = colleges
                    .FirstOrDefault(c => c.College_ID == student.Affiliated_College?.College_ID);

                student.Affiliated_Subjects = subjects
                    .Where(s => student.Affiliated_Subjects.Any(sub => sub.Subject_ID == s.Subject_ID)).ToList();
            }

            // Link students back to departments
            foreach (var dept in departments)
            {
                dept.Affiliated_Students = students
                    .Where(s => s.Affiliated_College?.College_ID == dept.Affiliated_College?.College_ID).ToList();

                dept.Affiliated_Subjects = subjects
                    .Where(s => s.Affiliated_Department?.Department_ID == dept.Department_ID).ToList();
            }
        }
    }

    [Serializable]
    public class UMSData
    {
        public List<University> Universities { get; set; }
        public List<College> Colleges { get; set; }
        public List<Department> Departments { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Student> Students { get; set; }

    }
}
