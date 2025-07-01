using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UMS
{
    public static class UMS_Manager
    {
        public static void Main_Home()
        {
            Utilities.DisplayHeader("Main Menu");

            Console.WriteLine("1- Universities\n" +
                             "2- Colleges\n" +
                             "3- Departments\n" +
                             "4- Subjects\n" +
                             "5- Students\n" +
                             "6- Evaluation Reports\n" +
                             "Esc- Exit\n");

            string Selective = Utilities.Normal_Key();
            switch (Selective)
            {
                case "1":
                    Utilities.DisplayHeader("Universities Menu");
                    Univ_Manager.Uni_Home();
                    break;

                case "2":
                    Utilities.DisplayHeader("Colleges Menu");
                    CollegeManager.College_Home();
                    break;

                case "3":
                    Utilities.DisplayHeader("Departments Menu");
                    DepartmentManager.Department_Home();
                    break;

                case "4":
                    Utilities.DisplayHeader("Subjects Menu");
                    SubjectManager.Subject_Home();
                    break;

                case "5":
                    Utilities.DisplayHeader("Students Menu");
                    StudentManager.Student_Home();
                    break;

                case "6":
                    Utilities.DisplayHeader("Evaluations Menu");
                    EvaluationManager.Evaluation_Home();
                    break;

                case "Esc":
                    ExitApplication();
                    break;
                default:
                    Console.WriteLine("Please, Enter a Valid Input");
                    Utilities.DisplayPressAnyKey();
                    break;
            }
        }

        private static void ExitApplication()
        {
            if (Utilities.ConfirmAction("Are you sure you want to exit?"))
            {
                try
                {
                    DataManager.SaveData();
                    Console.WriteLine("Data saved successfully. Goodbye!");
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving data: {ex.Message}");
                    Console.WriteLine("Exiting without saving...");
                    Environment.Exit(1);
                }
            }
        }
    }
}
