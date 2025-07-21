
namespace AsyncExample
{
    // Async Example Program
    internal class Program
    {
        // We need to wrap our return type in a Task to enable asynchronous programming
        // Task encapsulates the logic that we want to run asynchronously
        // Wait to complete the task before proceeding
        static async Task Main(string[] args)   // Change from static void to static async Task method to enable asynchronous programming
        {
            // 1st Async Example
            Task firstTask = new Task(() =>     // Creating an instance of a Task that runs asynchronously
            {
                //Console.WriteLine("First Task is running");
                Thread.Sleep(100); // Sleep in milliseconds
                Console.WriteLine("Task 1 - 100ms");
            });
            firstTask.Start();   // Start the task

            // 2nd Async Example together with Sync Example
            Task secondTask = ConsoleAfterDelayAsync("Task 2 - 150ms", 150);

            //ConsoleAfterDelay("Delay", 101); // Synchronous method call
            ConsoleAfterDelay("Delay - 75ms", 75); // Synchronous method call

            Task thirdTask = ConsoleAfterDelayAsync("Task 3 - 125ms", 50);  // This starts after Task Sync 

            await firstTask; // Await the completion of the first task. Program flow blocks here until the task is complete.
            
            await secondTask; // Await the completion of the second task

            Console.WriteLine("After task creation");

            await thirdTask; // Await the completion of the third task
        }

        // Synchronous method
        static void ConsoleAfterDelay(string text, int delayTime)
        {
            Thread.Sleep(delayTime);        // Synchronously wait for the specified delay time
            Console.WriteLine(text);
        }

        // Asynchronous method
        static async Task ConsoleAfterDelayAsync(string text, int delayTime)
        {
            await Task.Delay(delayTime);    // Asynchronously wait for the specified delay time
            Console.WriteLine(text);
        }
    }
}