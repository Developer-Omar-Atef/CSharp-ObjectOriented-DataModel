using System;
using System.Collections.Generic;
using System.Linq;

namespace UMS
{
    public static class Univ_Manager
    {
        public static void Uni_Home()
        {
            Console.WriteLine("\n1- Show Existed Universities\n" +
                                  "2- Add New University\n" +
                                  "3- Modify a University Data\n" +
                                  "4- Remove a University\n" +
                                  "5- Assign/Unassign College\n" +
                                  "6- Show Evaluation Results\n" +
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

                    case "6":
                        Console.WriteLine("----------------");
                        ShowEvaluation();
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
            Console.WriteLine("\nView Universities");
            Console.WriteLine("----------------");
            Console.WriteLine("\n1- Show All Universities Data\n" +
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
                        Utilities.DisplayHeader("Universities Menu");
                        Uni_Home();
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
            var universities = DataManager.Universities;
            if (universities.Count == 0)
            {
                Console.WriteLine("\nNo universities found.\n");


                Console.ReadLine();

                Utilities.DisplayHeader("Universities Menu");
                Uni_Home();
                return;
            }

            Console.WriteLine("\nAll Universities:");
            Console.WriteLine("-----------------");
            foreach (var uni in universities.OrderBy(u => u.Uni_ID))
            {
                Console.WriteLine($"\nID: {uni.Uni_ID}");
                Console.WriteLine($"Name: {uni.Uni_Name}");
                Console.WriteLine($"Location: {uni.Uni_Location}");
                Console.WriteLine($"Colleges: {uni.Affiliated_Colleges.Count}");
                Console.WriteLine($"Classification: {uni.Classification}");
                Console.WriteLine($"Success Percentage: {uni.SuccessPercentage:F2}%\n");
            }

            Console.ReadLine();

            Utilities.DisplayHeader("Universities Menu");
            Uni_Home();
        }

        private static void SearchById()
        {
            Console.Write("\nEnter University ID: ");
            int id = Utilities.IntInput();
            var uni = DataManager.Universities.FirstOrDefault(u => u.Uni_ID == id);

            if (uni == null)
            {
                Console.WriteLine("\nUniversity not found.\n");


                Console.ReadLine();

                Utilities.DisplayHeader("Universities Menu");
                Uni_Home();
                return;
            }

            Console.WriteLine("\nUniversity Details:");
            Console.WriteLine("-------------------");
            Console.WriteLine($"ID: {uni.Uni_ID}");
            Console.WriteLine($"Name: {uni.Uni_Name}");
            Console.WriteLine($"Location: {uni.Uni_Location}");
            Console.WriteLine($"Classification: {uni.Classification}");
            Console.WriteLine($"Success Percentage: {uni.SuccessPercentage:F2}%\n");

            var results = uni.GetStudentResults();
            Console.WriteLine($"\nStudent Results:");
            Console.WriteLine($"Passed: {results.passed}");
            Console.WriteLine($"Failed: {results.failed}");
            Console.WriteLine($"Total: {results.passed + results.failed}");

            Console.WriteLine("\nAffiliated Colleges:");
            if (uni.Affiliated_Colleges.Count == 0)
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (var college in uni.Affiliated_Colleges.OrderBy(c => c.College_Name))
                {
                    Console.WriteLine($"- {college.College_Name} (ID: {college.College_ID})\n");
                }
            }


            Console.ReadLine();

            Utilities.DisplayHeader("Universities Menu");
            Uni_Home();
        }

        private static void SearchByName()
        {
            Console.Write("\nEnter University Name (or part of name): ");
            string name = Console.ReadLine();
            var universities = DataManager.Universities
                .Where(u => u.Uni_Name.ToLower().Contains(name.ToLower()))
                .ToList();

            if (universities.Count == 0)
            {
                Console.WriteLine("\nNo universities found with that name.\n");


                Console.ReadLine();

                Utilities.DisplayHeader("Universities Menu");
                Uni_Home();
                return;
            }

            Console.WriteLine("\nSearch Results:");
            Console.WriteLine("--------------");
            foreach (var uni in universities)
            {
                Console.WriteLine($"\nID: {uni.Uni_ID}");
                Console.WriteLine($"Name: {uni.Uni_Name}");
                Console.WriteLine($"Location: {uni.Uni_Location}");
                Console.WriteLine($"Colleges: {uni.Affiliated_Colleges.Count}");
                Console.WriteLine($"Classification: {uni.Classification}"); 
                Console.WriteLine($"Success Percentage: {uni.SuccessPercentage:F2}%\n");

                var results = uni.GetStudentResults();
                Console.WriteLine($"\nStudent Results:");
                Console.WriteLine($"Passed: {results.passed}");
                Console.WriteLine($"Failed: {results.failed}");
                Console.WriteLine($"Total: {results.passed + results.failed}");

                Console.WriteLine("\nAffiliated Colleges:");
                if (uni.Affiliated_Colleges.Count == 0)
                {
                    Console.WriteLine("None");
                }
                else
                {
                    foreach (var college in uni.Affiliated_Colleges.OrderBy(c => c.College_Name))
                    {
                        Console.WriteLine($"- {college.College_Name} (ID: {college.College_ID})\n");
                    }
                }


                Console.ReadLine();

                Utilities.DisplayHeader("Universities Menu");
                Uni_Home();
            }
        }


        private static void ShowEvaluation()
        {
            var universities = DataManager.Universities.OrderByDescending(u => u.SuccessPercentage).ToList();
            if (universities.Count == 0)
            {
                Console.WriteLine("\nNo universities found.");


                Console.ReadLine();

                Utilities.DisplayHeader("Universities Menu");
                Uni_Home();
                return;
            }

            Console.WriteLine("\nUniversity Evaluation Report");
            Console.WriteLine("---------------------------");
            Console.WriteLine("(Ordered by Success Percentage)\n");

            foreach (var uni in universities)
            {
                var results = uni.GetStudentResults();
                int total = results.passed + results.failed;

                Console.WriteLine($"University: {uni.Uni_Name}");
                Console.WriteLine($"Classification: {uni.Classification}");
                Console.WriteLine($"Success Percentage: {uni.SuccessPercentage:F2}%");
                Console.WriteLine($"Passed Students: {results.passed}");
                Console.WriteLine($"Failed Students: {results.failed}");
                Console.WriteLine($"Total Students: {total}");

                Console.WriteLine("\nAffiliated Colleges Performance:");
                foreach (var college in uni.Affiliated_Colleges.OrderByDescending(c => c.SuccessPercentage))
                {
                    var collegeResults = college.GetStudentResults();
                    Console.WriteLine($"- {college.College_Name}: {college.SuccessPercentage:F2}% (Passed: {collegeResults.passed}, Failed: {collegeResults.failed})");
                }
                Console.WriteLine("----------------------------------\n");
            }


            Console.ReadLine();

            Utilities.DisplayHeader("Universities Menu");
            Uni_Home();
        }

        public static void Add()
        {
            var uni = new University
            {
                Uni_ID = DataManager.GetNextId(DataManager.Universities)
            };

            Console.WriteLine("\nAdd New University");
            Console.WriteLine("------------------");
            Console.WriteLine($"ID (auto-generated): {uni.Uni_ID}");

            Console.Write("Name: ");
            uni.Uni_Name = Console.ReadLine();

            Console.Write("Location: ");
            uni.Uni_Location = Console.ReadLine();

            DataManager.Universities.Add(uni);
            DataManager.SaveData();
            Console.WriteLine("\nUniversity added successfully!\n");


            Console.ReadLine();

            Utilities.DisplayHeader("Universities Menu");
            Uni_Home();
        }

        public static void Modify()
        {
            Console.Write("\nEnter University ID to modify: ");
            int id = Utilities.IntInput();
            var uni = DataManager.Universities.FirstOrDefault(u => u.Uni_ID == id);

            if (uni == null)
            {
                Console.WriteLine("\nUniversity not found.\n");


                Console.ReadLine();

                Utilities.DisplayHeader("Universities Menu");
                Uni_Home();
                return;
            }

            Console.WriteLine("\nModify University");
            Console.WriteLine("-----------------");
            Console.WriteLine($"Current Name: {uni.Uni_Name}");
            Console.Write("New Name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                uni.Uni_Name = newName;
            }

            Console.WriteLine($"\nCurrent Location: {uni.Uni_Location}");
            Console.Write("New Location (leave blank to keep current): ");
            string newLocation = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newLocation))
            {
                uni.Uni_Location = newLocation;
            }

            DataManager.SaveData();
            Console.WriteLine("\nUniversity updated successfully!\n");
            Console.ReadLine();

            Utilities.DisplayHeader("Universities Menu");
            Uni_Home();
        }

        public static void Remove()
        {
            Console.Write("\nEnter University ID to remove: ");
            int id = Utilities.IntInput();
            var uni = DataManager.Universities.FirstOrDefault(u => u.Uni_ID == id);

            if (uni == null)
            {
                Console.WriteLine("University not found.");


                Console.ReadLine();

                Utilities.DisplayHeader("Universities Menu");
                Uni_Home();
                return;
            }

            // Check if university has colleges
            if (uni.Affiliated_Colleges.Count > 0)
            {
                Console.WriteLine("\nCannot delete university with affiliated colleges. Please remove or unassign colleges first.\n");
                Console.WriteLine("Affiliated Colleges:");
                foreach (var college in uni.Affiliated_Colleges)
                {
                    Console.WriteLine($"- {college.College_Name} (ID: {college.College_ID})\n");
                }

                Console.ReadLine();

                Utilities.DisplayHeader("Universities Menu");
                Uni_Home();
                return;
            }

            Console.WriteLine($"\nYou are about to delete: {uni.Uni_Name}");
            Console.Write("Are you sure? (Y/N): ");
            string confirmation = Console.ReadLine().ToUpper();

            if (confirmation == "Y")
            {
                DataManager.Universities.Remove(uni);
                DataManager.SaveData();
                Console.WriteLine("University removed successfully!\n");
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.\n");
            }

            Console.ReadLine();

            Utilities.DisplayHeader("Universities Menu");
            Uni_Home();
        }

        public static void AssignCollege()
        {
            Console.Write("\nEnter University ID: ");
            int uniId = Utilities.IntInput();
            var uni = DataManager.Universities.FirstOrDefault(u => u.Uni_ID == uniId);

            if (uni == null)
            {
                Console.WriteLine("\nUniversity not found.\n");

                Console.ReadLine();

                Utilities.DisplayHeader("Universities Menu");
                Uni_Home();
                return;
            }

            Console.WriteLine("\n1- Assign College\n2- Unassign College\n");
            string choice = Utilities.Normal_Key();

            if (choice == "1")
            {
                Console.WriteLine("\nAvailable Colleges:");
                var unassignedColleges = DataManager.Colleges.Where(c => !uni.Affiliated_Colleges.Contains(c)).ToList();

                if (unassignedColleges.Count == 0)
                {
                    Console.WriteLine("\nNo colleges available to assign.\n");
                    Console.ReadLine();

                    Utilities.DisplayHeader("Universities Menu");
                    Uni_Home();
                    return;
                }

                foreach (var col in unassignedColleges)
                {
                    Console.WriteLine($"{col.College_ID}: {col.College_Name}");
                }

                Console.Write("\nEnter College ID to assign (0 to cancel): ");
                int collegeId = Utilities.IntInput();
                if (collegeId == 0)
                {
                    Utilities.DisplayHeader("Universities Menu");
                    Uni_Home();
                    return;
                }

                var college = DataManager.Colleges.FirstOrDefault(c => c.College_ID == collegeId);
                if (college == null)
                {
                    Console.WriteLine("\nCollege not found.\n");
                }
                else if (uni.Affiliated_Colleges.Contains(college))
                {
                    Console.WriteLine("\nCollege is already assigned to this university.\n");
                }
                else
                {
                    uni.Affiliated_Colleges.Add(college);
                    college.Affiliated_Universities.Add(uni);
                    DataManager.SaveData();
                    Console.WriteLine("\nCollege assigned successfully!\n");
                }
            }
            else if (choice == "2")
            {
                if (uni.Affiliated_Colleges.Count == 0)
                {
                    Console.WriteLine("\nUniversity has no assigned colleges.\n");
                    Console.ReadLine();

                    Utilities.DisplayHeader("Universities Menu");
                    Uni_Home();
                    return;
                }

                Console.WriteLine("\nAssigned Colleges:");
                foreach (var col in uni.Affiliated_Colleges)
                {
                    Console.WriteLine($"{col.College_ID}: {col.College_Name}\n");
                }

                Console.Write("\nEnter College ID to unassign (0 to cancel): ");
                int collegeId = Utilities.IntInput();
                if (collegeId == 0)
                {
                    Utilities.DisplayHeader("Universities Menu");
                    Uni_Home();
                    return;
                }

                var college = uni.Affiliated_Colleges.FirstOrDefault(c => c.College_ID == collegeId);
                if (college == null)
                {
                    Console.WriteLine("\nCollege not assigned to this university.\n");
                }
                else
                {
                    // Check if college has departments
                    if (college.Affiliated_Departments.Count > 0)
                    {
                        Console.WriteLine("\nCannot unassign college with departments. Please remove departments first.\n");
                    }
                    else
                    {
                        uni.Affiliated_Colleges.Remove(college);
                        college.Affiliated_Universities.Remove(uni);
                        DataManager.SaveData();
                        Console.WriteLine("\nCollege unassigned successfully!\n");
                    }
                }
            }

            Console.ReadLine();

            Utilities.DisplayHeader("Universities Menu");
            Uni_Home();
        }
    }
}