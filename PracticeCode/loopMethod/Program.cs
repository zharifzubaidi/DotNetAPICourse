using System;
using System.Net.NetworkInformation;

namespace OperatorConditional{
    internal class Program{
        
        static int accessibleInt = 7; // Variable is in class scope. This can be similar to global class in Cpp
        // Need to use static variable because main method is static.
        // Need to use first capital for variable outside method
        static void Main(string[] args) { // .Net require method to be static
            // Use CTRL + / to comment or uncomment
            
            // Scope
            // Lower scope such as method can access to class scope variable.
            Console.WriteLine(accessibleInt);
            // If we declare the same variable in the deeper level of scope, it will overwrite the name.
            // Local variable will overwrite global variable

            // // Loop
            int[] intsToCompress = new int[] {10, 15, 20, 25, 30, 12, 34};

            DateTime startTime = DateTime.Now;

            // // Static calculation
            // int totalValue1 = intsToCompress[0] + intsToCompress[1] + intsToCompress[2] + intsToCompress[3] + intsToCompress[4] + intsToCompress[5] + intsToCompress[6];
            // int totalValue2 = 0;
            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds); // Monitor duration
            // Console.Write("Total 1: ");
            // Console.WriteLine(totalValue1); //146

            // // For loop to get the total value in the array
            // // For loop will iterate each element in the array and sum it together
            // // Dynamic calculation
            // // For loop is faster than static calculation
            // startTime = DateTime.Now;
            // for (int i = 0; i < intsToCompress.Length; i++) {
            //     totalValue2 += intsToCompress[i];
            // }
            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds); // Monitor duration
            // Console.Write("Total 2: ");
            // Console.WriteLine(totalValue2);

            // // For each loop
            // // Foreach loop is faster than for loop
            // // More readable. Recommended.
            // startTime = DateTime.Now;
            // int totalValue3 = 0;
            // foreach(int intForCompression in intsToCompress) {
            //     totalValue3 += intForCompression;
            // }
            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds); // Monitor duration
            // Console.Write("Total 3: ");
            // Console.WriteLine(totalValue3);

            // // While loop
            // // Need to declare variable outside loop
            // // While loop faster than for loop
            // int index = 0;
            // int totalValue4 = 0;
            // startTime = DateTime.Now;
            // while(index < intsToCompress.Length){
            //     totalValue4 += intsToCompress[index];
            //     index++;
            // }
            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds); // Monitor duration
            // Console.Write("Total 4: ");
            // Console.WriteLine(totalValue4);           

            // // Do while loop
            // // Run first then check conditional
            // // Always run at least one time
            // // Slower than while loop
            // index = 0;
            // int totalValue5 = 0;
            // startTime = DateTime.Now;

            // do{
            //     totalValue5 += intsToCompress[index];
            //     index++;
            // } while(index < intsToCompress.Length); 

            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds); // Monitor duration
            // Console.Write("Total 5: ");
            // Console.WriteLine(totalValue5);

            // // Sum function for array
            // // Slower than all loops
            // startTime = DateTime.Now;
            // int totalValue6 = intsToCompress.Sum();
            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds); // Monitor duration
            // Console.Write("Total 6: ");
            // Console.WriteLine(totalValue6);

            // // If statement in for loop
            // int[] intsToCompress2 = new int[]{10, 15, 20, 25, 30, 12, 34};
            // int totalValueIf = 0;

            // // Check each element and only add value more that 20
            // foreach(int intToCompression2 in intsToCompress2) {
            //     if (intToCompression2 > 20){
            //         totalValueIf += intToCompression2;
            //     }
            // }
            // Console.WriteLine(totalValueIf);

            // Method
            startTime = DateTime.Now;
            int totalValue7 = Program.GetSum(intsToCompress);
            Console.WriteLine((DateTime.Now - startTime).TotalSeconds); // Monitor duration
            Console.Write("Total 7: ");
            Console.WriteLine(totalValue7);

        }
        // New method that can be called multiple times
        static private int GetSum(int[] intsToCompress){ //Need to be a static method to match the main method
            // int[] intsToCompress = new int[]{10, 15, 20, 25, 30, 12, 34};
            int totalValue = 0;
            foreach(int intForCompression in intsToCompress) {
                totalValue += intForCompression;
            }
            return totalValue;
        }
    }
}