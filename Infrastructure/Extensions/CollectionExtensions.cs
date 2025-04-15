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
}
