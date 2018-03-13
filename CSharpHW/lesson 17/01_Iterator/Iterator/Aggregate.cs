namespace Iterator
{
    abstract class Aggregate<T>
    {
        public abstract Iterator<T> CreateIterator();
    }
}