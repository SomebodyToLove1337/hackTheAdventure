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





//Console.WriteLine($"Hello, {config["endpoint"]}");

namespace hackTheAdventure
{
    public static class Game
    {
        //Character Creation Variables
        public static string characterName = "Cassius";

        //Print out Game Tiltle and overview
        public static void StartGame()
        {
            //Console.WriteLine("The Lost City of Zorath!\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to The Enchanted Forest! You are about to embark on a magical journey through a mystical realm filled with wonder and danger. You will encounter many strange creatures and obstacles along the way, but with your wits and courage, you will overcome them all and emerge victorious.\r\n\r\nAs you explore the enchanted woods and winding rivers of this mysterious land, you will uncover ancient artifacts and long-forgotten secrets that will lead you ever closer to your goal. But beware: there are those who will stop at nothing to keep the secrets of the forest hidden from the world.\r\n\r\nAre you ready to take on the challenge? The fate of the realm rests in your hands. Good luck, adventurer!\r\n");
            Console.WriteLine("        ,     \\    /      ,        \r\n       / \\    )\\__/(     / \\       \r\n      /   \\  (_\\  /_)   /   \\      \r\n ____/_____\\__\\@  @/___/_____\\____ \r\n|             |\\../|              |\r\n|              \\VV/               |\r\n|     The Lost City of Zorath!    |\r\n|_________________________________|\r\n |    /\\ /      \\\\       \\ /\\    | \r\n |  /   V        ))       V   \\  | \r\n |/     `       //        '     \\| \r\n `              V                '");
            Console.ResetColor();
            //CreateCharacter();
            //Choice();

        }

        public static void CreateCharacter()
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
            /*            var json = File.ReadAllText("secrets.json");
                        var secrets = JObject.Parse(json);
                        var secretKey = secrets["endpoint"].ToString();
                        Console.WriteLine(secretKey);
                        Console.ReadKey();*/
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            /*            Game.StartGame();
                        Game.CreateCharacter();
                        connectAzureOpenAIAsync();


                        //
                        Console.ReadKey();*/
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //Game.Dialog("Have you seen a strange creature around here?\nAbout three feet high, greenish, with fluffy hair?\n");
            //Game.Dialog("No, I haven't seen anything like that.", "red");
            Game.StartGame();
            Game.CreateCharacter();
            connectAzureOpenAIAsync();

            bool gameEnded = false;
            while (!gameEnded)
            {
                Console.WriteLine("What would you like to do, " + Game.characterName + "?");
                string userInput = Console.ReadLine();

                // Pass the userInput to the AI function or method
              

                // Pass the user input to the AI for a response
                // and handle the response accordingly

                // Check if the game has ended based on the AI response
                // If the game has ended, set gameEnded to true
            }

            Console.ReadKey();

        }

        private static async Task connectAzureOpenAIAsync()
        {
            var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

            // Retrieve App Secrets
            /*            IConfiguration config = new ConfigurationBuilder()
                            .AddUserSecrets<SomeClass>()
                            .Build();*/

            string proxyUrl = config.GetSection("settings")["endpoint"];

            //string proxyUrl = config[\settings:endpoint\];
            string key = config.GetSection("settings")["key"];

            // the full url is appended by /v1/api
            Uri proxyUri = new(proxyUrl + "/v1/api");

            // the full key is appended by "/YOUR-GITHUB-ALIAS"
            AzureKeyCredential token = new(key);

            // instantiate the client with the "full" values for the url and key/token
            OpenAIClient openAIClient = new(proxyUri, token);

            //Console.WriteLine($"Hello, {config["settings:endpoint"]}");
            //Console.WriteLine(proxyUri + " " + key);

            //Test connection
            /*            ChatCompletionsOptions completionOptions = new()
                        {
                            MaxTokens = 2048,
                            Temperature = 0.7f,
                            NucleusSamplingFactor = 0.95f,
                            DeploymentName = "gpt-3.5-turbo"
                        };

                       completionOptions.Messages.Add(new ChatMessage(ChatRole.System, "You are a Dungon Master for a text adventure game with the name 'The Enchanted Forest'. You will tell a story to the Hero. At the end of your text you will offer two options for the hero to go further."));
                       completionOptions.Messages.Add(new ChatMessage(ChatRole.User, "hi there"));

                        var systemPrompt =
            """
            You are a Dungon Master for a text adventure game with the name 'The Enchanted Forest'. 
            You will tell a story to the Hero. At the end of your text you will offer two options for the hero to go further.
            Please answer in JSON Format, with the two possible options to go further and when the game ends with the value "End: true"
            """;
                        ChatMessage systemMessage = new(ChatRole.System, systemPrompt);

                        ChatCompletions response = await openAIClient.GetChatCompletionsAsync(completionOptions);

                        completionOptions.Messages.Add(systemMessage);

                        // the user prompt is the "question" for the AI
                        string userGreeting = Console.ReadLine();

                        ChatMessage userGreetingMessage = new(ChatRole.User, userGreeting);
                        completionOptions.Messages.Add(userGreetingMessage);

                        //string characterName = Game.characterName;
                        Console.WriteLine(Game.characterName + $" >>> {userGreeting}");
                        //ChatCompletions response = await openAIClient.GetChatCompletionsAsync(completionOptions);

                        ChatMessage assistantResponse = response.Choices[0].Message;

                        Console.WriteLine($"AI >>> {assistantResponse.Content}");

                        Console.ReadKey();*/
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
""";
            ChatMessage systemMessage = new(ChatRole.System, systemPrompt);

            completionOptions.Messages.Add(systemMessage);

            // the user prompt is the "question" for the AI
            string userGreeting = """
Hello, i am 
""" + Game.characterName + """ and i am a Hero in the Enchanted Forest. I am looking for the Lost City of Zorath. Can you help me find it?""";

            ChatMessage userGreetingMessage = new(ChatRole.User, userGreeting);
            completionOptions.Messages.Add(userGreetingMessage);

            //Console.WriteLine($"User >>> {userGreeting}");


            //Console.WriteLine($"AI >>> {assistantResponse.Content}");

            ChatCompletions response = await openAIClient.GetChatCompletionsAsync(completionOptions);

            ChatMessage assistantResponse = response.Choices[0].Message;
            Game.Dialog($"Dungeon Master >>> {assistantResponse.Content}", "cyan");
            //Console.WriteLine($"Dungeon Master >>> {assistantResponse.Content}");

            completionOptions.Messages.Add(assistantResponse);

            var heroRequest = Console.ReadLine();

            Game.Dialog(Game.characterName + $" >>> {heroRequest}", "green");
            //Console.WriteLine($"User >>> {heroRequest}");

            ChatMessage heroMessage = new(ChatRole.User, heroRequest);

            completionOptions.Messages.Add(heroMessage);

            response = await openAIClient.GetChatCompletionsAsync(completionOptions);

            assistantResponse = response.Choices[0].Message;

            Game.Dialog($"Dungeon Master >>> {assistantResponse.Content}", "cyan");
            //Console.WriteLine($"Dungeon Master >>> {assistantResponse.Content}");

            Task.WaitAll();

            //End of Program
            Console.ReadKey();



        }
        private static void testAzureOpenAIConnection()
        {

        }
    }


}