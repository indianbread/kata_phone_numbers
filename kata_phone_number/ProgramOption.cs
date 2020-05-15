using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace kata_phone_number
{
    public class ProgramOption
    {
        public static void CheckListConsistency()
        {
            var result = PhoneNumberCheck.IsListConsistent(_phoneNumberList)
                ? "Phone number list is consistent"
                : "Phone number list is not consistent";
            Console.WriteLine($"Result for {_fileName.Substring(120)}");
            Console.WriteLine(result);
        }
        public static void GetInconsistentPhoneNumbers()
        {

            var results = PhoneNumberCheck.GetInconsistentNumbers(_phoneNumberList).ToList();
            Console.WriteLine($"Result for ...{_fileName.Substring(120)}:\n");
            if (!results.Any())
            {
                Console.WriteLine("This list is consistent. No matching numbers found.");
                return;
            }

            Console.WriteLine("The following phone numbers are inconsistent:");
            DisplayResults(results);
        }

        public static void FindByName()
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