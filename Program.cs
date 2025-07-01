using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UMS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "University Management System";

            // Load data from XML file if it exists
            try
            {
                DataManager.LoadData();
                DataManager.RelinkReferences();
                Console.WriteLine("System data loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                Console.WriteLine("Starting with empty database.");
            }

            // Display welcome message
            Console.WriteLine("====================================");
            Console.WriteLine("  UNIVERSITY MANAGEMENT SYSTEM");
            Console.WriteLine("====================================");
            Console.WriteLine("\nDeveloped by: Omar MOhamed Atef");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            // Main application loop
            while (true)
            {
                try
                {
                    Console.Clear();
                    UMS_Manager.Main_Home();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nAn error occurred: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                    // Attempt to save data before continuing
                    try
                    {
                        DataManager.SaveData();
                    }
                    catch
                    {
                        Console.WriteLine("Warning: Could not save data after error!");
                    }
                }
            }
        }
    }
}
