namespace LabWork27_buffet.Serivce;

public class ConsoleReader
{
    public void ReadKey(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("\nНажмите любую клавишу чтоб продолжить");
        Console.ReadKey();
    }
    public string StringFromConsole(string message)
    {
        Console.WriteLine(message);
        var readLine = Console.ReadLine() ?? "";
        return readLine;
    }

    public int PositiveIntFromConsole(string message)
    {
        Console.WriteLine(message);
        var readLine = Console.ReadLine() ?? "-1";
        if (int.TryParse(readLine, out int result))
        {
            return result;
        }
        return -1;
    }

    public int SelectMenu<T>(List<T> menuItems, string mes)
    {
        // Метод для того чтобы представить коллекцию в виде свич меню в консоли
        // Возвращает порядковый номер в списке выбранного элемента
        if (menuItems.Count == 0) return -1;
       
        
        int lenght = menuItems.Count;
        int row = Console.CursorTop;
        int col = Console.CursorLeft;
        int index = 0;
        while (true)
        {
            DrawMenu(menuItems, row, col, index, mes);
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
                    Console.Clear();
                    return -1;
                case ConsoleKey.Enter:
                    switch (index)
                    {
                        case 0:
                            if (menuItems[0]?.ToString() != "Выход") return 0;
                            Console.Clear();
                            return -1;
                        default:
                            Console.Clear();
                            return index;
                            
                    }
            }
        }
    }
    private static void DrawMenu<T>(IReadOnlyList<T> items, int row, int col, int index, string mes)
    {
        Console.Clear();
       
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
        Console.WriteLine($"{mes}\nДля выхода нажмите 'BackSpace'");
        
    }
}