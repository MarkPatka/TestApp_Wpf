namespace TestApp_Wpf.Infrastructure.Extensions;

public static class CollectionExtensions
{
    public static Queue<T> ToQueue<T>(this IEnumerable<T> values)
    {
        var queue = new Queue<T>();
        var iterator = values.GetEnumerator();
        
        while (iterator.MoveNext()) 
        {
            queue.Enqueue(iterator.Current);
        }
        return queue;
    }

    public static bool IsSubsequence(this IEnumerable<string> subSequence,
        IEnumerable<string> sequence, IEqualityComparer<string>? equalityComparer)
    {
        ArgumentNullException.ThrowIfNull(subSequence);
        ArgumentNullException.ThrowIfNull(sequence);

        var iterator = subSequence.GetEnumerator();
        if (!iterator.MoveNext()) return true; 

        foreach (var element in sequence)
        {            
            if (equalityComparer?.Equals(element, iterator.Current) == true)
            {                
                if (!iterator.MoveNext())
                {
                    return true; 
                }
            }
        }
        return false; 
    }
}
