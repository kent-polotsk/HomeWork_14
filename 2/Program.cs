using _1_store;
using System.Text;

internal class Program
{
    static void PrintGuide()
    {
        const string Guide =
            "1 - Ввести первое число\t2 - Ввести второе число\n" +
            "3 - Разделить первое число на второе\n" +
            "0 - Инструкция\tESC - выход";

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(Guide);
        Console.ResetColor();
    }

    static ConsoleKeyInfo PressKey()
    {
        int cursorLeft = Console.CursorLeft;
        ConsoleKeyInfo key = Console.ReadKey();
        Console.SetCursorPosition(cursorLeft, Console.CursorTop);
        Console.Write(" ");
        Console.SetCursorPosition(cursorLeft, Console.CursorTop);
        return key;
    }

    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Задание 2: работа с Exceptions");
        Console.ResetColor();

        PrintGuide();

        ConsoleKeyInfo key;

        string input = string.Empty;
        int first = 0,  second = 0;

        do
        {
            key = PressKey();
            switch (key.Key)
            {
                // Ввод первого числа
                case ConsoleKey.D1:
                    {
                        Console.WriteLine("Введите первое число (0-255): ");
                        Input(ref first);
                        break;
                    }

                // Ввод второго числа
                case ConsoleKey.D2:
                    {
                        Console.WriteLine("Введите второе число (0-255): ");
                        Input(ref second);
                        break;
                    }

                // Деление первого числа на второе
                case ConsoleKey.D3:
                    {
                        try
                        {
                            if (second != 0)
                                Console.WriteLine($" {first} / {second} = {Math.Round(first / (double)(second), 5)}");
                            else
                                throw new DivideByZeroException();
                        } 
                        catch (DivideByZeroException e)
                        {
                            Console.WriteLine("Ошибка деления на ноль: второе число равно нулю. " + e.Message);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Неизвестная ошибка. " + e.Message);
                        }
                        finally 
                        { 
                            Console.WriteLine("Для продолжения сделайте выбор\n"); 
                        }
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
                            Thread.Sleep(30);
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

        void Input(ref int num)
        {
            try
            {
                input = Console.ReadLine();
                if (input != string.Empty)
                {
                    try
                    {
                        if (typeof(int) == int.Parse(input).GetType())  
                        {
                            int tmp = int.Parse(input);
                            try
                            {
                                if (tmp >= 0 && tmp <= 255)
                                    num = tmp;
                                else 
                                    throw new FormatException();
                            }
                            catch (FormatException e) 
                            {
                                Console.WriteLine("Ошибка формата ввода: число вне диапазона " + e.Message);
                            }
                        }
                        else
                            throw new FormatException();
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Ошибка формата ввода: введено не число. " + e.Message);
                    }
                    finally { Console.WriteLine("Сделайте следующий выбор\n"); }
                }
                else
                    throw new StringIsEmptyException();
            }
            catch (StringIsEmptyException e)
            {
                Console.WriteLine($"StringIsEmpty. {e.Message} Время возникновения {e.Time.ToLongTimeString()}");
            }
        }
    }
}