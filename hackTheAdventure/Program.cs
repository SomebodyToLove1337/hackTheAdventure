using System;



namespace hackTheAdventure
{
    class Program
    {
        static void Main()
        {
            //Declare Variables for Character Creation
            string characterName = "Cassius";

            Console.WriteLine("The Lost City of Zorath!\n");
            Console.WriteLine("Welcome to The Enchanted Forest! You are about to embark on a magical journey through a mystical realm filled with wonder and danger. You will encounter many strange creatures and obstacles along the way, but with your wits and courage, you will overcome them all and emerge victorious.\r\n\r\nAs you explore the enchanted woods and winding rivers of this mysterious land, you will uncover ancient artifacts and long-forgotten secrets that will lead you ever closer to your goal. But beware: there are those who will stop at nothing to keep the secrets of the forest hidden from the world.\r\n\r\nAre you ready to take on the challenge? The fate of the realm rests in your hands. Good luck, adventurer!\r\n");
            Console.Write("You can enter a Name for your character now: ");

            characterName = Console.ReadLine();

            Console.WriteLine("\nWelcome " + characterName + " to the Enchanted Forest!\r\n");
            
        }

    }
}