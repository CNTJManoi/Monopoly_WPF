using Monopoly.Logic.Cards;

namespace Monopoly.Logic;

/// <summary>
///     Класс стека, в котором хранятся карты
/// </summary>
public class Deck
{
    private int _top;
    public Deck(List<Card> cards)
    {
        Cards = cards.ToArray();
        NewCardArray = Cards;
        Shuffle();

        _top = cards.Count - 1;
    }

    public Card[] Cards { get; private set; }
    public Card[] NewCardArray { get; }

    public void Push(Card card)
    {
        if (_top + 1 < Cards.Length)
        {
            _top++;
            Cards[_top] = card;
        }
    }

    public Card Peek()
    {
        return !IsEmpty() ? Cards[_top] : null;
    }

    public Card Pop()
    {
        if (!IsEmpty())
        {
            _top--;
            return Cards[_top + 1];
        }

        return null;
    }

    public bool IsEmpty()
    {
        if (_top == -1)
        {
            Cards = NewCardArray;
            _top = Cards.Length - 1;
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