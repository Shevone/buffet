namespace LabWork27_buffet.Serivce;

// Интерфейс ридера
public interface IReader
{
    T? GetItemFromList<T>(List<T?> list, string message); // Метод для того чтобы получить с консоли элемент коллекции
    int SelectMenu<T>(List<T> menuItems, string mes = ""); // Выбрать элемента и получтьб его индекс
    void Message(string message);
    public int? GetIntFromConsole(string message);
    public string GetStringFromConsole(string message);
}