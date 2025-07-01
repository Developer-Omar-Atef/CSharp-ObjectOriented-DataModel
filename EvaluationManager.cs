using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS
{
    public static class EvaluationManager
    {
        public static void Evaluation_Home()
        {
            Console.WriteLine("\n1- University Evaluation\n" +
                                  "2- College Evaluation\n" +
                                  "3- Department Evaluation\n" +
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
                        UniversityEvaluation();
                        cond = true;
                        break;

                    case "2":
                        Console.WriteLine("----------------");
                        CollegeEvaluation();
                        cond = true;
                        break;

                    case "3":
                        Console.WriteLine("----------------");
                        DepartmentEvaluation();
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

        private static void UniversityEvaluation()
        {
            var universities = DataManager.Universities.OrderByDescending(u => u.SuccessPercentage).ToList();
            if (universities.Count == 0)
            {
                Console.WriteLine("\nNo universities found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Evaluations Menu");
                Evaluation_Home();
                return;
            }

            Console.WriteLine("\nUniversity Evaluation Report (Ordered by Success Percentage)");
            Console.WriteLine("----------------------------------------------------------");
            foreach (var uni in universities)
            {
                var results = uni.GetStudentResults();
                int total = results.passed + results.failed;
                Console.WriteLine($"\nUniversity: {uni.Uni_Name}");
                Console.WriteLine($"Classification: {uni.Classification}");
                Console.WriteLine($"Passed: {results.passed} ({uni.SuccessPercentage:F2}%)");
                Console.WriteLine($"Failed: {results.failed}");
                Console.WriteLine($"Total Students: {total}");
                Console.WriteLine("Affiliated Colleges:");
                foreach (var college in uni.Affiliated_Colleges.OrderByDescending(c => c.SuccessPercentage))
                {
                    Console.WriteLine($"- {college.College_Name} ({college.Classification}, {college.SuccessPercentage:F2}%)\n");
                }
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Evaluations Menu");
            Evaluation_Home();
        }

        private static void CollegeEvaluation()
        {
            var colleges = DataManager.Colleges.OrderByDescending(c => c.SuccessPercentage).ToList();
            if (colleges.Count == 0)
            {
                Console.WriteLine("\nNo colleges found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Evaluations Menu");
                Evaluation_Home();
                return;
            }

            Console.WriteLine("\nCollege Evaluation Report (Ordered by Success Percentage)");
            Console.WriteLine("-------------------------------------------------------");
            foreach (var college in colleges)
            {
                var results = college.GetStudentResults();
                int total = results.passed + results.failed;
                Console.WriteLine($"\nCollege: {college.College_Name}");
                Console.WriteLine($"University: {string.Join(", ", college.Affiliated_Universities.Select(u => u.Uni_Name))}");
                Console.WriteLine($"Classification: {college.Classification}");
                Console.WriteLine($"Passed: {results.passed} ({college.SuccessPercentage:F2}%)");
                Console.WriteLine($"Failed: {results.failed}");
                Console.WriteLine($"Total Students: {total}");
                Console.WriteLine("Departments:");
                foreach (var dept in college.Affiliated_Departments.OrderByDescending(d => d.SuccessPercentage))
                {
                    Console.WriteLine($"- {dept.Department_Name} ({dept.SuccessPercentage:F2}%)\n");
                }
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Evaluations Menu");
            Evaluation_Home();
        }

        private static void DepartmentEvaluation()
        {
            var departments = DataManager.Departments.OrderByDescending(d => d.SuccessPercentage).ToList();
            if (departments.Count == 0)
            {
                Console.WriteLine("\nNo departments found.\n");

                Console.ReadLine();
                Utilities.DisplayHeader("Evaluations Menu");
                Evaluation_Home();
                return;
            }

            Console.WriteLine("\nDepartment Evaluation Report (Ordered by Success Percentage)");
            Console.WriteLine("----------------------------------------------------------");
            foreach (var dept in departments)
            {
                var results = dept.GetStudentResults();
                int total = results.passed + results.failed;
                Console.WriteLine($"\nDepartment: {dept.Department_Name}");
                Console.WriteLine($"College: {dept.Affiliated_College.College_Name}");
                Console.WriteLine($"University: {string.Join(", ", dept.Affiliated_College.Affiliated_Universities.Select(u => u.Uni_Name))}");
                Console.WriteLine($"Passed: {results.passed} ({dept.SuccessPercentage:F2}%)");
                Console.WriteLine($"Failed: {results.failed}");
                Console.WriteLine($"Total Students: {total}\n");
            }

            Console.ReadLine();
            Utilities.DisplayHeader("Evaluations Menu");
            Evaluation_Home();
        }
    }
}
