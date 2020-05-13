using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace kata_phone_number
{
    public class PhoneNumberCheck
    {
        public static IEnumerable<PhoneNumber> GetInconsistentNumbers(IEnumerable<PhoneNumber> phoneNumbers)
        {
            ICollection<PhoneNumber> inconsistentPhoneNumbers = new List<PhoneNumber>();
            return GetMatchingPrefixCount(phoneNumbers, inconsistentPhoneNumbers) ;
        }

        private static IEnumerable<PhoneNumber> GetMatchingPrefixCount(IEnumerable<PhoneNumber> phoneNumbersList, ICollection<PhoneNumber> inconsistentNumbers)
        {
            var orderedPhoneNumbers = phoneNumbersList.OrderBy(number => number.Number.Length).ToList();

            if (!orderedPhoneNumbers.Any()) return inconsistentNumbers;
            
            var firstNumber = orderedPhoneNumbers.First();
            orderedPhoneNumbers.RemoveAt(0);
            var matchingNumbers = orderedPhoneNumbers.Where(phoneNumber => 
                phoneNumber.Number.Substring(0, firstNumber.Number.Length).Contains(firstNumber.Number)).ToList();
            if (matchingNumbers.Any())
            {
                inconsistentNumbers.Add(firstNumber);
                AddNumberToListOfInconsistentNumbers(inconsistentNumbers, matchingNumbers);

            }
            return GetMatchingPrefixCount(orderedPhoneNumbers, inconsistentNumbers);
        }

        private static ICollection<PhoneNumber> AddNumberToListOfInconsistentNumbers(ICollection<PhoneNumber> inconsistentNumbers, IEnumerable<PhoneNumber> matchingNumbers)
        {
            var allNumbers = matchingNumbers.ToList();
            if (!allNumbers.Any()) return inconsistentNumbers;
            inconsistentNumbers.Add(allNumbers.First());
            allNumbers.RemoveAt(0);
            return AddNumberToListOfInconsistentNumbers(inconsistentNumbers, allNumbers);
        }
        public static IEnumerable<string> FindByName(string name, IEnumerable<PhoneNumber> phoneNumbers)
        {
            var result = phoneNumbers.Where(phoneNumber => phoneNumber.Name.Contains(name)).ToList();
            if (!result.Any()) throw new ArgumentException("No results found");
            return result.Select(number => number.ToString());
        }
        
    }
}