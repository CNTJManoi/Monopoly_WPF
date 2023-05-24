namespace Monopoly.Logic;

/// <summary>
///     класс, который бросает два кубика в игре
/// </summary>
public class Dice
{
    private Random _random;
    public Dice()
    {
        HasBeenThrown = false;
        _random = new Random();
    }
    public int FirstDice { get; private set; }
    public int SecondDice { get; private set; }
    public bool HasBeenThrown { get; set; }

    public int ThrowDice()
    {
        FirstDice = _random.Next(1, 6);
        SecondDice = _random.Next(1, 6);
        HasBeenThrown = true;
        return FirstDice + SecondDice;
    }

    public bool IsDouble()
    {
        return FirstDice == SecondDice;
    }

    public void EndTurn()
    {
        HasBeenThrown = false;
    }
}