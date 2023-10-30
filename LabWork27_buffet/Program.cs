using LabWork27_buffet.Models;
using LabWork27_buffet.Models.Persons;
using LabWork27_buffet.Serivce;

namespace LabWork27_buffet;

public static class Program
{
    public static void Main(string[] args)
    {
        
        
        // Создадим Работников
        var buffet = new Buffet(5);
        var emp1 = new Employee("Вася", 18000);
        var emp2 = new Employee("Иван", 18000);
        var emp3 = new Employee("Петр", 21000);
        var emp4 = new Employee("Стажер Егор", 15000);
        buffet.Employees.AddRange(
            new List<Employee>() { emp1, emp2,emp3,emp4 }
            );
        //Создадим Посетителей
        var vis1 = new Visitor("Игор Николаевич");
        var vis2 = new Visitor("Георгий Петрович");
        var vis3 = new Visitor("Николай Дмитриевич");
        var vis4 = new Visitor("Данил Юрьевич");
        var vis5 = new Visitor("Василия Николаевна");
        var vis6 = new Visitor("Виктория Олеговна");
        buffet.Visitors.AddRange(new List<Visitor>()
        {
            vis1,vis2,vis3,vis4,vis5,vis6
        });
        var prod1 = new Product(321.2, "Селедка под шубой");
        var prod2 = new Product(32.2, "Картошка");
        var prod3 = new Product(400.2, "Шашлык");
        var prod4 = new Product(1.2, "Салат Цезарь");
        var prod5 = new Product(10.2, "Десерт  сладкий");
           // Создадим блюда
        buffet.Products.AddRange(new List<Product>()
        {
            prod1,prod2,prod3,prod4,prod5
        });
        var consoleReaderService = new ConsoleReader();
        var service = new BaseService(consoleReaderService,buffet);
        service.Run();
    }

    
}