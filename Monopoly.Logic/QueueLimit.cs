using System.Collections.ObjectModel;

namespace Monopoly.Logic;

/// <summary>
///     Очередь, которая имеет фиксированный размер и может быть легко использована для привязки
/// </summary>
/// <typeparam name="T"></typeparam>
public class QueueLimit<T> : ObservableCollection<T>
{
    public QueueLimit(int limit)
    {
        Limit = limit;
    }
    public int Limit { get; }

    public void Enqueue(T item)
    {
        while (Count >= Limit) RemoveAt(Count - 1);
        Insert(0, item);
    }
}