namespace _02_Iterator
{
    interface IAbstractIterator
    {
        Item First();
        
        Item Next();
        
        bool IsDone { get; }
        
        Item CurrentItem { get; }
    }
}