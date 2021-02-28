using System;
using System.Collections.Generic;
using System.Linq;

namespace MastermindGame
{
    // Full disclosure I used https://www.codeproject.com/Tips/787502/Csharp-Random-Number-Guessing-Game
    // to help with creating the arrays
    partial class Program
    {
        public const int numTries = 10;

        static void Main()
        {
            Program game = new Program();
            game.Run();
        }

        void Run()
        {
            Console.WriteLine("A random number has been created.  You have ten attempts to solve.");
            Console.WriteLine(string.Format("\nYou have {0} attempts to win the game.", numTries));

            // Create answer
            string getAnswer = CreateAnswer;
            //commented out was used to test
            //Console.WriteLine(getAnswer);
            for (int i = 1; i <= numTries; i++)
            {
                // Get player's guess
                string userGuess = GetAttempt(i);

                List<Result> result = GetResult(getAnswer, userGuess);

                int flagCount = result.Where(f => f.Flag == true).Count();

                string finalString = string.Join("", result
                    .Select(c => (c.Answer).ToString()));

                if (flagCount == 4)
                {
                    Console.WriteLine("Random Number:{0} , Your Input:{1}", getAnswer, userGuess);
                    Console.WriteLine("Got it! Congratulations.");
                    break;
                }
                else if (i == numTries)
                {
                    Console.WriteLine("No attempts left.");
                    Console.WriteLine("The answer was {0}", getAnswer);
                }
                else
                {
                    Console.WriteLine(string.Format(finalString));
                }
            }

            Console.ReadLine();
        }


        public string CreateAnswer
        {
            get
            {
                Random random = new Random();
                string number = string.Empty;
                int length = 4;
                for (int i = 0; i < length; i++)
                {
                    number += random.Next(1, 7);
                }

                return number;
            }
        }

        public string GetAttempt(int attempt)
        {
            int inputNumber;

            Console.WriteLine(string.Format("\nGuess the number. Attempt:{0}", attempt));
            Console.WriteLine("Enter a 4 digit number");

            if (int.TryParse(Console.ReadLine(), out inputNumber)
                && inputNumber.ToString().Length == 4)
            {
                return inputNumber.ToString();
            }
            else
            {
                Console.WriteLine("You have entered a invalid input. 4 1's have been entered for you");
                return "1111";
            }
        }
        public List<Result> GetResult(string randomNumber, string userInput)
        {
            char[] splitRandomNumber = randomNumber.ToCharArray();
            char[] splitUserInput = userInput.ToCharArray();

            List<Result> results = new List<Result>();

            for (int index = 0; index < randomNumber.Length; index++)
            {
                Result result = new Result();
                var isPlusMinus = false;
                if (splitRandomNumber[index] == splitUserInput[index])
                {
                    result.Index = index;
                    result.Flag = splitRandomNumber[index] == splitUserInput[index];
                    result.Answer = "+";
                    results.Add(result);
                    isPlusMinus = true;
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (splitRandomNumber[index] == splitUserInput[j] && index != j)
                        {
                            result.Index = index;
                            result.Flag = false;
                            result.Answer = "-";
                            results.Add(result);
                            isPlusMinus = true;
                            break;
                        }
                    }
                }
                if (!isPlusMinus)
                {
                    result.Index = index;
                    result.Answer = " ";
                    results.Add(result);
                }
            }

            return results;
        }


    }
}
