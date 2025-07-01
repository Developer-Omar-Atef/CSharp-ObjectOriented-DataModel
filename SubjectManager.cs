using System;
using System.Collections.Generic;
using System.Linq;

namespace UMS
{
    public static class SubjectManager
    {
        public static void Subject_Home()
        {
            Console.WriteLine("\n1- Show Existed Subjects\n" +
                                  "2- Add New Subject\n" +
                                  "3- Modify a Subject Data\n" +
                                  "4- Remove a Subject\n" +
                                  "5- Assign/Unassign Department\n" +
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
                        AssignDepartment();
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
            Console.WriteLine("\n1- Show All Subjects Data\n" +
                                  "2- Search By ID\n" +
                                  "3- Search By Name\n" +
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

                    case "0":
                        Utilities.DisplayHeader("Subjects Menu");
                        Subject_Home();
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
            var subjects = DataManager.Subjects;
            if (subjects.Count == 0)
            {
                Console.WriteLine("\nNo subjects found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Subjects Menu");
                Subject_Home();
                return;
            }

            foreach (var subject in subjects)
            {
                Console.WriteLine($"\nID: {subject.Subject_ID}");
                Console.WriteLine($"Name: {subject.Subject_Name}");
                Console.WriteLine($"Department: {subject.Affiliated_Department?.Department_Name ?? "None"}");
                Console.WriteLine($"Credit Hours: {subject.Credit_Hours}\n");
            }


            Console.ReadLine();
            Utilities.DisplayHeader("Subjects Menu");
            Subject_Home();
        }

        private static void SearchById()
        {
            Console.Write("Enter Subject ID: ");
            int id = Utilities.IntInput();
            var subject = DataManager.Subjects.FirstOrDefault(s => s.Subject_ID == id);

            if (subject == null)
            {
                Console.WriteLine("\nSubject not found.\n");


                Console.ReadLine();
                Utilities.DisplayHeader("Subjects Menu");
                Subject_Home();
                return;
            }

            Console.WriteLine($"\nID: {subject.Subject_ID}");
            Console.WriteLine($"Name: {subject.Subject_Name}");
            Console.WriteLine($"Department: {subject.Affiliated_Department?.Department_Name ?? "None"}");
            Console.WriteLine($"Credit Hours: {subject.Credit_Hours}\n");


            Console.ReadLine();
            Utilities.DisplayHeader("Subjects Menu");
            Subject_Home();
        }

        private static void SearchByName()
        {
            Console.Write("Enter Subject Name: ");
            string name = Console.ReadLine();
            var subjects = DataManager.Subjects.Where(s => s.Subject_Name.Contains(name)).ToList();

            if (subjects.Count == 0)
            {
                Console.WriteLine("\nNo subjects found with that name.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Subjects Menu");
                Subject_Home();
                return;
            }

            foreach (var subject in subjects)
            {
                Console.WriteLine($"\nID: {subject.Subject_ID}");
                Console.WriteLine($"Name: {subject.Subject_Name}");
                Console.WriteLine($"Department: {subject.Affiliated_Department?.Department_Name ?? "None"}");
                Console.WriteLine($"Credit Hours: {subject.Credit_Hours}\n");
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Subjects Menu");
            Subject_Home();
        }

        public static void Add()
        {
            var subject = new Subject
            {
                Subject_ID = DataManager.GetNextId(DataManager.Subjects)
            };

            Console.Write($"ID(Unique): {subject.Subject_ID}\n");
            Console.Write($"Name: ");
            subject.Subject_Name = Console.ReadLine();

            Console.Write($"Credit Hours: ");
            subject.Credit_Hours = Utilities.IsPositive();

            DataManager.Subjects.Add(subject);
            DataManager.SaveData();
            Console.WriteLine("\nSubject added successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Subjects Menu");
            Subject_Home();
        }

        public static void Modify()
        {
            Console.Write("Enter Subject ID to modify: ");
            int id = Utilities.IntInput();
            var subject = DataManager.Subjects.FirstOrDefault(s => s.Subject_ID == id);

            if (subject == null)
            {
                Console.WriteLine("\nSubject not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Subjects Menu");
                Subject_Home();
                return;
            }

            Console.Write($"Current Name: {subject.Subject_Name}\nNew Name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                subject.Subject_Name = newName;
            }

            Console.Write($"Current Credit Hours: {subject.Credit_Hours}\nNew Credit Hours (0 to keep current): ");
            int creditHours = Utilities.IntInput();
            if (creditHours > 0)
            {
                subject.Credit_Hours = creditHours;
            }

            DataManager.SaveData();
            Console.WriteLine("\nSubject updated successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Subjects Menu");
            Subject_Home();
        }

        public static void Remove()
        {
            Console.Write("Enter Subject ID to remove: ");
            int id = Utilities.IntInput();
            var subject = DataManager.Subjects.FirstOrDefault(s => s.Subject_ID == id);

            if (subject == null)
            {
                Console.WriteLine("\nSubject not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Subjects Menu");
                Subject_Home();
                return;
            }

            // Check if subject is assigned to any students
            bool isAssigned = DataManager.Students.Any(s => s.Affiliated_Subjects.Contains(subject));
            if (isAssigned)
            {
                Console.WriteLine("\nCannot delete subject that is assigned to students.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Subjects Menu");
                Subject_Home();
                return;
            }

            // Remove from department
            if (subject.Affiliated_Department != null)
            {
                subject.Affiliated_Department.Affiliated_Subjects.Remove(subject);
            }

            DataManager.Subjects.Remove(subject);
            DataManager.SaveData();
            Console.WriteLine("\nSubject removed successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Subjects Menu");
            Subject_Home();
        }

        public static void AssignDepartment()
        {
            Console.Write("Enter Subject ID: ");
            int subjectId = Utilities.IntInput();
            var subject = DataManager.Subjects.FirstOrDefault(s => s.Subject_ID == subjectId);

            if (subject == null)
            {
                Console.WriteLine("\nSubject not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Subjects Menu");
                Subject_Home();
                return;
            }

            Console.WriteLine("\n1- Assign Department\n2- Unassign Department");
            string choice = Utilities.Normal_Key();

            if (choice == "1")
            {
                Console.WriteLine("Available Departments:");
                foreach (var d in DataManager.Departments)
                {
                    Console.WriteLine($"{d.Department_ID}: {d.Department_Name}");
                }

                Console.Write("Enter Department ID to assign: ");
                int deptId = Utilities.IntInput();
                var dept = DataManager.Departments.FirstOrDefault(d => d.Department_ID == deptId);

                if (dept == null)
                {
                    Console.WriteLine("\nDepartment not found.\n");
                }
                else if (subject.Affiliated_Department == dept)
                {
                    Console.WriteLine("\nSubject is already assigned to this department.\n");
                }
                else
                {
                    // Remove from previous department if any
                    if (subject.Affiliated_Department != null)
                    {
                        subject.Affiliated_Department.Affiliated_Subjects.Remove(subject);
                    }

                    subject.Affiliated_Department = dept;
                    dept.Affiliated_Subjects.Add(subject);
                    DataManager.SaveData();
                    Console.WriteLine("\nDepartment assigned successfully!\n");
                }
            }
            else if (choice == "2")
            {
                if (subject.Affiliated_Department == null)
                {
                    Console.WriteLine("\nSubject is not assigned to any department.\n");
                }
                else
                {
                    // Check if subject is assigned to any students
                    bool isAssigned = DataManager.Students.Any(s => s.Affiliated_Subjects.Contains(subject));
                    if (isAssigned)
                    {
                        Console.WriteLine("\nCannot unassign department because subject is assigned to students.\n");
                    }
                    else
                    {
                        subject.Affiliated_Department.Affiliated_Subjects.Remove(subject);
                        subject.Affiliated_Department = null;
                        DataManager.SaveData();
                        Console.WriteLine("\nDepartment unassigned successfully!\n");
                    }
                }
            }


            Console.ReadLine();
            Utilities.DisplayHeader("Subjects Menu");
            Subject_Home();
        }
    }
}