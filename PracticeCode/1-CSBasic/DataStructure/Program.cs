using System;

// namespace is the project name
namespace DataStructure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Array - a collection of items that are of the same type
            string[] myGroceryArray = new string[2];            // array size is inmutable = cannot be change

            myGroceryArray[0] = "Cheese";
            myGroceryArray[1] = "Milk";

            myGroceryArray[1] = "Yogurt";

            Console.WriteLine(myGroceryArray[0]);
            Console.WriteLine(myGroceryArray[1]);

            string[] myGroceryArray1 = ["Cheese", "Milk"];

            Console.WriteLine(myGroceryArray1[0]);
            Console.WriteLine(myGroceryArray1[1]);

            // List - a collection of items that can grow and shrink in size
            List<string> myGroceryList = new List<string>();            // list size ismutable = can be change
            myGroceryList.Add("Ice cream");
            myGroceryList.Add("Crackers");
            Console.WriteLine(myGroceryList[0]);
            Console.WriteLine(myGroceryList[1]);

            List<string> myGroceryList1 = ["Ice Cream", "Crackers"];
            Console.WriteLine(myGroceryArray1[0]);
            Console.WriteLine(myGroceryArray1[1]);

            IEnumerable<string> myGroceryEnumerable = myGroceryList;    // Convert List to IEnumerable
            List<string> myGroceryList2 = myGroceryEnumerable.ToList(); // Convert to List if you need to modify

            // Multidimensional array
            // 2D Array
            int[,] myMultidimensionalArray = {
                {1, 2}, // First row
                {3, 4}, // Second row
                {5, 6}, // Third row
                {7, 8}  // Fourth row
            };
            Console.WriteLine(myMultidimensionalArray[0, 0]); // Output: 1
            Console.WriteLine(myMultidimensionalArray[1, 1]); // Output: 4
            Console.WriteLine(myMultidimensionalArray[2, 0]); // Output: 5
            Console.WriteLine(myMultidimensionalArray[3, 1]); // Output: 8

            // 3D Array
            int[,,] my3DArray = new int[2, 2, 2]
            {
                { // 0
                    {1, 2}, // 0
                    {3, 4}  // 1
                },
                { // 1
                    {5, 6}, // 0
                    {7, 8}  // 1
                }
            };

            Console.WriteLine(my3DArray[0, 0, 0]); // Output: 1
            Console.WriteLine(my3DArray[1, 1, 1]); // Output: 8
            Console.WriteLine(my3DArray[1, 0, 1]); // Output: 6

            // Dictionary - a set of key-value pairs
            // Key can be of any type, but value can be of any type
            Dictionary<string, int> groceryPrices = new Dictionary<string, int>();
            groceryPrices["Cheese"] = 5;
            groceryPrices["Milk"] = 3;
            groceryPrices["Yogurt"] = 4;

            // Check price based on the key which is the item name or key
            Console.WriteLine($"Price of Cheese: {groceryPrices["Cheese"]}");
            Console.WriteLine($"Price of Milk: {groceryPrices["Milk"]}");
            Console.WriteLine($"Price of Yogurt: {groceryPrices["Yogurt"]}");

             

        }
    }
}
