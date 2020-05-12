using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
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
            
            var sut = new PhoneNumberCheck();
            
            Assert.False(sut.IsListConsistent(testPhoneList)); 
            Assert.True(sut.IsListConsistent(testPhoneList2));
        }

        [Fact]
        public void ReturnAPhoneNumberWhenUsingNameToSearch()
        {
            var fileName =
                @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/AppData/phone_data_5.txt";
            var testPhoneList = PhoneNumber.GetPhoneNumbers(fileName);
            var sut = new PhoneNumberCheck();
            
            Assert.Equal("010932357", sut.FindByName("Devon Osei", testPhoneList));
            
        }
    }
    
    
}