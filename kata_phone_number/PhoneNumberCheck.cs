using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace kata_phone_number
{
    public class PhoneNumberCheck
    {
        public bool IsListConsistent(IEnumerable<PhoneNumber> phoneNumbers)
        {
            var initialNumberOfMatches = 0;
            return GetMatchingPrefixCount(phoneNumbers, initialNumberOfMatches) == 0;

        }

        private int GetMatchingPrefixCount(IEnumerable<PhoneNumber> phoneNumbersList,
            int initialMatches)
        {
            var orderedPhoneNumbers = phoneNumbersList.OrderBy(number => number.Number.Length).ToList();
            if ( !orderedPhoneNumbers.Any())
            {
                return initialMatches;
            }
            var firstNumber = orderedPhoneNumbers.First().Number;
            orderedPhoneNumbers.RemoveAt(0);
            var matchingNumbers = orderedPhoneNumbers.Where(number => 
                number.Number.Substring(0, firstNumber.Length).Contains(firstNumber)).ToList();
            int numberOfMatches = initialMatches;
            if (matchingNumbers.Any())
            {
                numberOfMatches += matchingNumbers.Count;
                return numberOfMatches;
            }
            return GetMatchingPrefixCount(orderedPhoneNumbers, numberOfMatches);
        }
        
        public string FindByName(string name, IEnumerable<PhoneNumber> phoneNumbers)
        {
            var names = name.Split(" ");
            var result = phoneNumbers.Where(phoneNumber => phoneNumber.Name == name);
            if (!result.Any())
            {
                return "No results available";
            }
            return result.First().Number;
            
        }
    }
}