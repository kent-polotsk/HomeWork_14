namespace _1_store
{

    public class ItemsComparer : IEqualityComparer<Item>
    {
        bool IEqualityComparer<Item>.Equals(Item? x, Item? y)
        {
            if (x is null || y is null) return false;
            return x.Name.ToUpper() == y.Name.ToUpper();
        }

        public int GetHashCode(Item obj) => obj.Name.GetHashCode();

    }


    internal class Store
    {
        public List<Item> items;

        public Seller seller { get; set; }

        private Random random;

        public Store()
        {
            random = new Random();
            seller = null;
        }


        public void Print()
        {
            try
            {
                if (items != null && items.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("----Список товаров в магазине----");
                    Console.ResetColor();

                    //foreach (var i in items)
                    for (int i = 0; i< items.Count; i++)
                        Console.WriteLine(i+1 + " Товар: " + items[i].Name + " Цена: " + items[i].Price + "р");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("---------------------------------");
                    Console.ResetColor();
                }
                else
                    throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine("Товаров нет. Магазин пуст. " + e.Message);
            }
        }


        // +StringIsNullException
        public void AddSeller(string name, string surname)
        {
            try
            {
                if (this.seller is null)
                {
                    try
                    {
                        if (name != string.Empty && surname != string.Empty)
                        {
                            this.seller = new Seller(name, surname);
                            Console.WriteLine($"Продавец {seller.Name} {seller.Surname} теперь работает в магазине.");
                        }
                        else
                            throw new StringIsEmptyException();
                    }
                    catch (StringIsEmptyException e)
                    {
                        Console.WriteLine(e.Message + " Неверный формат продавца.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Неизвестная ошибка." + e.Message);
                    }
                }
                else
                    throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine("Продавец уже присутствует в магазине. " + e.Message);
            }
        }


        public void FireSeller()
        {
            try
            {
                if (SellerIsPresent())
                {
                    Console.WriteLine($"Продавец {seller.Name} {seller.Surname} уволен. Дальнейшие операции невозможны, пока не будет нового продавца.");
                    this.seller = null;
                }
                else
                    throw new NullReferenceException();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Продавец отсутствует: null. " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Продавец отсутствует в магазине. " + e.Message);
            }
        }


        public void AddItem(Item item)
        {
            if (SellerIsPresent())
            {
                try
                {
                    if (item is not null)
                    {
                        try
                        {
                            if (items is null)
                            {
                                items = [item];
                                Console.WriteLine($"Первый товар {item.Name} добавлен.");
                            }
                            else if (!items.Contains(item, new ItemsComparer()))
                            {
                                items.Add(item);
                                Console.WriteLine($"Товар {item.Name} добавлен.");
                            }
                            else throw new Exception();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Товар не добавлен, так как уже присутствует в магазине. " + e.Message);
                        }
                    }
                    else
                        throw new ArgumentException();
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Неверный формат товара." + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Неизвестная ошибка." + e.Message);
                }
            }
        }


        public bool SellerIsPresent()
        {
            try
            {
                if (seller is not null)
                    return true;
                else
                    throw new ArgumentException();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("В магазине отсутствует продавец, функция не может быть выполнена! " + e.Message);
                return false;
            }
        }


        public void SellItem(Buyer b, int index) 
        {
            try
            {
                if (b.Balance >= items[index].Price)
                {
                    Console.WriteLine($"Товар {items[index].Name} продан.");
                    items.RemoveAt(index);
                }
                else
                    throw new ArgumentException();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Недостаточно денег у покупателя. "+ e.Message);
            }
        }
    }
}

