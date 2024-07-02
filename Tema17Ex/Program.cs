using Tema17Ex;
using Tema17Ex.Data;

using (var context = new StoreContext())
{
    DbHelper.SeedDatabase();

    DbHelper.AddCategory(context, "Toys", "https://m.media-amazon.com/images/I/81iMfZ9ZqXL._AC_UF894,1000_QL80_.jpg");
    DbHelper.AddManufacturer(context, "Toys Manufacturer", "789 Boulevard", "CUI789");

    DbHelper.AddProduct(context, "Toy Car", 50, 3, 3);
    DbHelper.AddProduct(context, "Stuffed Animal", 30, 3, 3);
    DbHelper.AddProduct(context, "TV", 100, 1, 1);
    DbHelper.AddProduct(context, "C# for Dummies", 120, 2, 2);

    DbHelper.UpdateProductPrice(context, 3, 200);
    DbHelper.UpdateProductPrice(context, 1, 75);

    Console.WriteLine($"Total stock value: {DbHelper.GetTotalStockValue(context)}");
    Console.WriteLine($"Stock value by manufacturer 3: {DbHelper.GetStockValueByManufacturer(context, 3)}");
    Console.WriteLine($"Stock value by category 1: {DbHelper.GetStockValueByCategory(context, 1)}");
}

Console.WriteLine("Operations completed.");

        