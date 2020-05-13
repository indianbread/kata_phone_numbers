using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace kata_phone_number
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = 
                @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/AppData/phone_data_10000.txt";

            var phoneNumberList = PhoneNumber.GetPhoneNumbers(fileName);
            var results = PhoneNumberCheck.GetInconsistentNumbers(phoneNumberList);
            Console.WriteLine($"Result for {fileName}");
            if (!results.Any())
            {
                Console.WriteLine("This list is consistent");
                return;
            }

            Console.WriteLine("The following phone numbers are inconsistent:");
            DisplayResults(results.ToList());
            
        }

        public static string DisplayResults(List<PhoneNumber> results)
        { 
            if (!results.Any()) return null;
            Console.WriteLine(results.First().ToString());
            results.RemoveAt(0);
            return DisplayResults(results);
        }
    }
}