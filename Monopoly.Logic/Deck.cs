using Monopoly.Logic.Cards;

namespace Monopoly.Logic;

/// <summary>
///     Класс стека, в котором хранятся карты
/// </summary>
public class Deck
{
    public Deck(List<Card> cards)
    {
        Cards = cards.ToArray();
        NewCardArray = Cards;
        Shuffle();

        Top = cards.Count - 1;
    }

    public Card[] Cards { get; set; }
    public Card[] NewCardArray { get; set; }
    public int Top { get; set; }

    public void Push(Card card)
    {
        if (Top + 1 < Cards.Length)
        {
            Top++;
            Cards[Top] = card;
        }
    }


    public Card Peek()
    {
        return !IsEmpty() ? Cards[Top] : null;
    }

    public Card Pop()
    {
        if (!IsEmpty())
        {
            Top--;
            return Cards[Top + 1];
        }

        return null;
    }

    public bool IsEmpty()
    {
        if (Top == -1)
        {
            Cards = NewCardArray;
            Top = Cards.Length - 1;
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Разложить карточки в случайном порядке
    /// </summary>
    public void Shuffle()
    {
        var rng = new Random();
        var n = Cards.Length;
        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            var value = Cards[k];
            Cards[k] = Cards[n];
            Cards[n] = value;
        }
    }
}