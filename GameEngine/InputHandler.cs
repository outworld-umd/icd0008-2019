using System;

namespace GameEngine {
    
    public class InputHandler {

        private const string InputIsNotDigit = "IDI NAHHUI EBANII UROD!";
        
        public static int? GetUserIntInput(string prompt, int min, int max, string outOfBoundsMessage, bool canStop) {
            while (true) {
                Console.WriteLine(prompt);
                Console.Write(">");
                var choice = Console.ReadLine();
                if (int.TryParse(choice, out var input)) {
                    if (input < min || input > max) Console.WriteLine(outOfBoundsMessage);
                    else return input;
                }
                else if (choice?.ToLower() == "x" && canStop) {
                    return null;
                }
                else {
                    Console.WriteLine(InputIsNotDigit);
                }
            }
        }
        
        public static string GetUserStringInput(string prompt, int min, int max, string outOfBoundsMessage, bool canStop) {
            while (true) {
                Console.WriteLine(prompt);
                Console.Write(">");
                var choice = Console.ReadLine();
                if (choice != null && (choice.Length < min || choice.Length > max)) Console.WriteLine(outOfBoundsMessage);
                else if (choice?.ToLower() == "x" && canStop) {
                    return null;
                }
                else return choice;
            }
        }
    }
}