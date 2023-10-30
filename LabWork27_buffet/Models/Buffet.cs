using LabWork27_buffet.Models.Persons;

namespace LabWork27_buffet.Models;

public class Buffet
{
   
    public  List<Table> Tables { get; }
    public List<Table> BusyTables => Tables.Where(x => !x.IsBusy).ToList(); // занятые столики
    public List<Table> FreeTables => Tables.Where(x => x.IsBusy).ToList(); // не занятые столики
    public List<Visitor> Visitors { get; }
    public List<Visitor> FreeVisitors => Visitors.Where(x => !x.IsGetTable).ToList();
    public List<Visitor> VisitorsOnTable => Visitors.Where(x => x.IsGetTable).ToList();
    public List<Employee> Employees { get; }
    public List<Product> Products { get; }

    public Buffet(int countTables)
    {
        Products = new List<Product>();
        Tables = new List<Table>();
        Employees = new List<Employee>();
        Visitors = new List<Visitor>();
        InitializeBuffet(countTables);
    }
    // Создаем со старта столько столов, сколько укажем в конструкторе, и одного сотрудника
    private  void InitializeBuffet(int countTables)
    {
        var manager = new Employee("Сарший менеджер", 50000);

        for (var i = 0; i < countTables; i++)
        {
            Tables.Add(new Table(manager));
        }
    }
    // добавление созданного человека для взаимодействия
    public string AddNewPerson(Person person)
    {
        // Добваляем в спиок по типу
        switch (person)
        {
            case Employee emp:
                Employees.Add(emp);
                break;
            case Visitor visitor:
                Visitors.Add(visitor);
                break;
        }
        return person.Greetings();
    }
}