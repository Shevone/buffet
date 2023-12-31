﻿using System.Text;
using LabWork27_buffet.Models.Persons;

namespace LabWork27_buffet.Models;

public class Table
{
    public int Id { get; }
    private List<Order> _order;
    private List<Visitor> _visitors;
    private Employee ServicePerson { get; set; }

    private static int _id = 1;
    public bool IsBusy { get; set; }

    public Table(Employee servicePerson)
    {
        ServicePerson = servicePerson;
        servicePerson.ServeOneMoreTable();
        _order = new List<Order>();
        _visitors = new List<Visitor>();
        Id = _id;
        _id++;
    }

    public void ChangeServiceMan(Employee newServiceMan)
    {
        newServiceMan.ServeOneMoreTable();
        ServicePerson = newServiceMan;
    }
    public void ClearTheTable()
    {
        IsBusy = false;
        _order.Clear();
        _visitors.Clear();
    }
    public bool SetVisitors(List<Visitor> newVisitors)
    {
        if (IsBusy) return false;
        _visitors = newVisitors;
        IsBusy = true;
        return true;
    }
    public bool MakeOrder(Order prodToOrder)
    {
        if (!IsBusy) return false;
        _order.Add(prodToOrder);
        return true;
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder($"Столик {Id}\nОбслуживающий : {ServicePerson}\nТекущий заказ\n");
        foreach (var product in _order)
        {
            sb.Append($"- {product}\n");
        }
        sb.Append($"Текущие Посетители \n");
        foreach (var visitor in _visitors)
        {
            sb.Append($"- {visitor}\n");
        }
        return sb.ToString();
    }
}