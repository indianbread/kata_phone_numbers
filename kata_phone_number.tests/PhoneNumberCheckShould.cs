using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Xunit;
using kata_phone_number;

namespace kata_phone_number.tests
{
    public class PhoneNumberCheckShould
    {
        [Fact]
        public void ReturnAListContainingInconsistentNumbers()
        {
            var bob = new PhoneNumber("Bob", "91125426");
            var alice = new PhoneNumber("Alice", "97625992");
            var emergency = new PhoneNumber("Emergency", "911");
            
            var testPhoneList = new List<PhoneNumber>
            { bob, alice, emergency};
            
            var expectedResultList = new List<PhoneNumber>() {emergency, bob};

            var actual = PhoneNumberCheck.GetInconsistentNumbers(testPhoneList).ToList();
            
            Assert.Equal(2, actual.Count());
            Assert.Equal(expectedResultList.First(), actual.First());
            Assert.Equal(expectedResultList.Last(), actual.Last());
        }

        [Fact]
        public void ReturnAnEmptyListIfNoConsistentNumbers()
        {
            var fileName =
                @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/AppData/phone_data_5.txt";
            var testPhoneList2 = PhoneNumber.GetPhoneNumbers(fileName);
            
            Assert.Empty(PhoneNumberCheck.GetInconsistentNumbers(testPhoneList2));

        }

        [Fact]
        public void ReturnAPhoneNumberWhenUsingNameToSearch()
        {
            var fileName =
                @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/AppData/phone_data_5.txt";
            var testPhoneList = PhoneNumber.GetPhoneNumbers(fileName);
            var sut = new PhoneNumberCheck();
            var expectedResult = new List<PhoneNumber> {new PhoneNumber("Devon Osei", "010932357")};
            
            Assert.Equal(expectedResult, PhoneNumberCheck.FindByName("Devon Osei", testPhoneList));
            
        }

        [Fact]
        public void DisplayErrorMessageIfNoResultsFoundWhenSearchingByName()
        {
            var fileName =
                @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/AppData/phone_data_5.txt";
            var testPhoneList = PhoneNumber.GetPhoneNumbers(fileName);
            var sut = new PhoneNumberCheck();
            
            Assert.Throws<ArgumentException>(() => PhoneNumberCheck.FindByName("asddfg", testPhoneList));


        }
    }
    
    
}