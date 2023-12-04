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
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;

namespace hackTheAdventure
{
    public static class Game
    {
        //Debugging Variables
        public static bool statusDev = true;
        //Character Creation Variables
        public static string characterName = "Cassius";
        public static string language = "en";

        //Print out Game Tiltle and overview
        public static void StartGame()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to The Enchanted Forest! You are about to embark on a magical journey through a mystical realm filled with wonder and danger. You will encounter many strange creatures and obstacles along the way, but with your wits and courage, you will overcome them all and emerge victorious.\r\n\r\nAs you explore the enchanted woods and winding rivers of this mysterious land, you will uncover ancient artifacts and long-forgotten secrets that will lead you ever closer to your goal. But beware: there are those who will stop at nothing to keep the secrets of the forest hidden from the world.\r\n\r\nAre you ready to take on the challenge? The fate of the realm rests in your hands. Good luck, adventurer!\r\n");
            Console.WriteLine("        ,     \\    /      ,        \r\n       / \\    )\\__/(     / \\       \r\n      /   \\  (_\\  /_)   /   \\      \r\n ____/_____\\__\\@  @/___/_____\\____ \r\n|             |\\../|              |\r\n|              \\VV/               |\r\n|     The Lost City of Zorath!    |\r\n|_________________________________|\r\n |    /\\ /      \\\\       \\ /\\    | \r\n |  /   V        ))       V   \\  | \r\n |/     `       //        '     \\| \r\n `              V                '");
            Console.ResetColor();


        }

        public static void CreateCharacter()
        {

            Console.WriteLine("Please enter the Name of your Hero:");
            characterName = Console.ReadLine();
            Console.WriteLine("Hello " + characterName + "!");
            Console.Clear();

        }

        public static void chooseLanguage()
        {
            Console.WriteLine("Choose your language:");
            Console.WriteLine("1. English");
            Console.WriteLine("2. German");
            Console.WriteLine("3. Type your own language");
            Console.Write("> ");
            char charInput = Console.ReadKey().KeyChar;
            while (charInput != '1' && charInput != '2' && charInput != '3')
            {
                Console.WriteLine("Invalid Choice");
                Console.Write("> ");
                charInput = Console.ReadKey().KeyChar;
                Console.Clear();
            }
            if (charInput == '1')
            {
                language = "en";
            }
            else if (charInput == '2')
            {
                language = "de";
            }
            else if (charInput == '3')
            {
                Console.WriteLine("Please enter your language:");
                language = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Invalid Choice");
            }

        }

        public static void Dialog(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void Dialog(string message, string color, bool oneLine = false)
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
            if (oneLine)
            {
                Console.Write(message);
            }
            else
            {
                Console.WriteLine(message);
            }
           
            //Console.ReadKey();
            Console.ResetColor();
        }
        static void Choice()
        {
            char charInput;
            Console.WriteLine("What would you like to do" + characterName + " ?");
            Console.WriteLine("1. Go North");
            Console.WriteLine("2. Go East");
            Console.Write("> ");
            charInput = Console.ReadKey().KeyChar;
            while (charInput != '1' && charInput != '2')
            {
                
                Console.WriteLine("What would you like to do" + characterName + " ?");
                Console.WriteLine("1. Go North");
                Console.WriteLine("2. Go East");
                Console.Write("> ");
                
                Console.WriteLine(" was an Invalid Choice please choose 1 or 2");
                Console.Write("> ");
                
                charInput = Console.ReadKey().KeyChar;
                Console.Clear();
            }
            if (charInput == '1')
            {
                Console.WriteLine("You go North");
            }
            else if (charInput == '2')
            {
                Console.WriteLine("You go East");
            }
            else
            {
                Console.WriteLine("Invalid Choice");
            }
        }
        public static void AI(string userInput)
        {
            // Code for AI method goes here

            
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
            Game.CreateCharacter();
            Game.chooseLanguage();
            Console.Clear();
            connectAzureOpenAIAsync();
            

            bool gameEnded = false;

            Console.ReadKey();

        }

        private static async Task connectAzureOpenAIAsync()
        {
            var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

            string proxyUrl = config.GetSection("settings")["endpoint"];

            //string proxyUrl = config[\settings:endpoint\];
            string key = config.GetSection("settings")["key"];

            // the full url is appended by /v1/api
            Uri proxyUri = new(proxyUrl + "/v1/api");

            // the full key is appended by "/YOUR-GITHUB-ALIAS"
            AzureKeyCredential token = new(key);

            // instantiate the client with the "full" values for the url and key/token
            OpenAIClient openAIClient = new(proxyUri, token);

            ChatCompletionsOptions completionOptions = new()
            {
                MaxTokens = 2048,
                Temperature = 0.7f,
                NucleusSamplingFactor = 0.95f,
                DeploymentName = "gpt-35-turbo"
            };
            // the system prompt is the "context" for the AI
            var systemPrompt =
            """
            You are a Dungon Master for a text adventure game with the name 'The Enchanted Forest'. You will tell a story to the Hero. At the end of your text you will offer two options for the hero to go further.
            Please send your text only in the following language: 
            """ + Game.language + """

            Please use the following name for the Hero:
            """ + Game.characterName + """

            You can format your text with all C# commands for Console.WriteLine().
                        
            Please send the Options for the Hero like the following format:
            Option 1: Some Text
            Option 2: Some other Text

            If the hero  dies, please send the following text:
            You died. Game Over.

            If the hero loses, please send the following text:
            You are lost in the Enchanted Forest. Your Soul will be trapped here forever. Game Over.

            If the hero wins, please send the following text:
            You found the Lost City of Zorath. You won the game. Congratulations!
            """;
            ChatMessage systemMessage = new(ChatRole.System, systemPrompt);

            completionOptions.Messages.Add(systemMessage);

            // the user prompt is the "question" for the AI
            string heroRequest = """
Hello, i am 
""" + Game.characterName + """ and i am a Hero in the Enchanted Forest. I am looking for the Lost City of Zorath. Can you help me find it?""";
            /*            ChatCompletions response = await openAIClient.GetChatCompletionsAsync(completionOptions);

                        ChatMessage assistantResponse = response.Choices[0].Message;
                        Game.Dialog($"Dungeon Master >>> {assistantResponse.Content}", "cyan");*/
            var counter = 0;
            while (true)
            {
                try
                {

                    ChatCompletions response = await openAIClient.GetChatCompletionsAsync(completionOptions);

                    ChatMessage assistantResponse = response.Choices[0].Message;

                    if (counter == 0)
                        Game.Dialog("Dungeon Master >>>" + "\n" + $"{assistantResponse.Content}", "cyan");

                    completionOptions.Messages.Add(assistantResponse);


                    Game.Dialog("What do you want to do, " + Game.characterName + " (Type 'quit' to exit)", "yellow");
                    Game.Dialog("You (" + Game.characterName + ") >>> ", "green", true);
                    heroRequest = Console.ReadLine();

                    if (heroRequest.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    ChatMessage heroMessage = new(ChatRole.User, heroRequest);

                    completionOptions.Messages.Add(heroMessage);

                    response = await openAIClient.GetChatCompletionsAsync(completionOptions);

                    assistantResponse = response.Choices[0].Message;

                    if (assistantResponse.Content.Contains("You died. Game Over."))
                    {
                        Game.Dialog("Dungeon Master >>>" + "\n" + $"{assistantResponse.Content}", "red");
                        break;
                    }
                    else if (assistantResponse.Content.Contains("You are lost in the Enchanted Forest. Your Soul will be trapped here forever. Game Over."))
                    {
                        Game.Dialog("Dungeon Master >>>" + "\n" + $"{assistantResponse.Content}", "yellow");
                        break;
                    }
                    else if (assistantResponse.Content.Contains("You found the Lost City of Zorath. You won the game. Congratulations!"))
                    {
                        Game.Dialog("Dungeon Master >>>" + "\n" + $"{assistantResponse.Content}", "magenta");
                        break;
                    }
                    else
                    {
                        Game.Dialog("Dungeon Master >>>" + "\n" + $"{assistantResponse.Content}", "cyan");
                    }
                    counter++;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
}