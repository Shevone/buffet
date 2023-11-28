namespace LabWork27_buffet.Models.Persons;

public class Visitor : Person
{
    public Visitor(string name) : base(name)
    {
        IsGetTable = false;
    }
    public bool IsGetTable { get; set; }
    public override string ToString()
    {
        return $"Посетитель {Name}, Cидит ли за столиком {IsGetTable}";
    }

    public override string DisplayInfo()
    {
        return this.ToString();
    }
    
    public override string Greetings()
    {
        return $"Здравствуйте, у вас есть свободные столики....";
    }
   
    
    // Метод для сравнения по тому, сидят ли за столиком
    public static bool CompreIsSiting(Visitor obj1, Visitor obj2)
    {
        // Метод сравнения 2х объектов по паспортным данным
        // возвращает true если первый элемент больше второго
        // CompareTo возвращает 1- сли первый больше второ
        // 0 - равны
        // -1 - второй больше первого
        // Чтоб сравнивать пользовательские классы, эти классы должны реализовывать интерфес IComparable
        var res = obj1.IsGetTable.CompareTo(obj2.IsGetTable);
        if (res > 0)
        {
            return true;
        }
        return false;
    }
    // То же самое только в обратном порядке
    public static bool CompreIsNotSiting(Visitor obj1, Visitor obj2)
    {
        // Метод сравнения 2х объектов по паспортным данным
        // возвращает true если первый элемент больше второго
        // CompareTo возвращает 1- сли первый больше второ
        // 0 - равны
        // -1 - второй больше первого
        // Чтоб сравнивать пользовательские классы, эти классы должны реализовывать интерфес IComparable
        var res = obj1.IsGetTable.CompareTo(obj2.IsGetTable);
        if (res > 0)
        {
            return false;
        }
        return true;
    }
}