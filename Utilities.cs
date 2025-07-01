using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS
{
    public static class Utilities
    {
        /// <summary>
        /// Ckecks if the input is numeric ('Intiger') or not.
        /// </summary>
        public static bool IsInt(string str)
        {
            return int.TryParse(str, out _);  // Search for 'Output Parameter' and learn how to use it.
        }

        /// <summary>
        /// Forces the user to enter a 'number' as an input.
        /// </summary>
        /// <remarks>
        /// (Defencive Coding)
        /// </remarks>
        public static int IntInput()
        {
            string input = Console.ReadLine();

            while (!IsInt(input))
            {
                Console.WriteLine("Please, Enter a Valid Number");
                input = Console.ReadLine();
            }

            return int.Parse(input);
        }

        /// <summary>
        /// Forces the user to enter a 'Positive Number' as an input.
        /// </summary>
        /// <remarks>
        /// (Defencive Coding)
        /// </remarks>
        public static int IsPositive()
        {
            int input = IntInput();

            while (input <= 0)
            {
                Console.WriteLine("Please, Enter a Valid Number");
                input = IntInput();
            }

            return input;
        }

        /// <summary>
        /// Transform 'Esc' Key and 'Enter' Key to a string.
        /// </summary>
        /// <remarks>
        /// (Defencive Coding)
        /// </remarks>
        public static string Normal_Key()
        {
            var input = Console.ReadKey(intercept: false);

            if (input.Key == ConsoleKey.Escape)
            {
                return "Esc";
            }
            else if (input.Key == ConsoleKey.Enter)
            {
                return "";
            }
            else
            {
                string result = input.KeyChar.ToString();
                while (true)
                {
                    var nextKey = Console.ReadKey(intercept: false);
                    if (nextKey.Key == ConsoleKey.Enter)
                        break;

                    result += nextKey.KeyChar;
                }

                return result;
            }
        }

        public static void DisplayPressAnyKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static string GetNonEmptyInput(string prompt)
        {
            Console.Write(prompt);
            string input;
            while (string.IsNullOrWhiteSpace(input = Console.ReadLine()))
            {
                Console.WriteLine("Input cannot be empty. Please try again.");
                Console.Write(prompt);
            }
            return input;
        }

        public static bool ConfirmAction(string message)
        {
            Console.Write($"{message} (Y/N): ");
            string response = Console.ReadLine().ToUpper();
            return response == "Y";
        }

        public static void DisplayHeader(string title)
        {
            Console.Clear();
            Console.WriteLine(new string('=', title.Length + 4));
            Console.WriteLine($"  {title.ToUpper()}");
            Console.WriteLine(new string('=', title.Length + 4));
            Console.WriteLine();
        }









        //public static void Add()
        //{
        //    var uni = new University();
        //    foreach (var prop in uni.GetType().GetProperties())
        //    {
        //        Console.Write($"{prop.Name}: ");

        //        try
        //        {
        //            string input = Console.ReadLine();

        //            // Convert and set the value
        //            object value = Convert.ChangeType(input, prop.PropertyType);
        //            prop.SetValue(uni, value);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error setting {prop.Name}: {ex.Message}");
        //            // You might want to decrement the counter here to retry this property
        //            // or implement some recovery logic
        //        }
        //    }
    }
}
