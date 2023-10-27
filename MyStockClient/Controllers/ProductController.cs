using System.Net.Http.Headers;
using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyStockClient.Controllers;

public class ProductController : Controller
{
    private readonly HttpClient client = null;
    private string ProductApiUrl = "";
    
    public ProductController()
    {
        client = new HttpClient();
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        ProductApiUrl = "http://localhost:5001/product";
    }
    
    // GET: ProductController
    public async Task<IActionResult> Index()
    {
        HttpResponseMessage productResponse = await client.GetAsync(ProductApiUrl);
        List<Product>? listProducts = new List<Product>();
        if (productResponse.StatusCode == System.Net.HttpStatusCode.OK)
        {
            listProducts = await productResponse.Content.ReadFromJsonAsync<List<Product>>();
        }
        return View(listProducts);
    }
    
    // GET: ProductController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        HttpResponseMessage productResponse = await client.GetAsync(ProductApiUrl + $"/{id}");
        Product? product = new Product();
        if (productResponse.StatusCode == System.Net.HttpStatusCode.OK)
        {
            product = await productResponse.Content.ReadFromJsonAsync<Product>();
        }
        return View(product);
    }
    
    // GET: ProductController/Create
    public async Task<ActionResult> Create()
    {
        return View();
    }
    
    // POST: ProductController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Product p)
    {
        HttpResponseMessage response =
            await client.PostAsJsonAsync(ProductApiUrl, p);
        if (ModelState.IsValid)
        {
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
        }

        return Redirect("Create");
    }
    
    // GET: ProductController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        HttpResponseMessage productResponse = await client.GetAsync(ProductApiUrl + $"/{id}");
        Product product = new Product();
        if (productResponse.StatusCode == System.Net.HttpStatusCode.OK)
        {
            product = productResponse.Content.ReadFromJsonAsync<Product>().Result;
        }

        return View(product);
    }
    
    // POST: ProductController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, Product p)
    {
        if (ModelState.IsValid)
        {
            await client.PutAsJsonAsync(ProductApiUrl + $"/{p.ProductId}", p);
            return RedirectToAction("Index");
        }

        return View(p);
    }
    
    // POST: ProductController/Delete/5
    [HttpDelete]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id)
    {
        await client.DeleteAsync(ProductApiUrl + $"/{id}");
        return RedirectToAction("Index");
    }
}