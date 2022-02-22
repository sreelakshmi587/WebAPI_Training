using ProductLogWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductLogWebAPI.Controllers
{
    public class ProductController : ApiController
    {
        public IHttpActionResult GetAllProducts()
        {
            IList<ProductViewModel> products = null;

            using (var db = new ProductEntities())
            {
                products = db.Product_details
                            .Select(P => new ProductViewModel()
                            {
                                Id = P.Id,
                                Product_Name = P.Product_Name,
                                Product_Code = P.Product_Code,
                                Description = P.Description,
                                Price = P.Price
                            }).ToList<ProductViewModel>();
            }

            if (products.Count == 0)
            {
                return NotFound();
            }

            return Ok(products);
        }
        public IHttpActionResult GetProductById(int id)
        {
            ProductViewModel products = null;

            using (var db = new ProductEntities())
            {
                products = db.Product_details
                    .Where(p => p.Id == id)
                    .Select(p => new ProductViewModel()
                    {
                        Id = p.Id,
                        Product_Name = p.Product_Name,
                        Product_Code = p.Product_Code,
                        Description = p.Description,
                        Price = p.Price
                    }).FirstOrDefault<ProductViewModel>();
            }

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }
        public IHttpActionResult PostNewProducts(ProductViewModel products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            using (var db = new ProductEntities())
            {
                db.Product_details.Add(new Product_details()
                {
                    Id = products.Id,
                    Product_Name = products.Product_Name,
                    Product_Code = products.Product_Code,
                    Description = products.Description,
                    Price = products.Price
                });
                db.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult PutProduct(ProductViewModel product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var db = new ProductEntities())
            {
                var existingProduct = db.Product_details.Where(p => p.Id == product.Id)
                                                        .FirstOrDefault<Product_details>();

                if (existingProduct != null)
                {
                    existingProduct.Product_Name = product.Product_Name;
                    existingProduct.Product_Code = product.Product_Code;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid product id");

            using (var db = new ProductEntities())
            {
                var product = db.Product_details
                    .Where(p => p.Id == id)
                    .FirstOrDefault();

                db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return Ok();
        }

    }
}
