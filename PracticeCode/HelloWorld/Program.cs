// New template
// See https://aka.ms/new-console-template for more information
// WriteLine will include new line after display in powershell
//Console.WriteLine("Hello C# Programmer! Goodluck for your interview :)");
// Write will not include new line after display in powershell
//Console.Write("First");
//Console.Write("Second");

// Old template
using System;

namespace HelloWorld{
    internal class Program{
        static void Main(string[] args){
            // 1 byte is made up of 8 bits 00000000 - these bits can be used to store a number as follows
            // Each bit can be worth 0 or 1 of the value it is placed in
            // From the right we start with a value of 1 and double for each digit to the left
            // 00000000 = 0
            // 00000001 = 1
            // 00000010 = 2
            // 00000011 = 3
            // 00000100 = 4
            // 00000101 = 5
            // 00000110 = 6
            // 00000111 = 7
            // 00001000 = 8

            // Whole number
            // 1 byte (8 bit) unsigned, where signed means it can be negative
            byte myByte = 255;
            byte mySecondByte = 0;
 
            // 1 byte (8 bit) signed, where signed means it can be negative
            sbyte mySbyte = 127;
            sbyte mySecondSbyte = -128;
 
            // 2 byte (16 bit) unsigned, where signed means it can be negative
            ushort myUshort = 65535;
 
            // 2 byte (16 bit) signed, where signed means it can be negative
            short myShort = -32768;
 
            // 4 byte (32 bit) signed, where signed means it can be negative
            int myInt = 2147483647;
            int mySecondInt = -2147483648;
 
            // Decimal
            // 8 byte (64 bit) signed, where signed means it can be negative
            long myLong = -9223372036854775808;
 
 
            // 4 byte (32 bit) floating point number
            float myFloat = 0.751f;
            float mySecondFloat = 0.75f;
 
            // 8 byte (64 bit) floating point number
            double myDouble = 0.751;
            double mySecondDouble = 0.75d;
 
            // 16 byte (128 bit) floating point number
            decimal myDecimal = 0.751m;
            decimal mySecondDecimal = 0.75m;
 
            // Console.WriteLine(myFloat - mySecondFloat);
            // Console.WriteLine(myDouble - mySecondDouble);
            // Console.WriteLine(myDecimal - mySecondDecimal);
 
            // String
            string myString = "Hello World";
            // Console.WriteLine(myString);
            string myStringWithSymbols = "!@#$@^$%%^&(&%^*__)+%^@##$!@%123589071340698ughedfaoig137";
            // Console.WriteLine(myStringWithSymbols);

            // Bool
            bool myBool = true;

            // Array
            // Old declaration syntax
            string[] myGroceryArray = new string[2];
            myGroceryArray[0] = "Watermelon";
            myGroceryArray[1] = "Apple";
            // Console.WriteLine(myGroceryArray[0]);
            // Console.WriteLine(myGroceryArray[1]);
            // New declaration syntax
            string[] mySecondGroceryArray = {"Apples", "eggs"};
            // Console.WriteLine(mySecondGroceryArray[0]);
            // Console.WriteLine(mySecondGroceryArray[1]);

            // List
            // Declaration
            List<string> myGroceryList = new List<string>() {"Milk", "Cheese"}; // Dynamic size
            // Console.WriteLine(myGroceryList[0]);
            // Console.WriteLine(myGroceryList[1]);
            // Add new element into the list
            myGroceryList.Add("Oranges");
            // Console.WriteLine(myGroceryList[2]);

            // IEnumerable. Cannot add new element.
            IEnumerable<string> myGroceryIEnumerable = myGroceryList;
            // Console.WriteLine(myGroceryIEnumerable.First());

            // 2D Array
            string[,] myTwoDimensionalArray = new string[,] {
                {"Apples", "Eggs"},
                {"Milk", "Cheese"},
            };
            // Console.Write("2D Array 1,1: ");
            // Console.WriteLine(myTwoDimensionalArray[1,1]);

            // Dictionary. Key-value pair.
            Dictionary<string, string> myGroceryDictionary = new Dictionary<string, string>() {
                {"Cheese", "Dairy"}
            };
            Dictionary<string, string[]> myGroceryDictionaryArr = new Dictionary<string, string[]>() {
                {"Vegetable", new string[]{"Cucumber","Chilli","Broccoli"}}
            };

            Console.Write("Dictionary: ");
            Console.WriteLine(myGroceryDictionary["Cheese"]);
            Console.Write("DictionaryArr: ");
            Console.WriteLine(myGroceryDictionaryArr["Vegetable"][2]);
        }
    }
}