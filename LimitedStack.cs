public class LimitedSizeStack<T>
{
    public int Count { get; set; } = 0;
    private int size;
    public StackItem<T> Tail { get; set; }
    public StackItem<T> Head { get; set; }

    public LimitedSizeStack(int undoLimit)
    {
        size = undoLimit;
    }

    public void Push(T item)
    {
        
        if (size <= 0)
            return;
        if (Head == null)
        {
            Head = Tail = new StackItem<T>(item);
            Count++;
        }
        else if (size > Count)
            AppendValue(item);
        else
        {
            Count--;
            if (Count == 0)
            {
                Head = Tail = new StackItem<T>(item);
                Count++;
            }
            else
            {
                Tail = Tail.Prev;
                Tail.Next = null;
                AppendValue(item);
            }
        }
    }

    public T Pop()
    {
        if (Count == 1)
        {
            Count--;
            var temp = Head.Value;
            Head = Tail = null;
            return temp;
        }
        else
        {
            var temp = Head;
            Head = Head.Next;
            Head.Prev = null;
            Count--;
            return temp.Value;
        }

        return default(T);
    }

    #region HelperMethod
    void AppendValue(T item)
    {
        var temp = new StackItem<T>(item) { Next = Head };
        Head.Prev = temp;
        Head = temp;
        Count++;
    }
    #endregion

}

public class StackItem<T>
{
    public T Value { get; set; }
    
    public StackItem<T> Next { get; set; }
    public StackItem<T> Prev { get; set; }

    public StackItem(T value)
    {
        Value=value;
    }
}