using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace kata_phone_number
{
    public class PhoneNumber
    {
        public PhoneNumber(string name, string number)
        {
            Name = name;
            Number = number;
        }

        public string Name;
        public string Number;
        
        public static IEnumerable<PhoneNumber> GetPhoneNumbers(string fileName)
        {
            string[] fileData = File.ReadAllLines(fileName);
            //remove name and number heading from array
            fileData = fileData.Skip(1).ToArray();
            var contactDetails = fileData.Select(line => line.Split(","));
            var phoneNumbers = contactDetails.Select(detail => 
                new PhoneNumber(detail[0], RemoveDelimitersFromNumber(detail[1])))
                .ToImmutableArray();
            return phoneNumbers;
        }

        private static string RemoveDelimitersFromNumber(string rawPhoneNumber)
        {
            var delimiters = new string[] {"-", " "};
            var separatedNumberPortions = rawPhoneNumber.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            var numbersOnly = String.Join("", separatedNumberPortions);
            return numbersOnly;
        }
        
        
    }
}