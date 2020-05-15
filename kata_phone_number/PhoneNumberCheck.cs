using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace kata_phone_number
{
    public class PhoneNumberCheck
    {
        
        //opton 1
        public static bool IsListConsistent(IEnumerable<PhoneNumber> phoneNumbers)
        {
            return GetMatchingPrefixCount(phoneNumbers) == 0;
        }

        private static int GetMatchingPrefixCount(IEnumerable<PhoneNumber> phoneNumbersList)
        {
            int numberOfMatches = 0;
            var orderedPhoneNumbers = phoneNumbersList.OrderBy(number => number.Number.Length).ToList();
            
            if ( !orderedPhoneNumbers.Any()) return numberOfMatches;
            
            var firstNumber = orderedPhoneNumbers.First().Number;
            orderedPhoneNumbers.RemoveAt(0);
            var matchingNumbers = orderedPhoneNumbers.Where(phoneNumber => 
                phoneNumber.Number.Substring(0, firstNumber.Length).Contains(firstNumber)).ToList();
            
            if (matchingNumbers.Any()) return matchingNumbers.Count;
            
            return GetMatchingPrefixCount(orderedPhoneNumbers);
        }
        
        //Option 2 - Displaying a list of inconsistent numbers
        public static IEnumerable<PhoneNumber> GetInconsistentNumbers(IEnumerable<PhoneNumber> phoneNumbers)
        {
            var orderedPhoneNumbers = phoneNumbers.OrderBy(number => number.Number.Length).ToList();
            ICollection<PhoneNumber> inconsistentPhoneNumbers = new List<PhoneNumber>();
            return GetMatchingPhoneNumbers(orderedPhoneNumbers, inconsistentPhoneNumbers) ;
        }

        private static IEnumerable<PhoneNumber> GetMatchingPhoneNumbers(List<PhoneNumber> orderedPhoneNumbers, ICollection<PhoneNumber> inconsistentNumbers)
        {
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
            return GetMatchingPhoneNumbers(orderedPhoneNumbers, inconsistentNumbers);
        }
        
        //option 3

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