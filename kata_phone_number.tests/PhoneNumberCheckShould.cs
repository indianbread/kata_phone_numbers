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
        public void DetermineIfAPhoneNumberIsAPrefixOfAnother()
        {
            var testPhoneList = new List<PhoneNumber>
            {
                new PhoneNumber("Bob", "91125426" ),
                new PhoneNumber("Alice", "97625992" ),
                new PhoneNumber("Emergency", "911" )

            };
            var fileName =
                @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/AppData/phone_data_5.txt";
            var testPhoneList2 = PhoneNumber.GetPhoneNumbers(fileName);
            
            Assert.False(PhoneNumberCheck.IsListConsistent(testPhoneList)); 
            Assert.True(PhoneNumberCheck.IsListConsistent(testPhoneList2));
        }

        [Fact]
        public void ReturnAPhoneNumberWhenUsingNameToSearch()
        {
            var fileName =
                @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/AppData/phone_data_5.txt";
            var testPhoneList = PhoneNumber.GetPhoneNumbers(fileName);
            var sut = new PhoneNumberCheck();
            var expectedResult = new List<string> {"Name: Devon Osei, Number: 010932357"};
            
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