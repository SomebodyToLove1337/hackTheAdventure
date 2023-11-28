/*
 * hackTheAdventure
 * by Benedikt, 2023-11-28
 *  
 * This work is a derivative of 
 * "C# Adventure Game" by http://programmingisfun.com, used under CC BY.
 * https://creativecommons.org/licenses/by/4.0/
 */

using System;
using System.Security.Cryptography.X509Certificates;



namespace hackTheAdventure
{
    public static class Game
    {
        //Character Creation Variables
        static string characterName = "Cassius";

        //Print out Game Tiltle and overview
        public static void StartGame()
        {
            Console.WriteLine("The Lost City of Zorath!\n");
            Console.WriteLine("Welcome to The Enchanted Forest! You are about to embark on a magical journey through a mystical realm filled with wonder and danger. You will encounter many strange creatures and obstacles along the way, but with your wits and courage, you will overcome them all and emerge victorious.\r\n\r\nAs you explore the enchanted woods and winding rivers of this mysterious land, you will uncover ancient artifacts and long-forgotten secrets that will lead you ever closer to your goal. But beware: there are those who will stop at nothing to keep the secrets of the forest hidden from the world.\r\n\r\nAre you ready to take on the challenge? The fate of the realm rests in your hands. Good luck, adventurer!\r\n");
            CreateCharacter();

        }

        static void CreateCharacter()
        {
            
            Console.WriteLine("Please enter the Name of your Hero:");
            characterName = Console.ReadLine();
            Console.WriteLine("Hello " + characterName + "!");
            Console.WriteLine("Press any key to continue...");
            Console.Clear();
        }

        public static void Dialog(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ReadKey();
            Console.ResetColor();
        }
        public static void Dialog(string message, string color)
        {
            if (color == "red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (color == "green")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (color == "blue")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (color == "yellow")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (color == "magenta")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else if (color == "cyan")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (color == "white")
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (color == "black")
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(message);
            Console.ReadKey();
            Console.ResetColor();
        }


    }

    class Item
    {

    }
    class Program
    {
        static void Main()
        {
            Game.StartGame();
            Console.ReadKey();
            Game.Dialog("Have you seen a strange creature around here?\nAbout three feet high, greenish, with fluffy hair?\n");
            Game.Dialog("No, I haven't seen anything like that.", "red");

        }

    }


}