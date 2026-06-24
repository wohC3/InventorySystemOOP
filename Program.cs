using Inventory.Services;
using Products.Models;

class Program
{
    static void Main(string[] args)
    {

        ProductService service = new ProductService();

        bool menuRunning = true;

        while (menuRunning)
        {
            Console.WriteLine("Welcome to the inventory system! \n Choose 1 to add a product. \n Choose 2 to view all products. \n choose 3 to search for a product by Id. \n Choose 4 to update an existing product. \n Choose 5 to delete a product. \n Choose 6 to exit the program");
            int userInput;
            bool isInt = int.TryParse(Console.ReadLine(), out userInput);
            if (!isInt || userInput < 1 || userInput > 6)
            {
                Console.WriteLine("Invalid input, please enter a number 1-6!(Example: 1)");
                Console.WriteLine("press ENTER to go back to menu");
                Console.ReadLine();
                Console.Clear();
                continue;
            }
            switch (userInput)
            {
                case 1:
                    string productName = GetStringInput("Enter product name");
                    int productQuantity = GetIntInput("Enter product quantity"); ;
                    decimal productPrice = GetDecimalInput("Enter product price");

                    service.AddProduct(productName, productQuantity, productPrice);

                    Console.WriteLine("Successfully added item!");
                    Console.WriteLine("Press ENTER to return to menu");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 2:
                    var productsList = service.GetAllProducts();
                    if (productsList.Count <= 0)
                    {
                        Console.WriteLine("No products have been added yet");
                        break;
                    }
                    foreach (var result in productsList)
                    {
                        PrintProduct(result);
                    }
                    Console.WriteLine("Press ENTER to return to menu");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 3:

                    int inputId = GetIntInput("Enter id to search for a specific product");
                    var product = service.GetProductById(inputId);
                    if (product != null)
                    {
                        PrintProduct(product);
                    }
                    else
                    {
                        Console.WriteLine("No product with such id has been found!");
                    }
                    Console.WriteLine("Press ENTER to return to menu");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 4:
                    int inputIdUpdate = GetIntInput("Enter id to update specific product");

                    var productUpdate = service.GetProductById(inputIdUpdate);
                    if (productUpdate == null)
                    {
                        Console.WriteLine("No product with such id has been found!");
                        Console.WriteLine("Press ENTER to return to menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    string? productNameUpdate = GetOptionalStringInput("Enter product name \n(leave empty to keep old value)");
                    int? productQuantityUpdate = GetOptionalIntInput("Enter product quantity \n(leave empty keep old value)");
                    decimal? productPriceUpdate = GetOptionalDecimalInput("Enter product price \n(leave empty to keep old value)");


                    bool success = service.UpdateProduct(inputIdUpdate, productNameUpdate, productQuantityUpdate, productPriceUpdate);
                    if (success)
                    {
                        Console.WriteLine("Update successful!");
                    }
                    else
                    {
                        Console.WriteLine("Update failed!");
                    }

                    Console.WriteLine("Press ENTER to return to menu");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 5:
                    int inputIdDelete = GetIntInput("Enter id to delete specific product");
                    if (service.GetProductById(inputIdDelete) == null)
                    {
                        Console.WriteLine("No product with such id has been found!");
                        Console.WriteLine("Press ENTER to return to menu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    service.DeleteProduct(inputIdDelete);
                    Console.WriteLine("Delete successful");

                    Console.WriteLine("Press ENTER to return to menu");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 6:
                    menuRunning = false;
                    break;
            }
        }
    }

    static void PrintProduct(Product product)
    {
        Console.WriteLine($"Id: {product.Id}");
        Console.WriteLine($"Name: {product.Name}");
        Console.WriteLine($"Quantity: {product.Quantity}");
        Console.WriteLine($"Price: {product.Price}");
        Console.WriteLine("************************");
    }

    static string GetStringInput(string message)
    {
        Console.WriteLine(message);
        //fix possible null value warning
        string input = Console.ReadLine() ?? "";
        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid name input(example Bob). Try again: ");
            input = Console.ReadLine() ?? "";
        }
        return input;
    }
    static int GetIntInput(string message)
    {
        int input;
        Console.WriteLine(message);
        while (!int.TryParse(Console.ReadLine(), out input))
        {
            Console.WriteLine("Invalid number(example 10). Try again: ");
        }
        return input;
    }
    static decimal GetDecimalInput(string message)
    {
        decimal input;
        Console.WriteLine(message);
        while (!decimal.TryParse(Console.ReadLine(), out input))
        {
            Console.WriteLine("Invalid number(example 1.1). Try again: ");
        }
        return input;
    }

    static string? GetOptionalStringInput(string message)
    {
        Console.WriteLine(message);
        string input = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }
        return input;
    }

    static int? GetOptionalIntInput(string message)
    {
        Console.WriteLine(message);
        string input = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }
        int result;
        while (!int.TryParse(input, out result))
        {



            Console.WriteLine("Invalid number(example 10). \n(Leave empty to keep old value)");
            input = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(input))
                return null;
        }
        return result;
    }


    static decimal? GetOptionalDecimalInput(string message)
    {
        Console.WriteLine(message);
        string input = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }
        decimal result;
        while (!decimal.TryParse(input, out result))
        {
            Console.WriteLine("Invalid number(example 1.1). \n(Leave empty to keep old value)");
            input = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(input))
                return null;
        }
        return result;
    }


}
