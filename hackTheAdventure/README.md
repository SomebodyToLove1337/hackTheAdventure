# hackTheAdventure

This is a program developed for the hackTheAdventure project. It is written in C# and serves as a starting point for the adventure game.

## Description

The program.cs file contains the main entry point for the game. It initializes the game engine and starts the game loop. The game loop handles user input, updates the game state, and renders the game world.

## Features

- Interactive gameplay
- Multiple levels and challenges
- Player customization options

## Planned Features
- Score tracking and leaderboard
- More levels and challenges
- More customization options
- More interactive gameplay

## Installation

To run the program, you need to have the .NET Core SDK installed on your machine. Clone the repository and navigate to the project directory. Then, open a terminal and run the following command:

```bash
dotnet run
```
Add the API key to the secrets.json file. You need an Azure OpenAI Key to run the program. You can get one [here](https://azure.microsoft.com/en-us/services/cognitive-services/openai-text-analytics/).
Unfortunatly, the API key is not free, but you can get a free trial for 7 days.
You have to use the Visual Studio secret manager to add the API key to the secrets.json file. You can find more information about the secret manager [here](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows).

In the future, we will add a way to add the API key to the secrets.json file without using the Visual Studio secret manager.
I will also have to get the appsettings.json file to work, so that the user can add the API key to the file without using the Visual Studio secret manager.

## Usage

Once the program is running, follow the on-screen instructions to navigate through the game. Use the keyboard to input commands and make choices.

## Contributing

Contributions are welcome! If you have any ideas or improvements, feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
