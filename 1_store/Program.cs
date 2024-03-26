using _1_store;
using System.Text;

internal class Program
{

    public enum ItemNames
    {
        Хлеб,
        Колбаса,
        Молоко,
        Батон,
        Масло,
        Мясо,
        Пельмени,
        Мороженое,
        Огурцы,
        Помидоры,
        Апельсины,
        Кефир,
        Чай,
        Печенье,
        Картошка,
    }

    public static void PrintGuide()
    {
        const string Guide =
            "1 - Создать магазин\t\t2 - Ликвидировать магазин\n" +
            "3 - Взять на работу продавца\t4 - Уволить продавца\n" +
            "5 - Добавить товар\t\t6 - Купить товар\n" +
            "7 - Печать спаска товаров\t8 - Пополнить баланс\n" +
            "0 - Инструкция\nESC - выход";

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Guide);
        Console.ResetColor();
    }


    public static ConsoleKeyInfo PressKey()
    {
        int cursorLeft = Console.CursorLeft;
        ConsoleKeyInfo key = Console.ReadKey();
        Console.SetCursorPosition(cursorLeft, Console.CursorTop);
        Console.Write(" ");
        Console.SetCursorPosition(cursorLeft, Console.CursorTop);
        return key;
    }


    public static void DeleteStore(ref Store store)
    {
        try
        {
            if (store is not null)
            {
                try
                {
                    if (store.items is null || (store.items is not null && store.items.Count == 0))
                    {
                        if (store.seller is not null)
                            store.FireSeller();

                        store = null;
                        Console.WriteLine("Магазин успешно ликвидирован.");
                    }
                    else
                        throw new Exception();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Нельзя ликвидировать магазин, в нем еще остались товары! " + e.Message);
                }

            }
            else
                throw new NullReferenceException();
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine("Магазин уже ликвидирован. " + e.Message);
        }
    }


    public static bool StoreExists(Store? store)
    {
        try
        {
            if (store is not null)
                return true;
            else
                throw new NullReferenceException();
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine($"Магазин не создан. " + e.Message);
            return false;
        }
    }




    private static void Main(string[] args)
    {
        Array names = typeof(ItemNames).GetEnumValues();

        Store store = null;

        Buyer buyer = new Buyer();

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Задание 1: работа с Exceptions");
        Console.ResetColor();

        PrintGuide();

        ConsoleKeyInfo key;
        Random rand = new Random();

        do
        {
            key = PressKey();
            switch (key.Key)
            {
                // Создать магазин
                case ConsoleKey.D1:
                    {
                        try
                        {
                            if (store is null)
                            {
                                store = new Store();
                                Console.WriteLine("Создан новый магазин. В нем еще нет продавца и товаров.");
                            }
                            else
                                throw new ArgumentException();
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Магазин уже существует. " + e.Message);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Неизвестная ошибка. " + e.Message);
                        }
                        break;
                    }

                // Ликвидация магазина
                case ConsoleKey.D2:
                    {
                        if (StoreExists(store))
                            DeleteStore(ref store);
                        break;
                    }

                // Добавить продавца
                case ConsoleKey.D3:
                    {
                        if (StoreExists(store))
                        {
                            if (!store.SellerIsPresent())
                            {
                                string name = string.Empty;
                                string surname = string.Empty;

                                Console.Write("Введите имя продавца: ");
                                name = Console.ReadLine();

                                Console.Write("Введите фамилию продавца: ");
                                surname = Console.ReadLine();

                                store.AddSeller(name, surname);
                            }
                            else Console.WriteLine("В магазине уже есть продавец.");
                        }
                            break;
                    }

                // Уволить продавца
                case ConsoleKey.D4:
                    {
                        if (StoreExists(store))
                            store.FireSeller();
                        break;
                    }

                // Добавить товар
                // +StringIsNullException
                case ConsoleKey.D5:
                    {
                        if (StoreExists(store))
                        {
                            if (store.SellerIsPresent())
                            {
                                Console.WriteLine("1 - Добавить вручную, 2 - добавить рандомный товар");
                                key = PressKey();
                                if (key.Key == ConsoleKey.D1)
                                {
                                    try
                                    {
                                        Console.WriteLine("Введите наименование товара: ");
                                        string inputPrice = string.Empty;
                                        string name = string.Empty;
                                        name = Console.ReadLine();
                                        if (name == string.Empty)
                                        {
                                            throw new StringIsEmptyException();
                                        }
                                        else if (store.items is not null)
                                        {
                                            foreach (var item in store.items)
                                            {
                                                if (item.Name == name)
                                                {
                                                    throw new ArgumentException();
                                                }
                                            }
                                        }

                                        decimal price = -1;
                                        do
                                        {
                                            Console.WriteLine("Введите цену товара: ");
                                            inputPrice = Console.ReadLine();
                                            if (inputPrice == string.Empty)
                                            {
                                                throw new StringIsEmptyException();
                                            }
                                            else if (decimal.TryParse(inputPrice, out decimal result) && result >= 0)
                                                price = result;
                                            else 
                                                Console.WriteLine("Некорректный ввод цены товара, повторите: ");
                                        } while (price < 0);

                                        store.AddItem(new(name, price));

                                    }
                                    catch (StringIsEmptyException e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    catch (ArgumentException e)
                                    {
                                        Console.WriteLine("Товар не добавлен, так как уже присутствует в магазине. " + e.Message);
                                    }
                                    break;
                                }
                                else if (key.Key == ConsoleKey.D2)
                                {
                                    store.AddItem(new(((ItemNames)names.GetValue(rand.Next(0, names.Length))).ToString(), (decimal)Math.Round(rand.NextDouble() * rand.Next(0, 100), 2)));
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Неверный ввод.");
                                    break;
                                }
                            }
                        }

                        break;
                    }

                // Купить товар
                case ConsoleKey.D6:
                    {
                        if (StoreExists(store))
                        {
                            if (store.SellerIsPresent())
                            {
                                try
                                {
                                    if (store is not null && store.items is not null && store.items.Count > 1)
                                    {
                                        Console.WriteLine($"Введите номер товара для покупки (1-{store.items.Count}): ");
                                        string input;

                                        int index = -1;

                                        do
                                        {
                                            input = Console.ReadLine();
                                            if (int.TryParse(input, out int result) && result > 0 && result <= store.items.Count)
                                                index = result - 1;
                                            else Console.WriteLine("Некорректный ввод позиции товара, повторите: ");
                                        } while (index < 0);

                                        store.SellItem(buyer, index);

                                    }
                                    else if (store is not null && store.items is not null && store.items.Count == 1)
                                    {
                                        Console.WriteLine($"В наличии только товар {store.items[0].Name} по цене {store.items[0].Price}");
                                        store.SellItem(buyer, 0);
                                    }
                                    else
                                        throw new ArgumentException();
                                }
                                catch (ArgumentException e)
                                {
                                    Console.WriteLine($"Нет доступных товаров к покупке. " + e.Message);
                                }
                            }
                        }

                        break;
                    }

                // Печать списка товаров
                case ConsoleKey.D7:
                    {
                        if (StoreExists(store))
                            if (store.SellerIsPresent())
                                store.Print();
                        break;
                    }

                // Пополнить баланс покупателя
                case ConsoleKey.D8:
                    {
                        Console.WriteLine($"Введите сумму для пополнения: ");
                        buyer.TopUpBalance(Console.ReadLine());
                        break;
                    }

                // Печать инструкции
                case ConsoleKey.D0:
                    {
                        PrintGuide();
                        break;
                    }

                // Выход
                case ConsoleKey.Escape:
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.WriteLine("1");
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        string bye = "Работа приложения завершена...";
                        for (int i = 0; i < bye.Length; i++)
                        {
                            Console.Write(bye[i]);
                            Thread.Sleep(40);
                        }
                        Console.ResetColor();
                        Thread.Sleep(300);
                        Console.WriteLine();
                        Environment.Exit(0);
                        break;
                    }
                default: break;
            }
        }
        while (true);
    }
}
