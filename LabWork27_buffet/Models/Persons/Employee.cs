namespace LabWork27_buffet.Models.Persons;

public class Employee : Peron
{
    public Employee(string name, int salary) : base(name)
    {
        Salary = salary;
        NumberOfTableServed = 0;
    }
    private int _salary;
    public int NumberOfTableServed { get; private set; }

    public int Salary
    {
        get => _salary;
        set => _salary = value <= 0 ? 1 : value;
    }
    public void ServeOneMoreTable()
    {
        NumberOfTableServed++;
    }

    public override string ToString()
    {
        return $"Работник {Name}, Зарплату {Salary}, Количество обслуженных столов за смену {NumberOfTableServed}";
    }

    public override string Greetings()
    {
        return $"Здравствуйте, меня зовут {Name}. Добро пожаловать в наш ресторан.";
    }
}