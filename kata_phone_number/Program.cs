using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace kata_phone_number
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayOptions(_options, 1);
            var input = GetSelection();
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                _options[input].Invoke();
                watch.Stop();
                var elapsedTime = watch.ElapsedMilliseconds;
                Console.WriteLine("\nTime taken to search phone list: " + elapsedTime + " milliseconds");
            }
            catch (ArgumentException e) 
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static int GetSelection()
        {
            Console.WriteLine("Select your options:");
            var input = Console.ReadLine();
            var isInputValid = Int32.TryParse(input, out int selection);
            if (!isInputValid)
            {
                Console.WriteLine("Error: Not a valid number");
                return GetSelection();
            }

            if (!_options.ContainsKey(selection))
            {
                Console.WriteLine("Error: Not a valid option");
                return GetSelection();
            }
            
            return selection;
        }
        
        private static void DisplayOptions(Dictionary<int, Action> options, int optionsStartKey)
        {
            if (optionsStartKey > options.Count) return;
            Console.WriteLine($"{optionsStartKey}. {options[optionsStartKey].GetMethodInfo().Name}");
            optionsStartKey++;
            DisplayOptions(options, optionsStartKey);
        }

        
        private static Dictionary<int, Action> _options = new Dictionary<int, Action>()
        {
            {1, ProgramOption.CheckPhoneList},
            {2, ProgramOption.FindByName}
        };
    }
}

//TODO: Generate a test data of 10x more than previous one to log time difference in execution
//TODO: print start & stop time to log execution time
//TODO: Ask max how to create a library to store standard I/O handing & validation files to be reused

