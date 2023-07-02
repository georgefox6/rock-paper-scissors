using RockPaperScissors.Data;
using RockPaperScissors.Services;

namespace RockPaperScissors;

class Program
{
    static void Main(string[] args)
    {
        var gameState = InitialiseGame();

        while (!gameState.IsFinished)
        {
            gameState = GameLoop(gameState);

            OutputRenderer.RenderGameUpdate(gameState);
        }

        OutputRenderer.RenderGameFinished(gameState);
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

    public static GameState GameLoop(GameState gameState)
    {
        // Player one decides move
        var playerOneMove = MakeMove(gameState.GameMode, gameState.PlayerOne);

        // Player two decides move
        var playerTwoMove = MakeMove(gameState.GameMode, gameState.PlayerTwo);

        gameState.PreviousMoves.Add(Tuple.Create(playerOneMove, playerTwoMove));

        gameState.UpdateScores();

        return gameState;
    }
    public static Move MakeMove(GameMode gameMode, Player player)
    {
        if (player.IsHuman)
        {
            return CollectAndValidatePlayerMoveInput(gameMode, player);
        }
        else
        {
            return GetRandomMove(gameMode, player);
        }
    }

    public static Move GetRandomMove(GameMode gameMode, Player player)
    {
        Move[] moves;
        if (gameMode == GameMode.Standard)
        {
            moves = new Move[] { Move.Rock, Move.Paper, Move.Scissors };
        }
        else
        {
            moves = new Move[] { Move.Rock, Move.Paper, Move.Scissors, Move.Lizard, Move.Spock };
        }
        Random random = new Random();
        Move move = (Move)moves.GetValue(random.Next(moves.Length));
        return move;
    }


    public static Move CollectAndValidatePlayerMoveInput(GameMode gameMode, Player player)
    {
        string options = "Rock, Paper, Scissors";
        if (gameMode == GameMode.Extended)
        {
            options += ", Lizard, Spock";
        }
        Console.WriteLine($"{player.Name}, enter a move ({options}): ");
        var moveInput = Console.ReadLine();
        Console.Clear();

        while (true)
        {
            Console.Clear();
            switch (moveInput.Trim().ToLower())
            {
                case "rock":
                    {
                        return Move.Rock;
                    }
                case "paper":
                    {
                        return Move.Paper;
                    }
                case "scissors":
                    {
                        return Move.Scissors;
                    }
                case "lizard":
                    {
                        if (gameMode == GameMode.Extended)
                        {
                            return Move.Lizard;
                        }
                        else
                        {
                            Console.WriteLine("You can only select 'Lizard' if you play the extended version");
                        }
                        break;
                    }
                case "spock":
                    {
                        if (gameMode == GameMode.Extended)
                        {
                            return Move.Spock;
                        }
                        else
                        {
                            Console.WriteLine("You can only select 'Spock' if you play the extended version");
                        }
                        break;
                    }
            }
            Console.WriteLine($"Incorrect input. Enter a move ({options}): ");
            moveInput = Console.ReadLine();
        }

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