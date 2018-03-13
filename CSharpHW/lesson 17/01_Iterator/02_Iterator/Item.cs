namespace _02_Iterator
{
    /// <summary>
    /// A collection item
    /// </summary>
    class Item
    {
        private readonly string _name;

        // Constructor
        public Item(string name)
        {
            _name = name;
        }

        // Gets name
        public string Name
        {
            get { return _name; }
        }
    }
}