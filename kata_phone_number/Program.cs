using System;
using System.IO;

namespace kata_phone_number
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = 
                @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/AppData/phone_data_10000.txt";

            var phoneNumberList = PhoneNumber.GetPhoneNumbers(fileName);
            var result = PhoneNumberCheck.IsListConsistent(phoneNumberList)
                ? "Phone number list is consistent"
                : "Phone number list is not consistent";
            Console.WriteLine($"Result for {fileName}");
            Console.WriteLine(result);

        }
    }
}