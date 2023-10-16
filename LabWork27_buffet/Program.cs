using LabWork27_buffet.Models;
using LabWork27_buffet.Models.Persons;
using LabWork27_buffet.Serivce;

namespace LabWork27_buffet;

public static class Program
{
    public static void Main(string[] args)
    {
        var consoleReaderService = new ConsoleReader();
        var service = new BaseService(consoleReaderService);
        
        // Создадим Работников
        service.AddNewPerson(new Employee("Вася", 18000));
        service.AddNewPerson(new Employee("Иван", 18000));
        service.AddNewPerson(new Employee("Петр", 21000));
        service.AddNewPerson(new Employee("Стажер Егор", 15000));
        service.ChangeServiceManOfTable();
        service.GetAllInfoAboutTables();
       
        //Создадим Посетителей
        service.AddNewPerson(new Visitor("Игор Николаевич"));
        service.AddNewPerson(new Visitor("Георгий Петрович"));
        service.AddNewPerson(new Visitor("Николай Дмитриевич"));
        service.AddNewPerson(new Visitor("Данил Юрьевич"));
        service.AddNewPerson(new Visitor("Василия Николаевна"));
        service.AddNewPerson(new Visitor("Виктория Олеговна"));
        // Создадим блюда
        service.AddNewProduct(new Product(321.2, "Селедка под шубой"));
        service.AddNewProduct(new Product(32.2, "Картошка"));
        service.AddNewProduct(new Product(400.2, "Шашлык"));
        service.AddNewProduct(new Product(1.2, "Салат Цезарь"));
        service.AddNewProduct(new Product(10.2, "Десерт  сладкий"));
        // Усадим 2х и сделаем заказ
        service.SitPersonsToTable(2);
        service.MakeOrder(2);
        // Усадим еще двух и сделаем заказ
        service.SitPersonsToTable(2);
        // Протестируем корректность работы при таких значениях
        service.MakeOrder(7);
        
        // выведем всю инфу
        service.GetAllInfoAboutTables();
        // Освободим один стол
        service.ClearTable();
        // Усадим еще людей и сделаем закза
        service.SitPersonsToTable(2);
        service.MakeOrder(-1);
        // Опять усдаим людей
        service.SitPersonsToTable(2);
        service.SitPersonsToTable(2);
        // Добавил стол
        service.AddTable();
        service.ClearTable();
        service.ClearTable();
        service.ClearTable();
        service.GetAllInfoAboutTables();
    }

    
}