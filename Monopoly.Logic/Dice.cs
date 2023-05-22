namespace Monopoly.Logic;

/// <summary>
///     класс, который бросает два кубика в игре
/// </summary>
public class Dice
{
    public Dice()
    {
        HasBeenThrown = false;
        Random = new Random();
    }

    public int FirstDice { get; set; }
    public int SecondDice { get; set; }
    public bool HasBeenThrown { get; set; }
    private Random Random { get; }

    public int ThrowDice()
    {
        FirstDice = Random.Next(1, 6);
        SecondDice = Random.Next(1, 6);
        return FirstDice + SecondDice;
    }

    public bool IsDouble()
    {
        return FirstDice == SecondDice;
    }
}