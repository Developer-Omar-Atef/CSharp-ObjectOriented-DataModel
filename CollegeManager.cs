using System;
using System.Collections.Generic;
using System.Linq;

namespace UMS
{
    public static class CollegeManager
    {
        public static void College_Home()
        {
            Console.WriteLine("\n1- Show Existed Colleges\n" +
                                  "2- Add New College\n" +
                                  "3- Modify a College Data\n" +
                                  "4- Remove a College\n" +
                                  "5- Assign/Unassign University\n" +
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
                        AssignUniversity();
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
            Console.WriteLine("\n1- Show All Colleges Data\n" +
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
                        Utilities.DisplayHeader("Colleges Menu");
                        College_Home();
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
            var colleges = DataManager.Colleges;
            if (colleges.Count == 0)
            {
                Console.WriteLine("\nNo colleges found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Colleges Menu");
                College_Home();
                return;
            }

            foreach (var college in colleges)
            {
                Console.WriteLine($"\nID: {college.College_ID}");
                Console.WriteLine($"Name: {college.College_Name}");
                Console.WriteLine($"Affiliated Universities: {string.Join(", ", college.Affiliated_Universities.Select(u => u.Uni_Name))}");
                Console.WriteLine($"Classification: {college.Classification}");
                Console.WriteLine($"Success Percentage: {college.SuccessPercentage:F2}%\n");
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Colleges Menu");
            College_Home();
        }

        private static void SearchById()
        {
            Console.Write("Enter College ID: ");
            int id = Utilities.IntInput();
            var college = DataManager.Colleges.FirstOrDefault(c => c.College_ID == id);

            if (college == null)
            {
                Console.WriteLine("\nCollege not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Colleges Menu");
                College_Home();
                return;
            }

            Console.WriteLine($"\nID: {college.College_ID}");
            Console.WriteLine($"Name: {college.College_Name}");
            Console.WriteLine($"Affiliated Universities: {string.Join(", ", college.Affiliated_Universities.Select(u => u.Uni_Name))}");
            Console.WriteLine($"Classification: {college.Classification}");
            Console.WriteLine($"Success Percentage: {college.SuccessPercentage:F2}%\n");


            Console.ReadLine();
            Utilities.DisplayHeader("Colleges Menu");
            College_Home();
        }

        private static void SearchByName()
        {
            Console.Write("Enter College Name: ");
            string name = Console.ReadLine();
            var colleges = DataManager.Colleges.Where(c => c.College_Name.Contains(name)).ToList();

            if (colleges.Count == 0)
            {
                Console.WriteLine("\nNo colleges found with that name.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Colleges Menu");
                College_Home();
                return;
            }

            foreach (var college in colleges)
            {
                Console.WriteLine($"\nID: {college.College_ID}");
                Console.WriteLine($"Name: {college.College_Name}");
                Console.WriteLine($"Affiliated Universities: {string.Join(", ", college.Affiliated_Universities.Select(u => u.Uni_Name))}");
                Console.WriteLine($"Classification: {college.Classification}");
                Console.WriteLine($"Success Percentage: {college.SuccessPercentage:F2}%\n");
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Colleges Menu");
            College_Home();
        }

        private static void ShowEvaluation()
        {
            var colleges = DataManager.Colleges;
            if (colleges.Count == 0)
            {
                Console.WriteLine("\nNo colleges found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Colleges Menu");
                College_Home();
                return;
            }

            foreach (var college in colleges.OrderByDescending(c => c.SuccessPercentage))
            {
                var results = college.GetStudentResults();
                int total = results.passed + results.failed;
                Console.WriteLine($"\nCollege: {college.College_Name}");
                Console.WriteLine($"Classification: {college.Classification}");
                Console.WriteLine($"Passed: {results.passed} ({college.SuccessPercentage:F2}%)");
                Console.WriteLine($"Failed: {results.failed}");
                Console.WriteLine($"Total Students: {total}\n");
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Colleges Menu");
            College_Home();
        }

        public static void Add()
        {
            var college = new College
            {
                College_ID = DataManager.GetNextId(DataManager.Colleges)
            };

            Console.Write($"ID(Unique): {college.College_ID}\n");
            Console.Write($"Name: ");
            college.College_Name = Console.ReadLine();

            DataManager.Colleges.Add(college);
            DataManager.SaveData();
            Console.WriteLine("\nCollege added successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Colleges Menu");
            College_Home();
        }

        public static void Modify()
        {
            Console.Write("Enter College ID to modify: ");
            int id = Utilities.IntInput();
            var college = DataManager.Colleges.FirstOrDefault(c => c.College_ID == id);

            if (college == null)
            {
                Console.WriteLine("\nCollege not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Colleges Menu");

                Console.ReadLine();
                Utilities.DisplayHeader("Colleges Menu");
                College_Home();
                return;
            }

            Console.Write($"Current Name: {college.College_Name}\nNew Name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                college.College_Name = newName;
            }

            DataManager.SaveData();
            Console.WriteLine("\nCollege updated successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Colleges Menu");
            College_Home();
        }

        public static void Remove()
        {
            Console.Write("Enter College ID to remove: ");
            int id = Utilities.IntInput();
            var college = DataManager.Colleges.FirstOrDefault(c => c.College_ID == id);

            if (college == null)
            {
                Console.WriteLine("\nCollege not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Colleges Menu");
                College_Home();
                return;
            }

            // Check if college has departments
            if (college.Affiliated_Departments.Count > 0)
            {
                Console.WriteLine("\nCannot delete college with departments. Please delete departments first.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Colleges Menu");
                College_Home();
                return;
            }

            // Remove from universities
            foreach (var uni in college.Affiliated_Universities.ToList())
            {
                uni.Affiliated_Colleges.Remove(college);
            }

            DataManager.Colleges.Remove(college);
            DataManager.SaveData();
            Console.WriteLine("\nCollege removed successfully!\n");

            Console.ReadLine();
            Utilities.DisplayHeader("Colleges Menu");
            College_Home();
        }

        public static void AssignUniversity()
        {
            Console.Write("Enter College ID: ");
            int collegeId = Utilities.IntInput();
            var college = DataManager.Colleges.FirstOrDefault(c => c.College_ID == collegeId);

            if (college == null)
            {
                Console.WriteLine("\nCollege not found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Colleges Menu");
                College_Home();
                return;
            }

            Console.WriteLine("\n1- Assign University\n2- Unassign University");
            string choice = Utilities.Normal_Key();

            if (choice == "1")
            {
                Console.WriteLine("Available Universities:");
                foreach (var uni in DataManager.Universities)
                {
                    Console.WriteLine($"{uni.Uni_ID}: {uni.Uni_Name}");
                }

                Console.Write("Enter University ID to assign: ");
                int uniId = Utilities.IntInput();
                var university = DataManager.Universities.FirstOrDefault(u => u.Uni_ID == uniId);

                if (university == null)
                {
                    Console.WriteLine("University not found.");
                }
                else if (college.Affiliated_Universities.Contains(university))
                {
                    Console.WriteLine("\nUniversity is already assigned to this college.\n");
                }
                else
                {
                    college.Affiliated_Universities.Add(university);
                    university.Affiliated_Colleges.Add(college);
                    DataManager.SaveData();
                    Console.WriteLine("\nUniversity assigned successfully!\n");
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine("Assigned Universities:");
                foreach (var uni in college.Affiliated_Universities)
                {
                    Console.WriteLine($"{uni.Uni_ID}: {uni.Uni_Name}");
                }

                Console.Write("Enter University ID to unassign: ");
                int uniId = Utilities.IntInput();
                var university = college.Affiliated_Universities.FirstOrDefault(u => u.Uni_ID == uniId);

                if (university == null)
                {
                    Console.WriteLine("\nUniversity not assigned to this college.\n");
                }
                else
                {
                    college.Affiliated_Universities.Remove(university);
                    university.Affiliated_Colleges.Remove(college);
                    DataManager.SaveData();
                    Console.WriteLine("\nUniversity unassigned successfully!\n");
                }
            }


            Console.ReadLine();
            Utilities.DisplayHeader("Colleges Menu");
            College_Home();
        }
    }
}