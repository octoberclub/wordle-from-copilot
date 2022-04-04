using System;
using System.Collections.Generic;

namespace wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a new wordle
            Wordle wordle = new Wordle();            
            wordle.Play();
        }
    }

    public class Wordle
    {
        string [] words { get; set; }
        public Wordle()
        {
            // fetch list if five letter words
            words = System.IO.File.ReadAllLines("fiveletterwords.txt");            
        }

        public void Play()
        {
            // pick a random word
            Random rnd = new Random();
            int index = rnd.Next(0, words.Length);
            string wordToGuess = words[index];
            // Console.WriteLine("Guess the word: " + wordToGuess);

            List<string> guessedWords = new List<string>();

            int numOfTries = 0;
            while(numOfTries < 6)
            {
                // enter a five letter word            
                Console.WriteLine("Enter a five letter word. Attempt #" + (numOfTries + 1) + " of 6");
                string word = Console.ReadLine();

                //check if word is five characters
                if (word.Length != 5)
                {
                    Console.WriteLine("Word must be five characters");
                    continue;
                }

                guessedWords.Add(word);

                // checl if word is correct
                if (word == wordToGuess)
                {
                    break;
                }

                renderWordle(wordToGuess, guessedWords);

                // check each character in word matches
                numOfTries++;
            }

            if(numOfTries == 6)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("You lose! The word was {0}", wordToGuess);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("You win! The word was {0}", wordToGuess);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        private void renderWordle(string wordToGuess, List<string> guessedWords)
        {
            // render wordle
            Console.WriteLine("Wordle:");
            foreach(string word in guessedWords)
            {
                // check each character in word matches
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == wordToGuess[i])
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(word[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (wordToGuess.Contains(word[i]))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(word[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write(word[i]);
                    }                    
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
