using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace kata_phone_number
{
    public class PhoneNumberCheck
    {
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
        
        public static IEnumerable<string> FindByName(string name, IEnumerable<PhoneNumber> phoneNumbers)
        {
            var result = phoneNumbers.Where(phoneNumber => phoneNumber.Name.Contains(name)).ToList();
            if (!result.Any()) throw new ArgumentException("No results found");
            return result.Select(number => number.ToString());
        }
        
    }
}