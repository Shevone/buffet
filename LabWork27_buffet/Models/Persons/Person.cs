namespace LabWork27_buffet.Models.Persons;

public abstract class Person
{
    private PassportData PassportData;
    protected Person(string name)
    {
        PassportData = new PassportData(name);
    }

    public string Name => PassportData.Name;

    public abstract string Greetings();
    public abstract string DisplayInfo();
    // Метод для сравнения по паспортным данным
    public static bool CompareByPassportData(Person obj1, Person obj2)
    {
        // Метод сравнения 2х объектов по паспортным данным
        // возвращает true если первый элемент больше второго
        // CompareTo возвращает 1- сли первый больше второ
        // 0 - равны
        // -1 - второй больше первого
        // Чтоб сравнивать пользовательские классы, эти классы должны реализовывать интерфес IComparable
        var res = obj1.PassportData.CompareTo(obj2.PassportData);
        if (res > 0)
        {
            return true;
        }
        return false;
    }
}
