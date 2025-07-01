using System;
using System.Collections.Generic;
using System.Linq;

namespace UMS
{
    public static class StudentManager
    {
        public static void Student_Home()
        {
            Console.WriteLine("\n1- Show Existed Students\n" +
                                  "2- Add New Student\n" +
                                  "3- Modify a Student Data\n" +
                                  "4- Remove a Student\n" +
                                  "5- Assign/Unassign Subjects\n" +
                                  "6- Enter/Update Marks\n" +
                                  "0- Back to Previous Tab\n" +
                                  "Esc- Back to Home Screen\n");

            bool cond = false;
            while (!cond)
            {
                string Selective = Utilities.Normal_Key();
                switch (Selective)
                {
                    case "1":
                        Console.WriteLine("----------------");
                        Read();
                        cond = true;
                        break;

                    case "2":
                        Console.WriteLine("----------------");
                        Add();
                        cond = true;
                        break;

                    case "3":
                        Console.WriteLine("----------------");
                        Modify();
                        cond = true;
                        break;

                    case "4":
                        Console.WriteLine("----------------");
                        Remove();
                        cond = true;
                        break;

                    case "5":
                        Console.WriteLine("----------------");
                        AssignSubjects();
                        cond = true;
                        break;

                    case "6":
                        Console.WriteLine("----------------");
                        EnterMarks();
                        cond = true;
                        break;

                    case "0":
                        UMS_Manager.Main_Home();
                        cond = true;
                        break;

                    case "Esc":
                        UMS_Manager.Main_Home();
                        cond = true;
                        break;

                    default:
                        Console.WriteLine("Please, Enter a Valid Input");
                        break;
                }
            }
        }

        public static void Read()
        {
            Console.WriteLine("\n1- Show All Students Data\n" +
                                  "2- Search By ID\n" +
                                  "3- Search By Name\n" +
                                  "4- Show Evaluation Results\n" +
                                  "0- Back to Previous Tab\n" +
                                  "Esc- Back to Home Screen\n");

            bool cond = false;
            while (!cond)
            {
                string Selective = Utilities.Normal_Key();
                switch (Selective)
                {
                    case "1":
                        Console.WriteLine("----------------");
                        ShowAll();
                        cond = true;
                        break;

                    case "2":
                        Console.WriteLine("----------------");
                        SearchById();
                        cond = true;
                        break;

                    case "3":
                        Console.WriteLine("----------------");
                        SearchByName();
                        cond = true;
                        break;

                    case "4":
                        Console.WriteLine("----------------");
                        ShowEvaluation();
                        cond = true;
                        break;

                    case "0":
                        Utilities.DisplayHeader("Students Menu");
                        Student_Home();
                        cond = true;
                        break;

                    case "Esc":
                        UMS_Manager.Main_Home();
                        cond = true;
                        break;

                    default:
                        Console.WriteLine("Please, Enter a Valid Input");
                        break;
                }
            }
        }

        private static void ShowAll()
        {
            var students = DataManager.Students;
            if (students.Count == 0)
            {
                Console.WriteLine("\nNo students found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Students Menu");
                Student_Home();
                return;
            }

            foreach (var student in students)
            {
                Console.WriteLine($"\nID: {student.Student_ID}");
                Console.WriteLine($"Name: {student.Student_Name}");
                Console.WriteLine($"University: {student.Affiliated_University?.Uni_Name ?? "None"}");
                Console.WriteLine($"College: {student.Affiliated_College?.College_Name ?? "None"}");
                Console.WriteLine($"Subjects: {student.Affiliated_Subjects.Count}");
                Console.WriteLine($"Status: {(student.HasPassed() ? "Passed" : "Failed")}\n");
                Console.WriteLine("Subject Marks:");

                foreach (var subject in student.Affiliated_Subjects)
                {
                    var subjectMark = student.SubjectMarks.FirstOrDefault(sm => sm.SubjectId == subject.Subject_ID);
                    double mark = subjectMark?.Mark ?? 0;
                    Console.WriteLine($"- {subject.Subject_Name}: {mark}/100 {(mark >= 50 ? "Pass" : "Fail")}\n");
                }
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Students Menu");
            Student_Home();
        }

        private static void SearchById()
        {
            Console.Write("Enter Student ID: ");
            int id = Utilities.IntInput();
            var student = DataManager.Students.FirstOrDefault(s => s.Student_ID == id);

            if (student == null)
            {
                Console.WriteLine("\nStudent not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Students Menu");
                Student_Home();
                return;
            }

            Console.WriteLine($"\nID: {student.Student_ID}");
            Console.WriteLine($"Name: {student.Student_Name}");
            Console.WriteLine($"University: {student.Affiliated_University?.Uni_Name ?? "None"}");
            Console.WriteLine($"College: {student.Affiliated_College?.College_Name ?? "None"}");
            Console.WriteLine($"Subjects: {student.Affiliated_Subjects.Count}");
            Console.WriteLine($"Status: {(student.HasPassed() ? "Passed" : "Failed")}\n");
            Console.WriteLine("Subject Marks:");
            foreach (var subject in student.Affiliated_Subjects)
            {
                var subjectMark = student.SubjectMarks.FirstOrDefault(sm => sm.SubjectId == subject.Subject_ID);
                double mark = subjectMark?.Mark ?? 0;
                Console.WriteLine($"- {subject.Subject_Name}: {mark}/100 {(mark >= 50 ? "Pass" : "Fail")}\n");
            }


            Console.ReadLine();
            Utilities.DisplayHeader("Students Menu");
            Student_Home();
        }

        private static void SearchByName()
        {
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();
            var students = DataManager.Students.Where(s => s.Student_Name.Contains(name)).ToList();

            if (students.Count == 0)
            {
                Console.WriteLine("No students found with that name.");

                Console.ReadLine();
                Utilities.DisplayHeader("Students Menu");
                Student_Home();
                return;
            }

            foreach (var student in students)
            {
                Console.WriteLine($"\nID: {student.Student_ID}");
                Console.WriteLine($"Name: {student.Student_Name}");
                Console.WriteLine($"University: {student.Affiliated_University?.Uni_Name ?? "None"}");
                Console.WriteLine($"College: {student.Affiliated_College?.College_Name ?? "None"}");
                Console.WriteLine($"Subjects: {student.Affiliated_Subjects.Count}");
                Console.WriteLine($"Status: {(student.HasPassed() ? "Passed" : "Failed")} \n");
                Console.WriteLine("Subject Marks:");
                foreach (var subject in student.Affiliated_Subjects)
                {
                    var subjectMark = student.SubjectMarks.FirstOrDefault(sm => sm.SubjectId == subject.Subject_ID);
                    double mark = subjectMark?.Mark ?? 0;
                    Console.WriteLine($"- {subject.Subject_Name}: {mark}/100 {(mark >= 50 ? "Pass" : "Fail")}\n");
                }
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Students Menu");
            Student_Home();
        }

        private static void ShowEvaluation()
        {
            var students = DataManager.Students;
            if (students.Count == 0)
            {
                Console.WriteLine("\nNo students found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Students Menu");
                Student_Home();
                return;
            }

            int passed = students.Count(s => s.HasPassed());
            int failed = students.Count - passed;
            double percentage = students.Count == 0 ? 0 : (passed * 100.0 / students.Count);

            Console.WriteLine("\nStudent Evaluation Report");
            Console.WriteLine("-----------------------");
            Console.WriteLine($"Total Students: {students.Count}");
            Console.WriteLine($"Passed: {passed} ({percentage:F2}%)");
            Console.WriteLine($"Failed: {failed}\n");


            Console.ReadLine();
            Utilities.DisplayHeader("Students Menu");
            Student_Home();
        }

        public static void Add()
        {
            var student = new Student
            {
                Student_ID = DataManager.GetNextId(DataManager.Students)
            };

            Console.Write($"ID(Unique): {student.Student_ID}\n");
            Console.Write($"Name: ");
            student.Student_Name = Console.ReadLine();

            // Assign university
            Console.WriteLine("Available Universities:");
            foreach (var uni in DataManager.Universities)
            {
                Console.WriteLine($"{uni.Uni_ID}: {uni.Uni_Name}\n");
            }

            Console.Write("Enter University ID to assign (0 to skip): ");
            int uniId = Utilities.IntInput();
            if (uniId > 0)
            {
                var university = DataManager.Universities.FirstOrDefault(u => u.Uni_ID == uniId);
                if (university != null)
                {
                    student.Affiliated_University = university;
                }
            }

            // Assign college
            Console.WriteLine("Available Colleges:");
            foreach (var college in DataManager.Colleges)
            {
                Console.WriteLine($"{college.College_ID}: {college.College_Name}");
            }

            Console.Write("Enter College ID to assign (0 to skip): ");
            int collegeId = Utilities.IntInput();
            if (collegeId > 0)
            {
                var college = DataManager.Colleges.FirstOrDefault(c => c.College_ID == collegeId);
                if (college != null)
                {
                    student.Affiliated_College = college;
                }
            }

            DataManager.Students.Add(student);
            DataManager.SaveData();
            Console.WriteLine("\nStudent added successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Students Menu");
            Student_Home();
        }

        public static void Modify()
        {
            Console.Write("Enter Student ID to modify: ");
            int id = Utilities.IntInput();
            var student = DataManager.Students.FirstOrDefault(s => s.Student_ID == id);

            if (student == null)
            {
                Console.WriteLine("Student not found.");

                Console.ReadLine();
                Utilities.DisplayHeader("Students Menu");
                Student_Home();
                return;
            }

            Console.Write($"Current Name: {student.Student_Name}\nNew Name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                student.Student_Name = newName;
            }

            // Update university
            Console.WriteLine($"Current University: {student.Affiliated_University?.Uni_Name ?? "None"}\n");
            Console.WriteLine("Available Universities:");
            foreach (var uni in DataManager.Universities)
            {
                Console.WriteLine($"{uni.Uni_ID}: {uni.Uni_Name}");
            }

            Console.Write("Enter University ID to assign (0 to keep current): ");
            int uniId = Utilities.IntInput();
            if (uniId > 0)
            {
                var university = DataManager.Universities.FirstOrDefault(u => u.Uni_ID == uniId);
                if (university != null)
                {
                    student.Affiliated_University = university;
                }
            }

            // Update college
            Console.WriteLine($"Current College: {student.Affiliated_College?.College_Name ?? "None"} \n");
            Console.WriteLine("Available Colleges:");
            foreach (var college in DataManager.Colleges)
            {
                Console.WriteLine($"{college.College_ID}: {college.College_Name}");
            }

            Console.Write("Enter College ID to assign (0 to keep current): ");
            int collegeId = Utilities.IntInput();
            if (collegeId > 0)
            {
                var college = DataManager.Colleges.FirstOrDefault(c => c.College_ID == collegeId);
                if (college != null)
                {
                    student.Affiliated_College = college;
                }
            }

            DataManager.SaveData();
            Console.WriteLine("\nStudent updated successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Students Menu");
            Student_Home();
        }

        public static void Remove()
        {
            Console.Write("Enter Student ID to remove: ");
            int id = Utilities.IntInput();
            var student = DataManager.Students.FirstOrDefault(s => s.Student_ID == id);

            if (student == null)
            {
                Console.WriteLine("\nStudent not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Students Menu");
                Student_Home();
                return;
            }

            DataManager.Students.Remove(student);
            DataManager.SaveData();
            Console.WriteLine("\nStudent removed successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Students Menu");
            Student_Home();
        }

        public static void AssignSubjects()
        {
            Console.Write("Enter Student ID: ");
            int studentId = Utilities.IntInput();
            var student = DataManager.Students.FirstOrDefault(s => s.Student_ID == studentId);

            if (student == null)
            {
                Console.WriteLine("\nStudent not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Students Menu");
                Student_Home();
                return;
            }

            Console.WriteLine("\n1- Assign Subject\n2- Unassign Subject");
            string choice = Utilities.Normal_Key();

            if (choice == "1")
            {
                Console.WriteLine("Available Subjects:");
                foreach (var subject in DataManager.Subjects)
                {
                    Console.WriteLine($"{subject.Subject_ID}: {subject.Subject_Name}");
                }

                Console.Write("Enter Subject ID to assign (0 to finish): ");
                int subjectId;
                while ((subjectId = Utilities.IntInput()) != 0)
                {
                    var subject = DataManager.Subjects.FirstOrDefault(s => s.Subject_ID == subjectId);
                    if (subject == null)
                    {
                        Console.WriteLine("\nSubject not found.\n");
                    }
                    else if (student.Affiliated_Subjects.Contains(subject))
                    {
                        Console.WriteLine("\nSubject is already assigned to this student.\n");
                    }
                    else
                    {
                        student.Affiliated_Subjects.Add(subject);
                        Console.WriteLine("\nSubject assigned successfully!\n");
                    }

                    Console.Write("Enter Subject ID to assign (0 to finish): ");
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine("Assigned Subjects:");
                foreach (var subject in student.Affiliated_Subjects)
                {
                    Console.WriteLine($"{subject.Subject_ID}: {subject.Subject_Name}");
                }

                Console.Write("Enter Subject ID to unassign (0 to finish): ");
                int subjectId;
                while ((subjectId = Utilities.IntInput()) != 0)
                {
                    var subject = student.Affiliated_Subjects.FirstOrDefault(s => s.Subject_ID == subjectId);
                    if (subject == null)
                    {
                        Console.WriteLine("\nSubject not assigned to this student.\n");
                    }
                    else
                    {
                        student.Affiliated_Subjects.Remove(subject);

                        // Remove the mark for the subject
                        var subjectMark = student.SubjectMarks.FirstOrDefault(sm => sm.SubjectId == subjectId);
                        if (subjectMark != null)
                        {
                            student.SubjectMarks.Remove(subjectMark);
                        }

                        Console.WriteLine("\nSubject unassigned successfully!\n");
                    }
                }
            }

            DataManager.SaveData();

            Console.ReadLine();
            Utilities.DisplayHeader("Students Menu");
            Student_Home();
        }

        public static void EnterMarks()
        {
            Console.Write("Enter Student ID: ");
            int studentId = Utilities.IntInput();
            var student = DataManager.Students.FirstOrDefault(s => s.Student_ID == studentId);

            if (student == null)
            {
                Console.WriteLine("\nStudent not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Students Menu");
                Student_Home();
                return;
            }

            if (student.Affiliated_Subjects.Count == 0)
            {
                Console.WriteLine("\nStudent has no subjects assigned.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Students Menu");
                Student_Home();
                return;
            }

            Console.WriteLine("Assigned Subjects:");
            foreach (var subject in student.Affiliated_Subjects)
            {
                var subjectMark = student.SubjectMarks.FirstOrDefault(sm => sm.SubjectId == subject.Subject_ID);
                double mark = subjectMark?.Mark ?? 0;
                Console.WriteLine($"- {subject.Subject_Name}: {mark}/100 {(mark >= 50 ? "Pass" : "Fail")}\n");
            }

            Console.Write("Enter Subject ID to update mark (0 to finish): ");
            int subjectId;
            while ((subjectId = Utilities.IntInput()) != 0)
            {
                var subject = student.Affiliated_Subjects.FirstOrDefault(s => s.Subject_ID == subjectId);
                if (subject == null)
                {
                    Console.WriteLine("\nSubject not assigned to this student.\n");
                }
                else
                {
                    Console.Write($"Enter mark for {subject.Subject_Name} (0-100): ");
                    double mark;
                    while (!double.TryParse(Console.ReadLine(), out mark) || mark < 0 || mark > 100)
                    {
                        Console.WriteLine("Please enter a valid mark between 0 and 100.");
                        Console.Write($"Enter mark for {subject.Subject_Name} (0-100): ");
                    }
                    student.AddMark(subject.Subject_ID, mark);
                    Console.WriteLine("\nMark updated successfully!\n");
                }

                Console.Write("Enter Subject ID to update mark (0 to finish): ");
            }

            DataManager.SaveData();

            Console.ReadLine();
            Utilities.DisplayHeader("Students Menu");
            Student_Home();
        }
    }
}