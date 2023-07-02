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
}

public enum GameMode
{
    Standard,
    Extended
}
