namespace _1_store
{
    public class Item : IComparable<Item>, IEquatable<Item>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public int CompareTo(Item? other)
        {
            if (other is not null)
                return Price.CompareTo(other.Price);
            else
                return -1;
        }

        public bool Equals(Item? other)
        {
            return Name.Equals(other.Name);
        }
    }
}
