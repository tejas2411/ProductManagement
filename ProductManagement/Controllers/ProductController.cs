using ProductManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        ProductDBEntities2 dbobj = new ProductDBEntities2();
        public ActionResult Product(ProductList LIST)
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddProduct(ProductList model) //data from form will be received in model object.
        {

            if(ModelState.IsValid)
            {
                //object created to insert data into my table.
                ProductList list = new ProductList();

                list.ProductID = model.ProductID;
                list.ProductName = model.ProductName;
                list.CategoryID = model.CategoryID;
                list.CategoryName = model.CategoryName;


                if(model.ProductID==0)
                {
                    dbobj.ProductLists.Add(list);
                    dbobj.SaveChanges();
                }
                else
                {
                    dbobj.Entry(list).State = EntityState.Modified;
                    dbobj.SaveChanges();
                }

               
                
            }
            ModelState.Clear();
            return View("Product");

        }


        public ActionResult Productlist()
        {
            var tab = dbobj.ProductLists.ToList();

            return View(tab);
        }


        public ActionResult Delete (int id)
        {
            var tab = dbobj.ProductLists.Where(x => x.ProductID == id).FirstOrDefault();
            dbobj.ProductLists.Remove(tab);
            dbobj.SaveChanges();

            var Updatedlist = dbobj.ProductLists.ToList();

            return View("Productlist",Updatedlist);
        }

    }
}