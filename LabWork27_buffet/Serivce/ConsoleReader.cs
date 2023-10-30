namespace LabWork27_buffet.Serivce;

public class ConsoleReader : IReader
{
    public int? GetIntFromConsole(string message)
    {
        Console.WriteLine(message);
        if(int.TryParse(Console.ReadLine() ?? "0", out var intRes));
        {
            return intRes;
        }
        return null;

    }

    public string GetStringFromConsole(string message)
    {
        Console.WriteLine(message);
        var readString = Console.ReadLine() ?? "";
        return readString;
    }

    public void Message(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("Нажмите любую клавишу чтобы продолжить");
        Console.ReadKey();
    }
    public T? GetItemFromList<T>(List<T?> list, string message = "")
    {
        var r = SelectMenu(list, message);
        if (r == -1)
        {
            return default;
        }
        Console.Clear();
        return list[r];
    }
    public int SelectMenu<T>(List<T> menuItems, string mes = "")
    {
        // Метод для того чтобы представить коллекцию в виде свич меню в консоли
        // Возвращает порядковый номер в списке выбранного элемента
        if (menuItems.Count == 0) return -1;
        Console.Clear();
        
        int lenght = menuItems.Count;
        Console.WriteLine("Меню");
        Console.WriteLine();
        Console.WriteLine("Для выхода нажмите 'BackSpace'");
        if (mes != "")
        {
            Console.WriteLine(mes);
            Console.WriteLine();
        }
        Console.WriteLine();
        int row = Console.CursorTop;
        int col = Console.CursorLeft;
        int index = 0;
        while (true)
        {
            DrawMenu(menuItems, row, col, index);
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.DownArrow:
                    if (index < menuItems.Count - 1)
                        index++;
                    break;
                case ConsoleKey.UpArrow:
                    if (index > 0)
                        index--;
                    break;
                case ConsoleKey.Backspace:
                    return -1;
                case ConsoleKey.Enter:
                    switch (index)
                    {
                        case 0:
                            if (menuItems[0].ToString() != "Выход") return 0;
                            return -1;
                        default:
                            return index;
                            
                    }
            }
        }
    }
    private static void DrawMenu<T>(List<T> items, int row, int col, int index)
    {
        Console.SetCursorPosition(col, row);
        for (int i = 0; i < items.Count; i++)
        {
            if (i == index)
            {
                Console.BackgroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine(items[i]);
            Console.ResetColor();
        }

        Console.WriteLine();
    }
}