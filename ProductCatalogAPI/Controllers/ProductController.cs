using Microsoft.AspNetCore.Mvc;
using ProductCatalogAPI.Method;

[Route("api/products")]
[ApiController]

public class ProductController : ControllerBase
{
    private static List<Product> products = new List<Product>();

    // Get all products
    [HttpGet]
    public ActionResult<List<Product>> GetAll() => products;
    // Same as:
    //public ActionResult<List<Product>> GetAll()
    //{
    //    return Ok(products);
    //}

    // Get a product by ID
    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        return product == null ? NotFound() : Ok(product);
    }
    // Same as:
    //public ActionResult<Product> GetById(int id)
    //{
    //    // Look for a product that matches the given ID
    //    Product foundProduct = null;

    //    // Check each product one by one
    //    foreach (var product in products)
    //    {
    //        // Convert the ID to string since product.Id is string
    //        if (product.Id == id.ToString())
    //        {
    //            foundProduct = product; // We found it!
    //            break; // No need to keep looking
    //        }
    //    }

    //    // Did we find the product?
    //    if (foundProduct == null)
    //    {
    //        return NotFound(); // Say "not found"
    //    }

    //    return Ok(foundProduct); // Say "here's the product"
    //}

    // Create a new product
    [HttpPost]
    public ActionResult<Product> Create(Product newProduct)
    {
        newProduct.Id = products.Count + 1;
        products.Add(newProduct);
        return CreatedAtAction(
            nameof(GetById),         // 1. Which method can retrieve this item
            new { id = newProduct.Id }, // 2. The parameters needed for that method  
            newProduct                  // 3. The actual created item to return
        );
    }

    // Update an existing product
    [HttpPut("{id}")]
    public ActionResult Update(int id, Product updatedProduct)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        return Ok(product);
    }

    // Delete a product
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        products.Remove(product);
        return NoContent();
    }
}

