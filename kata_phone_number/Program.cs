using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace kata_phone_number
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayOptions(_options, 1);
            var selection = GetValidSelection();
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                _options[selection].Invoke();
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

        private static int GetValidSelection()
        {
            Console.WriteLine("Select your option:");
            var input = Console.ReadLine();
            var isInputValid = Int32.TryParse(input, out int selection);
            if (!isInputValid || !_options.ContainsKey(selection))
            {
                Console.WriteLine("Error: Not a valid selection");
                return GetValidSelection();
            }
            return selection;
        }
        
        private static void DisplayOptions(Dictionary<int, Action> options, int optionsStartKey)
        {
            if (optionsStartKey > options.Count) return;
            var methodName = options[optionsStartKey].GetMethodInfo().Name;
            Console.WriteLine($"{optionsStartKey}. {FormatMethodName(methodName)}");
            optionsStartKey++;
            DisplayOptions(options, optionsStartKey);
        }

        private static string FormatMethodName(string option)
        {
            var optionWords = Regex.Split(option, @"(?<!^)(?=[A-Z])");
            return String.Join(" ", optionWords);
        }

        
        private static Dictionary<int, Action> _options = new Dictionary<int, Action>()
        {
            {1, ProgramOption.CheckListConsistency},
            {2, ProgramOption.GetInconsistentPhoneNumbers},
            {3, ProgramOption.FindByName}
        };
        
    }
}



