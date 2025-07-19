using System;

// namespace is the project name
namespace OperatorsConditionals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Operators Example

            int myInt = 5;
            int mySecondInt = 10;
            Console.WriteLine("Initial value: " + myInt);

            myInt++;
            Console.WriteLine("Incremented value: " + myInt);

            myInt += mySecondInt;
            Console.WriteLine("After adding second int: " + myInt);

            myInt -= mySecondInt;
            Console.WriteLine("After subtracting second int: " + myInt);

            Console.WriteLine("Product of myInt and mySecondInt: " + ((myInt - 1) * mySecondInt));

            Console.WriteLine("Division of myInt by mySecondInt: " + (mySecondInt / (myInt - 1)));

            Console.WriteLine("Power: " + Math.Pow(5, 4));          // Power function to square

            Console.WriteLine("Square root: " + Math.Sqrt(25));     // Square root function

            string myString = "test";
            Console.WriteLine("String concatenation: " + myString + " string");
            Console.WriteLine("String interpolation: " + $"{myString} string");
            Console.WriteLine("String interpolation with variable: " + $"{myString} string with variable");
            Console.WriteLine("String interpolation with variable and number: " + $"{myString} string with variable and number {myInt}");

            myString += " concatenated";

            Console.WriteLine("String after concatenation: " + myString);
            Console.WriteLine("String length: " + myString.Length);
            Console.WriteLine("String in uppercase: " + myString.ToUpper());
            Console.WriteLine("String in lowercase: " + myString.ToLower());
            Console.WriteLine("String contains 'test': " + myString.Contains("test"));
            Console.WriteLine("String starts with 'test': " + myString.StartsWith("test"));
            Console.WriteLine("String ends with 'ed': " + myString.EndsWith("ed"));
            Console.WriteLine("String index of 'test': " + myString.IndexOf("test"));
            Console.WriteLine("String last index of 't': " + myString.LastIndexOf("t"));
            Console.WriteLine("String substring from index 5: " + myString.Substring(5));
            Console.WriteLine("String split by space: " + string.Join(", ", myString.Split(' ')));
            Console.WriteLine("String replace 'test' with 'example': " + myString.Replace("test", "example"));
            Console.WriteLine("String trim: " + myString.Trim());
            Console.WriteLine("String trim start: " + myString.TrimStart());
            Console.WriteLine("String trim end: " + myString.TrimEnd());
            Console.WriteLine("String is null or empty: " + string.IsNullOrEmpty(myString));
            Console.WriteLine("String is null or whitespace: " + string.IsNullOrWhiteSpace(myString));
            Console.WriteLine("String format: " + string.Format("Formatted string: {0}, {1}", myString, myInt));
            Console.WriteLine("String comparison: " + string.Compare(myString, "test string", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("String equals 'test string': " + myString.Equals("test string", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(myString);
            string[] stringArray = myString.Split(' ');
            Console.WriteLine("First element of string array: " + stringArray[0]);
            Console.WriteLine("Second element of string array: " + stringArray[1]);

            // Conditionals Example
            // If-else if-else statement
            myInt = 15;
            mySecondInt = 20;
            Console.WriteLine("Conditional statements example:");
            if (myInt > mySecondInt)
            {
                Console.WriteLine("myInt is greater than mySecondInt");
            }
            else if (myInt < mySecondInt)
            {
                Console.WriteLine("myInt is less than mySecondInt");
            }
            else
            {
                Console.WriteLine("myInt is equal to mySecondInt");
            }

            if (myInt % 2 == 0)
            {
                Console.WriteLine("myInt is even");
            }
            else
            {
                Console.WriteLine("myInt is odd");
            }

            string myCow = "cow";
            string myCapitalizedCow = "Cow";
            myCapitalizedCow = myCapitalizedCow.ToLower(); // Convert to lowercase for comparison

            if (myCow == myCapitalizedCow)
            {
                Console.WriteLine("myCow is equal to myCapitalizedCow");
            }
            else
            {
                Console.WriteLine("myCow is not equal to myCapitalizedCow");
            }

            // Switch statement
            Console.WriteLine("Switch statement example:");
            switch (myInt)
            {
                case 0:
                    Console.WriteLine("myInt is zero");
                    break;
                case 1:
                    Console.WriteLine("myInt is one");
                    break;
                case 2:
                    Console.WriteLine("myInt is two");
                    break;
                default:
                    Console.WriteLine("myInt is greater than two");
                    break;
            }



        }
    }
}
