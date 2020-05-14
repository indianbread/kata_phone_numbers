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
            var options = new Dictionary<int, Action>()
            {
                {1, CheckPhoneList},
                {2, FindByName}
            };

            DisplayOptions(options, 1);
            var input = GetSelection();
            try
            {
                var validatedSelection = GetValidatedSelection(options, input);
                options[validatedSelection].Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                input = GetSelection();
                GetValidatedSelection(options, input);
            }
            
        }

        private static string GetSelection()
        {
            Console.WriteLine("Select your options:");
            var input = Console.ReadLine();
            return input;
        }

        private static int GetValidatedSelection(Dictionary<int, Action> options, string selection)
        {
            var isNumber = Int32.TryParse(selection, out int option);
            if (!isNumber) throw new ArgumentException("Error: Not a valid selection ");
            if(!options.ContainsKey(option)) throw new ArgumentException("Error: Not a valid option");
            return option;
        }



        private static void DisplayOptions(Dictionary<int, Action> options, int optionsKey)
        {
            if (optionsKey > options.Count) return;
            Console.WriteLine($"{optionsKey}. {options[optionsKey].GetMethodInfo().Name}");
            optionsKey++;
            DisplayOptions(options, optionsKey);
        }

        private static void CheckPhoneList()
        {

            var results = PhoneNumberCheck.GetInconsistentNumbers(_phoneNumberList);
            Console.WriteLine($"Result for {_fileName}");
            if (!results.Any())
            {
                Console.WriteLine("This list is consistent");
                return;
            }

            Console.WriteLine("The following phone numbers are inconsistent:");
            DisplayResults(results.ToList());
        }

        private static void FindByName()
        {
            Console.WriteLine("Enter a name to search:");
            var nameToSearch = Console.ReadLine();
            var results = PhoneNumberCheck.FindByName(nameToSearch, _phoneNumberList);
            DisplayResults(results);
        }


        private static string DisplayResults(List<PhoneNumber> results)
        { 
            if (!results.Any()) return null;
            Console.WriteLine(results.First().ToString());
            results.RemoveAt(0);
            return DisplayResults(results);
        }

        private static readonly string _fileName = @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/AppData/phone_data_10000.txt";

        private static readonly IEnumerable<PhoneNumber> _phoneNumberList = PhoneNumber.GetPhoneNumbers(_fileName);
    }
}