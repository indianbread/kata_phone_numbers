using System;
using System.Collections.Generic;
using Xunit;
using kata_phone_number;

namespace kata_phone_number.tests
{
    public class PhoneNumberCheckShould
    {
        [Fact]
        public void DetermineIfAPhoneNumberIsAPrefixOfAnother()
        {
            var testPhoneList = new Dictionary<int, string>()
            {
                {91125426, "Bob"},
                {97625992, "Alice"},
                {911, "Emergency"}
            };
            
            var sut = new PhoneNumberCheck();
            
            Assert.False(sut.IsListConsistent(testPhoneList));
        }
    }
    
    //group phone numbers by digit count
    // check if phone numbers contain the same numbers as the phone numbers with a lower digit count
    
}