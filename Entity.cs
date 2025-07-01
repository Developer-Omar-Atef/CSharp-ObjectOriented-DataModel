using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UMS
{
    public class University
    {
        public int Uni_ID { get; set; }
        public string Uni_Name { get; set; }
        public string Uni_Location { get; set; }
        public List<College> Affiliated_Colleges { get; set; } = new List<College>();

        public char Classification
        {
            get
            {
                double percentage = SuccessPercentage;
                if (percentage >= 90) return 'A';
                if (percentage >= 80) return 'B';
                if (percentage >= 70) return 'C';
                if (percentage >= 60) return 'D';
                return 'E';
            }
        }

        public double SuccessPercentage
        {
            get
            {
                if (Affiliated_Colleges.Count == 0) return 0;
                return Affiliated_Colleges.Average(c => c.SuccessPercentage);
            }
        }

        public (int passed, int failed) GetStudentResults()
        {
            int passed = 0, failed = 0;
            foreach (var college in Affiliated_Colleges)
            {
                var results = college.GetStudentResults();
                passed += results.passed;
                failed += results.failed;
            }
            return (passed, failed);
        }
    }

    public class College
    {
        public int College_ID { get; set; }
        public string College_Name { get; set; }

        [XmlIgnore]
        public List<Department> Affiliated_Departments { get; set; } = new List<Department>();

        [XmlIgnore]
        public List<University> Affiliated_Universities { get; set; } = new List<University>();

        public char Classification
        {
            get
            {
                double percentage = SuccessPercentage;
                if (percentage >= 90) return 'A';
                if (percentage >= 80) return 'B';
                if (percentage >= 70) return 'C';
                if (percentage >= 60) return 'D';
                return 'E';
            }
        }

        public double SuccessPercentage
        {
            get
            {
                if (Affiliated_Departments.Count == 0) return 0;
                return Affiliated_Departments.Average(d => d.SuccessPercentage);
            }
        }

        public (int passed, int failed) GetStudentResults()
        {
            int passed = 0, failed = 0;
            foreach (var dept in Affiliated_Departments)
            {
                var results = dept.GetStudentResults();
                passed += results.passed;
                failed += results.failed;
            }
            return (passed, failed);
        }
    }

    public class Department
    {
        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public College Affiliated_College { get; set; }

        //[XmlIgnore]
        public List<Student> Affiliated_Students { get; set; } = new List<Student>();

        //[XmlIgnore]
        public List<Subject> Affiliated_Subjects { get; set; } = new List<Subject>();

        

        public double SuccessPercentage
        {
            get
            {
                if (Affiliated_Students.Count == 0) return 0;
                var results = GetStudentResults();
                int total = results.passed + results.failed;
                return total == 0 ? 0 : (results.passed * 100.0 / total);
            }
        }

        public (int passed, int failed) GetStudentResults()
        {
            int passed = 0, failed = 0;
            foreach (var student in Affiliated_Students)
            {
                if (student.HasPassed()) passed++;
                else failed++;
            }
            return (passed, failed);
        }
    }

    public class Student
    {
        public int Student_ID { get; set; }
        public string Student_Name { get; set; }
        public University Affiliated_University { get; set; }
        public College Affiliated_College { get; set; }

        public List<Subject> Affiliated_Subjects { get; set; } = new List<Subject>();

        public List<SubjectMark> SubjectMarks { get; set; } = new List<SubjectMark>();
        public class SubjectMark
        {
            public int SubjectId { get; set; }
            public double Mark { get; set; }
        }

        public bool HasPassed()
        {
            if (Affiliated_Subjects.Count == 0) return false;

            return Affiliated_Subjects.All(sub =>
                SubjectMarks.Any(mark => mark.SubjectId == sub.Subject_ID && mark.Mark >= 50));
        }

        public void AddMark(int subjectId, double mark)
        {
            var existingMark = SubjectMarks.FirstOrDefault(sm => sm.SubjectId == subjectId);
            if (existingMark != null)
            {
                existingMark.Mark = mark;
            }
            else
            {
                SubjectMarks.Add(new SubjectMark { SubjectId = subjectId, Mark = mark });
            }
        }
    }

    public class Subject
    {
        public int Subject_ID { get; set; }
        public string Subject_Name { get; set; }
        public int Credit_Hours { get; set; }


        public Department Affiliated_Department { get; set; }
    }
}
