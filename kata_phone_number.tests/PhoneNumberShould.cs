using System;
using System.IO;
using System.Linq;
using Xunit;

namespace kata_phone_number.tests
{
    public class PhoneNumberShould
    {
        [Fact]
        public void ParseTextFileToPhoneNumberObjects()
        {
            var fileName =
                @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/AppData/phone_data_5.txt";

            var sut = PhoneNumber.GetPhoneNumbers(fileName);
            var expectedFirstEntry = new PhoneNumber("Kimberlee Turlington", "025164684873") ;
            
            Assert.Equal(5, sut.Count());
            Assert.Equal(expectedFirstEntry.Name, sut.First().Name);
            Assert.Equal(expectedFirstEntry.Number, sut.First().Number);

        }
        
        //functional programming - is it still acceptable to parse text data to objects
        // how can i parse each line to a phone number object without using a loop
    }
}