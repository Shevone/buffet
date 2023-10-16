namespace LabWork27_buffet.Serivce;

// Интерфейс ридера
public interface IReader
{
    T? GetItemFromList<T>(List<T?> list, string message); // Метод для того чтобы получить с консоли элемент коллекции
}