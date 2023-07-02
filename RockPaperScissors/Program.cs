using RockPaperScissors.Data;

namespace RockPaperScissors;

class Program
{
    static void Main(string[] args)
    {
        var gameState = InitialiseGame();

    }

    public static GameState InitialiseGame()
    {
        var gameState = new GameState();    

        var bestOf = CollectAndValidateBestOfInput();
        var playerOne = CollectAndValidatePlayerInput("Player One");
        var playerTwo = CollectAndValidatePlayerInput("Player Two");
        var gameMode = CollectAndValidateGameModeInput();

        return new GameState
        {
            BestOf = bestOf,
            TurnNumber = 0,
            GameMode = gameMode,
            PlayerOne = playerOne,
            PlayerTwo = playerTwo,
            IsFinished = false            
        };
    }

    public static int CollectAndValidateBestOfInput()
    {
        Console.WriteLine("I would like to play a best of...");
        var bestOfInput = Console.ReadLine();
        Console.Clear();

        while (true)
        {
            if (int.TryParse(bestOfInput, out int output) && output % 2 == 1)
            {
                return output;
            }
            else
            {
                Console.WriteLine("You have entered an invalid best of. Please enter an odd number.");
                bestOfInput = Console.ReadLine();
                Console.Clear();
            }
        }
    }

    public static Player CollectAndValidatePlayerInput(string playerNumber)
    {
        var player = new Player();
        
        Console.WriteLine($"Enter the name of {playerNumber}...");
        player.Name = Console.ReadLine().Trim();
        Console.Clear();

        Console.WriteLine($"Do you want {player.Name} to be Human or AI controlled? Enter 'AI' or 'Human'");
        var isHumanInput = Console.ReadLine();

        while (true)
        {
            Console.Clear();
            switch (isHumanInput.Trim().ToLower())
            {
                case "human":
                    {
                        player.IsHuman = true;
                        return player;
                    }
                case "ai":
                    {
                        player.IsHuman = false;
                        return player;
                    }
            }
            Console.WriteLine($"Incorrect input. Do you want {player.Name} to be Human or AI controlled? Enter 'AI' or 'Human'");
            isHumanInput = Console.ReadLine();
        } 
    }

    public static GameMode CollectAndValidateGameModeInput()
    {
        Console.WriteLine("Do you want to play the standard or extended game mode? Enter 'Standard' or 'Extended'");
        var gameModeInput = Console.ReadLine();

        while (true)
        {
            Console.Clear();
            switch (gameModeInput.Trim().ToLower())
            {
                case "standard":
                    {
                        return GameMode.Standard;
                    }
                case "extended":
                    {
                        return GameMode.Extended;
                    }
            }
            Console.WriteLine($"Incorrect input. Do you want to play the standard or extended game mode? Enter 'Standard' or 'Extended'");
            gameModeInput = Console.ReadLine();
        }
    }
}



