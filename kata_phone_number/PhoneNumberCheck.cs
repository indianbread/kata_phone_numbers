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
                RemoveNumberFromPhoneNumbersList(matchingNumbers, orderedPhoneNumbers);

            }
            return GetMatchingPrefixCount(orderedPhoneNumbers, inconsistentNumbers);
        }
        
        public static List<PhoneNumber> FindByName(string name, IEnumerable<PhoneNumber> phoneNumbers)
        {
            var result = phoneNumbers.Where(phoneNumber => phoneNumber.Name.Contains(name)).ToList();
            if (!result.Any()) throw new ArgumentException("Error: No results found");
            return result;
        }
        
        private static ICollection<PhoneNumber> AddNumberToListOfInconsistentNumbers(ICollection<PhoneNumber> inconsistentNumbers, IEnumerable<PhoneNumber> matchingNumbers)
        {
            var allNumbers = matchingNumbers.ToList();
            if (!allNumbers.Any()) return inconsistentNumbers;
            inconsistentNumbers.Add(allNumbers.First());
            allNumbers.RemoveAt(0);
            return AddNumberToListOfInconsistentNumbers(inconsistentNumbers, allNumbers);
        }

        private static void RemoveNumberFromPhoneNumbersList(IEnumerable<PhoneNumber> matchingNumbers, List<PhoneNumber> phoneNumbers)
        {
            var allNumbers = matchingNumbers.ToList();
            if (!allNumbers.Any()) return;
            phoneNumbers.Remove(allNumbers.First());
            allNumbers.RemoveAt(0);
            RemoveNumberFromPhoneNumbersList(allNumbers, phoneNumbers);
        }
        
    }
}