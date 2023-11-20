using System.Text;

namespace LabWork27_buffet.Models;

public class Order
{
    public int Id { get;  }
    private readonly Dictionary<Product,int> _products = new ();
    public IReadOnlyList<Product> Products => _products.Keys.ToList();
    private static int _id = 1;
    public Order()
    {
        Id = _id;
        _id++;
    }

    public void AddProdToOrder(Product product)
    {
        if (!_products.ContainsKey(product))
        {
            _products.Add(product,1);
            return;
        }
        _products[product]++;
    }

    public override string ToString()
    {
        var sb = new StringBuilder($"Заказ номер {Id}\n");
        foreach (var kProduct in _products)
        {
            sb.Append($"- {kProduct.Key} : {kProduct.Value}\n");
        }
        return sb.ToString();
    }
}