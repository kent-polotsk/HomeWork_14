namespace _1_store
{
    internal class Buyer
    {
        public string Name { get; }

        public decimal Balance { get; set; }

        public Buyer()
        {
            Name = "Покупатель";
            Balance = 0;
        }

        public void TopUpBalance(string sum)
        {
            try
            {
                if (decimal.TryParse(sum, out decimal result) && result > 0)
                {
                    Balance += result;
                    Console.WriteLine($"{Name} пополнил баланс. На счету {Balance}р.");
                }
                else if (decimal.TryParse(sum, out decimal result2) && result2 == 0)
                {
                    Console.WriteLine($"{Name} не пополнил баланс. На счету по-прежнему {Balance}р.");
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Некорректный ввод суммы. " + e.Message);
            }

        }
    }
}
