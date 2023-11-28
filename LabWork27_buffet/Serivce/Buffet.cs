using System.Text;
using LabWork27_buffet.Lab2;
using LabWork27_buffet.Models;
using LabWork27_buffet.Models.Persons;

namespace LabWork27_buffet.Serivce;

public class Buffet
{
    private  List<Product> _products;
    private  List<Table> _tables;
    private  PersonCollection<Employee> _employees;
    private  PersonCollection<Visitor> _visitors;
    private  List<Order> _orders;
    public List<Order> Orders => _orders;
    public List<Product> Products => _products;
    public List<Table> Tables => _tables;
    public List<Employee> Employees => new (_employees);
    public List<Visitor> Visitors => new (_visitors); 

    public List<Table> FreeTables => _tables.Where(x => !x.IsBusy).ToList();
    public List<Table> ReservedTables => _tables.Where(x => x.IsBusy).ToList();
    
    public List<Visitor> FreePersons => _visitors.Where(x => x?.IsGetTable == false).ToList();
    public Buffet()
    {
        _products = new List<Product>();
        _tables = new List<Table>();
        _employees = new PersonCollection<Employee>();
        _visitors = new PersonCollection<Visitor>();
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
   // =======================================================================================================
   // Сортировка
   public void SortVis(int index)
   {
       // Из вне получаем индек и сортируем по выбранному методу
       switch (index)
       {
           case 1:
               // Паспортные данные
               _visitors.SortPersons(Person.CompareByPassportData);
                break;
           case 2:
               // Посадка возрастание
               _visitors.SortPersons(Visitor.CompreIsSiting);
                break;
           case 3:
               // Посадка убывание
               _visitors.SortPersons(Visitor.CompreIsNotSiting);
                break;
       }
   }
   public void SortEmp(int index)
   {
       // Из вне получаем индек и сортируем по выбранному методу
       switch (index)
       {
           case 1:
               // Паспортные данные
               _employees.SortPersons(Person.CompareByPassportData);
               break;
           case 2:
               // ЗП возрастание
               _employees.SortPersons(Employee.CompareByHighestSalary);
               break;
           case 3:
               // ЗП убывание
               _employees.SortPersons(Employee.CompareByLowestSalary);
               break;
       }
   }
}