using System;

// namespace is the project name
namespace MethodExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] intsToCompress = new int[] { 10, 15, 20, 25, 30, 12, 34 };
            int totalValue = 0;
            totalValue = GetSum(intsToCompress);
            Console.WriteLine("Total Value: " + totalValue);

            int[] intsToCompress2 = new int[] { 5, 10, 15, 20 };
            int totalValue2 = GetSum(intsToCompress2);
            Console.WriteLine("Total Value 2: " + totalValue2);
        }

        private static int GetSum(int[] intsToCompress )
        {
            //int[] intsToCompress = new int[] { 10, 15,20,25,30,12,34};
            int totalValue = 0;

            foreach (int intForCompression in intsToCompress)
            {
                totalValue += intForCompression;
            }
            
            return totalValue;
        }
    }
}
