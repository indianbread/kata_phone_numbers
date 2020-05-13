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
            var expectedLastEntry = new PhoneNumber("Yoshiko Dekany", "031492144");
            Assert.Equal(5, sut.Count());
            Assert.Equal(expectedFirstEntry.Name, sut.First().Name);
            Assert.Equal(expectedFirstEntry.Number, sut.First().Number);
            Assert.Equal(expectedLastEntry.Name, sut.Last().Name);
            Assert.Equal(expectedLastEntry.Number, sut.Last().Number);

        }

        
    }
}