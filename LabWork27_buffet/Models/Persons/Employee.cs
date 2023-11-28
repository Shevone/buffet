namespace LabWork27_buffet.Models.Persons;

public class Employee : Person
{
    public Employee(string name, int salary) : base(name)
    {
        Salary = salary;
        NumberOfTableServed = 0;
    }
    private int _salary;
    private int NumberOfTableServed { get; set; }

    public int Salary
    {
        get => _salary;
        set => _salary = value <= 0 ? 1 : value;
    }
    public void ServeOneMoreTable()
    {
        NumberOfTableServed++;
    }

    public override string DisplayInfo()
    {
        return this.ToString();
    }

    public override string ToString()
    {
        return $"Работник {Name}, Зарплату {Salary}, Количество обслуженных столов за смену {NumberOfTableServed}";
    }

    public override string Greetings()
    {
        return $"Здравствуйте, меня зовут {Name}. Добро пожаловать в наш ресторан.";
    }
    
    // Метод сортировки по возрастанию зарплаты
    public static bool CompareByHighestSalary(Employee obj1, Employee obj2)
    {
        // Метод сравнения 2х объектов по паспортным данным
        // возвращает true если первый элемент больше второго
        // CompareTo возвращает 1- сли первый больше второ
        // 0 - равны
        // -1 - второй больше первого
        // Чтоб сравнивать пользовательские классы, эти классы должны реализовывать интерфес IComparable
        var res = obj1.Salary.CompareTo(obj2.Salary);
        if (res > 0)
        {
            return true;
        }
        return false;
    }
    // Метод сортировки по убыванию зарплаты
    public static bool CompareByLowestSalary(Employee obj1, Employee obj2)
    {
        // Метод сравнения 2х объектов по паспортным данным
        // возвращает true если первый элемент больше второго
        // CompareTo возвращает 1- сли первый больше второ
        // 0 - равны
        // -1 - второй больше первого
        // Чтоб сравнивать пользовательские классы, эти классы должны реализовывать интерфес IComparable
        var res = obj1.Salary.CompareTo(obj2.Salary);
        if (res > 0)
        {
            return false;
        }
        return true;
    }
}
