using System;

// namespace is the project name
namespace Loop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example 1
            int[] intsToCompress = new int[] { 10, 15, 20, 25, 30, 12, 34 };
            DateTime startTime = DateTime.Now;
            int totalValue = intsToCompress[0] + intsToCompress[1] + intsToCompress[2]
             + intsToCompress[3] + intsToCompress[4] + intsToCompress[5] + intsToCompress[6];
            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine("Total value of the array by accessing index is: " + totalValue);

            // Example 2
            int totalValue2 = 0;
            startTime = DateTime.Now;
            for (int i = 0; i < intsToCompress.Length; i++)     // Fast
            {
                totalValue2 += intsToCompress[i];
            }
            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine("Total value of the array by using loop is: " + totalValue2);

            // Example 3
            totalValue2 = 0;
            startTime = DateTime.Now;
            foreach (int intForCompression in intsToCompress)   // Preferable // Faster
            {
                totalValue2 += intForCompression; // Using foreach loop
            }
            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine("Total value of the array by using foreach loop is: " + totalValue2);

            // Example 4
            totalValue2 = 0;
            startTime = DateTime.Now;
            int index = 0;
            while (index < intsToCompress.Length)
            {
                totalValue2 += intsToCompress[index];
                index++;
            }
            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine("Total value of the array by using while loop is: " + totalValue2);

            // Example 5
            totalValue2 = 0;
            startTime = DateTime.Now;
            index = 0;
            do
            {
                totalValue2 += intsToCompress[index];
                index++;
            } while (index < intsToCompress.Length);
            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine("Total value of the array by using do-while loop is: " + totalValue2);

            // Example 6
            totalValue2 = 0;
            totalValue2 = intsToCompress.Sum(); // Using LINQ
            Console.WriteLine("Total value of the array by using LINQ is: " + totalValue2);

            // Example 7
            totalValue = 0;
            foreach (int intForCompression in intsToCompress)
            {
                if (intForCompression > 20) // Condition to filter values
                {
                    totalValue += intForCompression;
                }
            }
            Console.WriteLine("Total value of the array by using foreach loop and condition is: " + totalValue);

            
        }
    }
}
