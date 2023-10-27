using BusinessObjects.Models;
using DataAccess;

namespace Repositories;

public class ProductRepository : IProductRepository
{
    public void SaveProduct(Product p) => ProductDAO.SaveProduct(p);

    public Product GetProductById(int id) => ProductDAO.FindProductById(id);

    public void DeleteProduct(Product p) => ProductDAO.DeleteProduct(p);

    public void UpdateProduct(Product p) => ProductDAO.UpdateProduct(p);

    public List<Product> GetProducts() => ProductDAO.GetProducts();
}