namespace Inventory.Services;

using Products.Models;

public class ProductService
{

    private List<Product> _products = new();
    private int _id = 0;

    public Product AddProduct(string name, int quantity, decimal price)
    {
        //auto increment id
        _id += 1;
        Product product = new Product();
        product.Id = _id;
        product.Name = name;
        product.Quantity = quantity;
        product.Price = price;
        _products.Add(product);
        return product;
    }

    public List<Product> GetAllProducts()
    {
        return _products;
    }

    public Product? GetProductById(int id)
    {
        //to return one product, LINQ Where returns a collection
        //if not found return null
        var result = _products.FirstOrDefault(p => id == p.Id);
        return result;
    }

    //possible nulls to signal if user wants to update or not, if null => keep old value
    public bool UpdateProduct(int id, string? name = null, int? quantity = null, decimal? price = null)
    {
        foreach (var product in _products)
        {
            if (product.Id == id)
            {
                if (name != null) product.Name = name;
                if (quantity.HasValue) product.Quantity = quantity.Value;
                if (price.HasValue) product.Price = price.Value;
                return true;
            }
        }
        return false;
    }

    public bool DeleteProduct(int id)
    {
        for (int i = 0; i < _products.Count; i++)
        {
            if (_products[i].Id == id)
            {
                _products.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
}
