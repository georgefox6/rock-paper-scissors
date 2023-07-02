using RockPaperScissors.Data;

namespace RockPaperScissors.Services;
public class OutputRenderer
{
    public static void RenderGameUpdate(GameState gameState)
    {
        var lastRound = gameState.PreviousMoves[gameState.PreviousMoves.Count - 1];
        var playerOneMove = lastRound.Item1;
        var playerTwoMove = lastRound.Item2;

        Console.Clear();
        Console.WriteLine($"Best of {gameState.BestOf}");
        Console.WriteLine($"{gameState.PlayerOne.Name}'s move: {playerOneMove}");
        Console.WriteLine($"{gameState.PlayerTwo.Name}'s move: {playerTwoMove}");
        if (gameState.PreviousRoundWinner != null)
        {
            Console.WriteLine($"{gameState.PreviousRoundWinner.Name} wins this round!");
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{gameState.PlayerOne.Name}: {gameState.PlayerOneScore}");
        Console.WriteLine($"{gameState.PlayerTwo.Name}: {gameState.PlayerTwoScore}");
        Console.WriteLine();
        Console.ResetColor();
    }

    public static void RenderGameFinished(GameState gameState)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("The game is finished!");
        Console.WriteLine($"The winner is... {gameState.PreviousRoundWinner.Name}");
        Console.ResetColor();
        Console.WriteLine($"The final score was {gameState.PlayerOneScore} - {gameState.PlayerTwoScore}");

        var mostUsedMove = GetMostUsedMove(gameState.PreviousMoves);

        Console.WriteLine($"The most used move was {mostUsedMove}");
    }

    private static Move GetMostUsedMove(List<Tuple<Move, Move>> rounds) 
    {
        var moves = new List<Move>();
        foreach(var round in rounds)
        {
            moves.Add(round.Item1);
            moves.Add(round.Item2);
        }

        var mostCommonValue = moves
            .GroupBy(value => value)
            .OrderByDescending(group => group.Count())
            .Select(group => group.Key)
            .FirstOrDefault();

        return mostCommonValue;
    }
}
