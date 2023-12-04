﻿/*
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            characterName = Console.ReadLine();
            Console.WriteLine("Hello " + characterName + "!");
            Console.WriteLine("Press any key to continue...");
            Console.Clear();

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
            if (oneLine == true)
            {
                Console.Write(message);
            }
            else
            {
                Console.WriteLine(message);
            }

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
            //Game.StartGame();
            //Game.CreateCharacter();
            //connectAzureOpenAIAsync();
            //testConnection();
            NonStreamingChat();

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
""";
            ChatMessage systemMessage = new(ChatRole.System, systemPrompt);

            completionOptions.Messages.Add(systemMessage);

            // the user prompt is the "question" for the AI
            string userGreeting = """
Hello, i am 
""" + Game.characterName + """ and i am a Hero in the Enchanted Forest. I am looking for the Lost City of Zorath. Can you help me find it?""";

            ChatMessage userGreetingMessage = new(ChatRole.User, userGreeting);
            completionOptions.Messages.Add(userGreetingMessage);

            Console.WriteLine($"User >>> {userGreeting}");



            while (true)
            {

                try
                {
                    Console.WriteLine("What do you want to do? (Type 'quit' to exit)");
                    //ChatCompletions response = await openAIClient.GetChatCompletionsAsync(completionOptions);
                    ChatCompletions response = await openAIClient.GetChatCompletionsAsync(completionOptions);

                    ChatMessage assistantResponse = response.Choices[0].Message;
                    Console.WriteLine($"AI >>> {assistantResponse.Content}");

                    Game.Dialog("Dungeon Master >>>" + "\n" + $"{assistantResponse.Content}", "cyan");
                    //Console.WriteLine($"Dungeon Master >>> {assistantResponse.Content}");

                    completionOptions.Messages.Add(assistantResponse);

                    Console.WriteLine("What do you want to do? (Type 'quit' to exit)");
                    Game.Dialog(Game.characterName + " >>> ", "green", true);
                    var heroRequest = Console.ReadLine();

                    //Game.Dialog(Game.characterName + "\n" + $" >>> {heroRequest}", "green");
                    //Console.WriteLine($"User >>> {heroRequest}");
                    if (heroRequest.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    ChatMessage heroMessage = new(ChatRole.User, heroRequest);

                    completionOptions.Messages.Add(heroMessage);

                    response = await openAIClient.GetChatCompletionsAsync(completionOptions);

                    assistantResponse = response.Choices[0].Message;

                    //Game.Dialog("Dungeon Master >>>" + "\n" + $"{assistantResponse.Content}", "cyan");
                    //Console.WriteLine($"Dungeon Master >>> {assistantResponse.Content}");

                    Task.WaitAll();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        public static async Task testConnection()
        {
            if (Game.statusDev == true)
            {
                Console.WriteLine("Test Connection to Azure OpenAI API");
            }
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
            You are a hiking enthusiast who helps people discover fun hikes. You are upbeat and friendly. 
            You ask people what type of hikes they like to take and then suggest some.
            """;
            ChatMessage systemMessage = new(ChatRole.System, systemPrompt);

            completionOptions.Messages.Add(systemMessage);

            // the user prompt is the "question" for the AI
            string userGreeting = """
            Hallo, ich habe fragen zu schönen Wanderwegen rund um Böbing. Können Sie mir helfen?
            """;

            ChatMessage userGreetingMessage = new(ChatRole.User, userGreeting);
            completionOptions.Messages.Add(userGreetingMessage);

            Console.WriteLine($"User >>> {userGreeting}");


            //Console.WriteLine($"AI >>> {assistantResponse.Content}");

            ChatCompletions response = await openAIClient.GetChatCompletionsAsync(completionOptions);

            ChatMessage assistantResponse = response.Choices[0].Message;

            Console.WriteLine($"AI >>> {assistantResponse.Content}");

            completionOptions.Messages.Add(assistantResponse);

            var hikeRequest =
             """
            Kannst du mir empfehlungen für Sehenswürdigkeiten in der Nähe geben?
            """;

            Console.WriteLine($"User >>> {hikeRequest}");

            ChatMessage hikeMessage = new(ChatRole.User, hikeRequest);

            completionOptions.Messages.Add(hikeMessage);

            response = await openAIClient.GetChatCompletionsAsync(completionOptions);

            assistantResponse = response.Choices[0].Message;

            Console.WriteLine($"AI >>> {assistantResponse.Content}");

            //End of Program
            Console.ReadKey();
        }

    }

}