using System.Text;
using LabWork27_buffet.Models;
using LabWork27_buffet.Models.Persons;

namespace LabWork27_buffet.Serivce;

public class Buffet
{
    private readonly List<Product> _products;
    private readonly List<Table> _tables;
    private readonly List<Employee> _employees;
    private readonly List<Visitor> _visitors;
    private readonly List<Order> _orders;
    public IReadOnlyList<Order> Orders => _orders;
    public IReadOnlyList<Product> Products => _products;
    public IReadOnlyList<Table> Tables => _tables;
    public IReadOnlyList<Employee> Employees => _employees;
    public IReadOnlyList<Visitor> Visitors => _visitors; 

    public IReadOnlyList<Table> FreeTables => _tables.Where(x => !x.IsBusy).ToList();
    public IReadOnlyList<Table> ReservedTables => _tables.Where(x => x.IsBusy).ToList();
    
    public IReadOnlyList<Visitor> FreePersons => _visitors.Where(x => x?.IsGetTable == false).ToList();
    public Buffet()
    {
        _products = new List<Product>();
        _tables = new List<Table>();
        _employees = new List<Employee>();
        _visitors = new List<Visitor>();
        _orders = new List<Order>();
        Initialize(5);
    }
    // Создаем со старта 5 столов и одного сотрудника
    private  void Initialize(int countTables)
    {
        var manager = new Employee("Старший менеджер", 50000);

        for (var i = 0; i < 5; i++)
        {
            _tables.Add(new Table(manager));
        }
    }
    // добавление созданного человека для взаимодействия
    public string AddNewPerson(Person newPerson)
    {
        var res = (newPerson.Greetings());
        // Добваляем в спиок по типу
        switch (newPerson)
        {
            case Employee emp:
                _employees.Add(emp);
                break;
            case Visitor visitor:
                _visitors.Add(visitor);
                break;
        }
        return res;
    }
    // Добавить новое блюдо
    public void AddNewProduct(Product newProduct)
    {
        _products.Add(newProduct);
    }
    // Добавить стол
    public void AddTable(Table table)
    {
        // Выбираем сотрудника чтоб приставтить его к этому столу
        _tables.Add(table);
    }
    public void AddOrder(Order order)
    {
        _orders.Add(order);
    }
   public string GetAllInfoAboutTables()
   {
       var sb = new StringBuilder();
        foreach (var table in _tables)
        {
            sb.Append(table + "\n");
        }

        return sb.ToString();
   }
}