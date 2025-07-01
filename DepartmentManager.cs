using System;
using System.Collections.Generic;
using System.Linq;

namespace UMS
{
    public static class DepartmentManager
    {
        public static void Department_Home()
        {
            Console.WriteLine("\n1- Show Existed Departments\n" +
                                  "2- Add New Department\n" +
                                  "3- Modify a Department Data\n" +
                                  "4- Remove a Department\n" +
                                  "5- Assign/Unassign College\n" +
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
                        AssignCollege();
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
            Console.WriteLine("\n1- Show All Departments Data\n" +
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
                        Utilities.DisplayHeader("Departments Menu");
                        Department_Home();
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
            var departments = DataManager.Departments;
            if (departments.Count == 0)
            {
                Console.WriteLine("\nNo departments found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Departments Menu");
                Department_Home();
                return;
            }

            foreach (var dept in departments)
            {
                Console.WriteLine($"\nID: {dept.Department_ID}");
                Console.WriteLine($"Name: {dept.Department_Name}");
                Console.WriteLine($"College: {dept.Affiliated_College?.College_Name ?? "None"}");
                Console.WriteLine($"Students: {dept.Affiliated_Students.Count}");
                Console.WriteLine($"Subjects: {dept.Affiliated_Subjects.Count}");
                Console.WriteLine($"Success Percentage: {dept.SuccessPercentage:F2}%\n");
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Departments Menu");
            Department_Home();
        }

        private static void SearchById()
        {
            Console.Write("Enter Department ID: ");
            int id = Utilities.IntInput();
            var dept = DataManager.Departments.FirstOrDefault(d => d.Department_ID == id);

            if (dept == null)
            {
                Console.WriteLine("\nDepartment not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Departments Menu");
                Department_Home();
                return;
            }

            Console.WriteLine($"\nID: {dept.Department_ID}");
            Console.WriteLine($"Name: {dept.Department_Name}");
            Console.WriteLine($"College: {dept.Affiliated_College?.College_Name ?? "None"}");
            Console.WriteLine($"Students: {dept.Affiliated_Students.Count}");
            Console.WriteLine($"Subjects: {dept.Affiliated_Subjects.Count}");
            Console.WriteLine($"Success Percentage: {dept.SuccessPercentage:F2}%\n");


            Console.ReadLine();
            Utilities.DisplayHeader("Departments Menu");
            Department_Home();
        }

        private static void SearchByName()
        {
            Console.Write("Enter Department Name: ");
            string name = Console.ReadLine();
            var departments = DataManager.Departments.Where(d => d.Department_Name.Contains(name)).ToList();

            if (departments.Count == 0)
            {
                Console.WriteLine("\nNo departments found with that name.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Departments Menu");
                Department_Home();
                return;
            }

            foreach (var dept in departments)
            {
                Console.WriteLine($"\nID: {dept.Department_ID}");
                Console.WriteLine($"Name: {dept.Department_Name}");
                Console.WriteLine($"College: {dept.Affiliated_College?.College_Name ?? "None"}");
                Console.WriteLine($"Students: {dept.Affiliated_Students.Count}");
                Console.WriteLine($"Subjects: {dept.Affiliated_Subjects.Count}");
                Console.WriteLine($"Success Percentage: {dept.SuccessPercentage:F2}%\n");
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Departments Menu");
            Department_Home();
        }

        private static void ShowEvaluation()
        {
            var departments = DataManager.Departments.OrderByDescending(d => d.SuccessPercentage).ToList();
            if (departments.Count == 0)
            {
                Console.WriteLine("\nNo departments found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Departments Menu");
                Department_Home();
                return;
            }

            Console.WriteLine("\nDepartment Evaluation Report (Ordered by Success Percentage)");
            Console.WriteLine("----------------------------------------------------------");
            foreach (var dept in departments)
            {
                var results = dept.GetStudentResults();
                int total = results.passed + results.failed;
                Console.WriteLine($"\nDepartment: {dept.Department_Name}");
                Console.WriteLine($"College: {dept.Affiliated_College?.College_Name ?? "None"}");
                Console.WriteLine($"Passed: {results.passed} ({dept.SuccessPercentage:F2}%)");
                Console.WriteLine($"Failed: {results.failed}");
                Console.WriteLine($"Total Students: {total}\n");
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Departments Menu");
            Department_Home();
        }

        public static void Add()
        {
            var dept = new Department
            {
                Department_ID = DataManager.GetNextId(DataManager.Departments)
            };

            Console.Write($"ID(Unique): {dept.Department_ID}\n");
            Console.Write($"Name: ");
            dept.Department_Name = Console.ReadLine();

            DataManager.Departments.Add(dept);
            DataManager.SaveData();
            Console.WriteLine("\nDepartment added successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Departments Menu");
            Department_Home();
        }

        public static void Modify()
        {
            Console.Write("Enter Department ID to modify: ");
            int id = Utilities.IntInput();
            var dept = DataManager.Departments.FirstOrDefault(d => d.Department_ID == id);

            if (dept == null)
            {
                Console.WriteLine("\nDepartment not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Departments Menu");
                Department_Home();
                return;
            }

            Console.Write($"Current Name: {dept.Department_Name}\nNew Name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                dept.Department_Name = newName;
            }

            DataManager.SaveData();
            Console.WriteLine("\nDepartment updated successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Departments Menu");
            Department_Home();
        }

        public static void Remove()
        {
            Console.Write("Enter Department ID to remove: ");
            int id = Utilities.IntInput();
            var dept = DataManager.Departments.FirstOrDefault(d => d.Department_ID == id);

            if (dept == null)
            {
                Console.WriteLine("\nDepartment not found.\n");
                
                Console.ReadLine();
                Utilities.DisplayHeader("Departments Menu");
                Department_Home();
                return;
            }

            // Check if department has students or subjects
            if (dept.Affiliated_Students.Count > 0 || dept.Affiliated_Subjects.Count > 0)
            {
                Console.WriteLine("\nCannot delete department with students or subjects. Please remove them first.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Departments Menu");
                Department_Home();
                return;
            }

            // Remove from college
            if (dept.Affiliated_College != null)
            {
                dept.Affiliated_College.Affiliated_Departments.Remove(dept);
            }

            DataManager.Departments.Remove(dept);
            DataManager.SaveData();
            Console.WriteLine("\nDepartment removed successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Departments Menu");
            Department_Home();
        }

        public static void AssignCollege()
        {
            Console.Write("Enter Department ID: ");
            int deptId = Utilities.IntInput();
            var dept = DataManager.Departments.FirstOrDefault(d => d.Department_ID == deptId);

            if (dept == null)
            {
                Console.WriteLine("\nDepartment not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Departments Menu");
                Department_Home();
                return;
            }

            Console.WriteLine("\n1- Assign College\n2- Unassign College");
            string choice = Utilities.Normal_Key();

            if (choice == "1")
            {
                Console.WriteLine("Available Colleges:");
                foreach (var col in DataManager.Colleges)
                {
                    Console.WriteLine($"{col.College_ID}: {col.College_Name}\n");
                }

                Console.Write("Enter College ID to assign: ");
                int collegeId = Utilities.IntInput();
                var college = DataManager.Colleges.FirstOrDefault(c => c.College_ID == collegeId);

                if (college == null)
                {
                    Console.WriteLine("\nCollege not found.\n");
                }
                else if (dept.Affiliated_College == college)
                {
                    Console.WriteLine("\nDepartment is already assigned to this college.\n");
                }
                else
                {
                    // Remove from previous college if any
                    if (dept.Affiliated_College != null)
                    {
                        dept.Affiliated_College.Affiliated_Departments.Remove(dept);
                    }

                    dept.Affiliated_College = college;
                    college.Affiliated_Departments.Add(dept);
                    DataManager.SaveData();
                    Console.WriteLine("\nCollege assigned successfully!\n");
                }
            }
            else if (choice == "2")
            {
                if (dept.Affiliated_College == null)
                {
                    Console.WriteLine("\nDepartment is not assigned to any college.\n");
                }
                else
                {
                    // Check if department has students or subjects
                    if (dept.Affiliated_Students.Count > 0 || dept.Affiliated_Subjects.Count > 0)
                    {
                        Console.WriteLine("\nCannot unassign college because department has students or subjects.\n");
                    }
                    else
                    {
                        dept.Affiliated_College.Affiliated_Departments.Remove(dept);
                        dept.Affiliated_College = null;
                        DataManager.SaveData();
                        Console.WriteLine("\nCollege unassigned successfully!\n");
                    }
                }
            }


            Console.ReadLine();
            Utilities.DisplayHeader("Departments Menu");
            Department_Home();
        }
    }
}