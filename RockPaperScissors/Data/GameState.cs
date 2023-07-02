namespace RockPaperScissors.Data;
public class GameState
{
    public int BestOf { get; set; }
    public int TurnNumber { get; set; }
    public GameMode GameMode { get; set; }
    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }
    public List<Tuple<Move, Move>> PreviousMoves { get; set; } = new List<Tuple<Move, Move>>();
    public bool IsFinished { get; set; } = false;
    public int PlayerOneScore { get; set; } = 0;
    public int PlayerTwoScore { get; set; } = 0;
    public Player PreviousRoundWinner { get; set; }


    public void UpdateScores()
    {
        //Get the moves from the last round
        var lastRound = PreviousMoves[PreviousMoves.Count - 1];
        var playerOneMove = lastRound.Item1;
        var playerTwoMove = lastRound.Item2;

        //Calculate winner
        switch (playerOneMove)
        {
            case Move.Rock:
                if (playerTwoMove == Move.Paper || playerTwoMove == Move.Spock)
                {
                    PlayerTwoScore += 1;
                    PreviousRoundWinner = PlayerTwo;
                }
                else if (playerTwoMove == Move.Scissors || playerTwoMove == Move.Lizard)
                {
                    PlayerOneScore += 1;
                    PreviousRoundWinner = PlayerOne;
                }
                break;

            case Move.Paper:
                if (playerTwoMove == Move.Rock || playerTwoMove == Move.Spock)
                {
                    PlayerOneScore += 1;
                    PreviousRoundWinner = PlayerOne;
                }
                else if (playerTwoMove == Move.Scissors || playerTwoMove == Move.Lizard)
                {
                    PlayerTwoScore += 1;
                    PreviousRoundWinner = PlayerTwo;
                }
                break;

            case Move.Scissors:
                if (playerTwoMove == Move.Rock || playerTwoMove == Move.Spock)
                {
                    PlayerTwoScore += 1;
                    PreviousRoundWinner = PlayerTwo;
                }
                else if (playerTwoMove == Move.Paper || playerTwoMove == Move.Lizard)
                {
                    PlayerOneScore += 1;
                    PreviousRoundWinner = PlayerOne;
                }
                break;

            case Move.Lizard:
                if (playerTwoMove == Move.Rock || playerTwoMove == Move.Scissors)
                {
                    PlayerTwoScore += 1;
                    PreviousRoundWinner = PlayerTwo;
                }
                else if (playerTwoMove == Move.Paper || playerTwoMove == Move.Spock)
                {
                    PlayerOneScore += 1;
                    Console.WriteLine($"{PlayerOne.Name} wins this round!");
                }
                break;

            case Move.Spock:
                if (playerTwoMove == Move.Paper || playerTwoMove == Move.Lizard)
                {
                    PlayerTwoScore += 1;
                    PreviousRoundWinner = PlayerTwo;
                }
                else if (playerTwoMove == Move.Rock || playerTwoMove == Move.Scissors)
                {
                    PlayerOneScore += 1;
                    PreviousRoundWinner = PlayerOne;
                }
                break;
        }

        TurnNumber++;

        double winningScore = (double)BestOf / 2.0;

        if (PlayerOneScore > winningScore || PlayerTwoScore > winningScore)
        {
            IsFinished = true;
        }
    }
}

public enum GameMode
{
    Standard,
    Extended
}
