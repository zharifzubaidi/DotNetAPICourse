using System;

namespace OperatorConditional{
    internal class Program{
        static void Main(string[] args) {
            // Use CTRL + / to comment or uncomment
            // //Operators
            // // Addition
            // int myInt = 5;
            // int mySecondInt = 10;
            // int total = myInt + mySecondInt;
            // Console.WriteLine(total);
            
            // // Increment
            // myInt++;
            // Console.WriteLine(myInt);
            // mySecondInt+=12;
            // Console.WriteLine(mySecondInt);
            // mySecondInt-=12;
            // Console.WriteLine(mySecondInt);
            
            // // Multiple
            // Console.WriteLine(myInt * mySecondInt);
            
            // // Division
            // Console.WriteLine(mySecondInt / myInt);
            
            // // Using Math library for exponent and square root operation
            // Console.WriteLine(Math.Pow(5,4));
            // Console.WriteLine(Math.Sqrt(25));

            // // String manipulation
            // string myString = "test";
            // Console.WriteLine(myString);
            // // 1st way of string append
            // myString += ". second part."; //This operation can append string together
            // Console.WriteLine(myString);
            // // 2nd way of string append
            // myString = myString + " third part.";
            // Console.WriteLine(myString);
            // // Split
            // string[] myStringArr = myString.Split(". "); // Split dot (.)
            // Console.WriteLine(myStringArr[0]);
            // Console.WriteLine(myStringArr[1]);
            // Console.WriteLine(myStringArr[2]);
            // int myThirdInt = 5;
            // int myFourthInt = 10;
            
            // // Equal
            // // 1st way
            // Console.WriteLine(myThirdInt.Equals(myFourthInt));
            // Console.WriteLine(myThirdInt.Equals(myFourthInt/2));
            // // 2nd way
            // Console.WriteLine(myThirdInt == myFourthInt);
            // Console.WriteLine(myThirdInt == myFourthInt/2);
            // // Not equal
            // Console.WriteLine(myThirdInt != myFourthInt);
            // // Greater, lesser
            // Console.WriteLine(myThirdInt >= myFourthInt);
            // Console.WriteLine(myThirdInt > myFourthInt/2);
            // Console.WriteLine(myThirdInt <= myFourthInt);
            // Console.WriteLine(myThirdInt < myFourthInt/2);
            // Comparison. AND & OR
            // Console.WriteLine(5 < 10 || 5 > 10);
            // Console.WriteLine(5 < 10 && 5 > 10);

            // Conditional statement
            // IF statement
            // int myFifthInt = 5;
            // int mySixthInt = 10;

            // if (myFifthInt < mySixthInt){
            //     myFifthInt += 10;
            // }
            //Console.WriteLine(myFifthInt);

            // string myCow = "cow";
            // string myCapCow = "COWs";
            // string myCapCowCnvrt = myCapCow.ToLower();
            // if (myCow == myCapCow){
            //     Console.WriteLine("Equal");
            // }
            // if (myCow != myCapCow){
            //     Console.WriteLine("Not equal");
            // }
            // if (myCow == myCapCow){
            //     Console.WriteLine("Same");
            // }
            // else if(myCow == myCapCowCnvrt){
            //     Console.WriteLine("Same without case sensitivity");
            // }
            // else{
            //     Console.WriteLine("Not the same");
            // }

            // Switch statement
            // Can compare variable to a constant value only
            string myCat = "cat";

            switch(myCat){
                case "cat":
                    Console.WriteLine("Lowercase");
                    break;
                case "Cat":
                    Console.WriteLine("Capitalised");
                    break;
                default:
                    Console.WriteLine("Default ran");
                    break;
            }

        }
    }
}
