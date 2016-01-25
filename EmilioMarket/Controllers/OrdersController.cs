using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using EmilioMarket.ViewModels;
using EmilioMarket.Models;

namespace EmilioMarket.Controllers
{
    public class OrdersController : Controller
    {
        //Me conecto a la BD:
        EmilioMarketContext db = new EmilioMarketContext();
        // GET: Orders
        public ActionResult NewOrder()
        {
            var orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.ProductOrders = new List<ProductOrder>();

            //Utilizo session para guardar los datos, en el viwbag no los puedo guuardar porque es efimero,
            // solo muestra los datos y no los gretiene o guarda:
            Session["orderView"] = orderView;//Almaceno los datos: 

            //para mandar la lista a los cientes:
            var list = db.Customers.ToList();
            list.Add(new Customer { CustomerId = 0, FirstName = "[Seleccione un Cliente.....]"});
            ViewBag.CustomerId = new SelectList(list.OrderBy(c => c.LastName), "CustomerId", "FullName");
          

            return View(orderView);
        }

        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {
            //Recupero o leo los datos(objetos) del la session:
             orderView = Session["orderView"] as OrderView;//hago un casting
             
            var customerId = int.Parse(Request["CustomerId"]);

            if (customerId == 0)
            {
                //para mandar la lista a los cientes:
                var list1 = db.Customers.ToList();
                list1.Add(new Customer { CustomerId = 0, FirstName = "[Seleccione un Cliente.....]" });
                ViewBag.CustomerId = new SelectList(list1.OrderBy(c => c.LastName), "CustomerId", "FullName");  
                 ViewBag.Error = "Debe Seleccionar un Cliente.....";
                
                return View(orderView);
            }

            var customer = db.Customers.Find(customerId);
            if (customer == null)
            {
                //para mandar la lista a los cientes:
                var list1 = db.Customers.ToList();
                list1.Add(new Customer { CustomerId = 0, FirstName = "[Seleccione un Cliente.....]" });
                ViewBag.CustomerId = new SelectList(list1.OrderBy(c => c.LastName), "CustomerId", "FullName");

                ViewBag.Error = "El Cliente no Existe.....";
                
                return View(orderView);
            }
            //validando detalles del producto: lo tienen que seleccionar
            if (orderView.ProductOrders.Count == 0)
            {
                //para mandar la lista a los cientes:
                var list1 = db.Customers.ToList();
                list1.Add(new Customer { CustomerId = 0, FirstName = "[Seleccione un Cliente.....]" });
                ViewBag.CustomerId = new SelectList(list1.OrderBy(c => c.LastName), "CustomerId", "FullName");

                ViewBag.Error = "Debe Ingresar Detalle del Producto.....";

                return View(orderView);
            }

            var orderId = 0;
            //Manejo transacsional:
            //Start Transction:
            //Inyectar error para prueba de la transaction:
            int i = 0;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order
                    {
                        CustomerId = customerId,
                        OrderDate = DateTime.Now,
                        OrderStatus = OrderStatus.Created
                    };

                    db.Orders.Add(order);
                    db.SaveChanges();

                    //onsulta linq para obtner el maximo de la tabla orders:
                    orderId = db.Orders.ToList().Select(o => o.OrderId).Max();

                    foreach (var item in orderView.ProductOrders)
                    {
                        var orderDetail = new OrderDetail
                        {
                            ProductId = item.ProductId,
                            Descripcion = item.Descripcion,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            OrderId = orderId
                        };

                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();

                        //Error provocado para probar transaction
                        //i++;
                        //if (i > 2)
                        //{
                        //    int a = 0;
                        //    i /= a;

                        //}

                    }

                    //Confirmo la transaction;
                    transaction.Commit();

                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    ViewBag.Error = "ERROR: " + ex.Message;
                  

                    //para mandar la lista a los cientes:
                    var listC = db.Customers.ToList();
                    listC.Add(new Customer { CustomerId = 0, FirstName = "[Seleccione un Cliente.....]" });
                    ViewBag.CustomerId = new SelectList(listC.OrderBy(c => c.LastName), "CustomerId", "FullName");

                    return View(orderView);
                }     

           
          }//end Transaction

            ViewBag.Message = string.Format("La orden: {0}, Grabada OK.", orderId);

            //para mandar la lista a los cientes:
            var list = db.Customers.ToList();
            list.Add(new Customer { CustomerId = 0, FirstName = "[Seleccione un Cliente.....]" });
            ViewBag.CustomerId = new SelectList(list.OrderBy(c => c.LastName), "CustomerId", "FullName");

            orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.ProductOrders = new List<ProductOrder>();
            Session["orderView"] = orderView;

            return View(orderView);
        }

        public ActionResult AddProduct()
        {
            //para mandar la lista a los cientes:
            var list = db.Products.ToList();
            list.Add(new Product { ProductId = 0, Descripcion = "[Seleccione un Producto.....]" });
            ViewBag.ProductId = new SelectList(list.OrderBy(p => p.Descripcion), "ProductId", "Descripcion");


            return View(); 
        }

        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            //Recupero o leo los datos(objetos) del la session:
            var orderView = Session["orderView"] as OrderView;//hago un casting

            //Validar si selelcionar o mandaron un producto un producto:
            //y recupero el ProductId:
            var productId = int.Parse(Request["ProductId"]);
            
            if (productId == 0)
            {
                //para mandar la lista a los cientes:
                var list = db.Products.ToList();
                list.Add(new Product { ProductId = 0, Descripcion = "[Seleccione un Producto.....]" });
                ViewBag.ProductId = new SelectList(list.OrderBy(p => p.Descripcion), "ProductId", "Descripcion");
                ViewBag.Error = "Debe Seleccionar un Producto.....";

                return View(productOrder);
            }

            var product = db.Products.Find(productId);
            
            if (productId == null)
            {
                //para mandar la lista a los clientes:
                var list = db.Products.ToList();
                list.Add(new Product { ProductId = 0, Descripcion = "[Seleccione un Producto.....]" });
                ViewBag.ProductId = new SelectList(list.OrderBy(p => p.Descripcion), "ProductId", "Descripcion");
                ViewBag.Error = "Producto no Existe.....";

                return View(productOrder);
            }

            //Busco si el producto existe:
            productOrder = orderView.ProductOrders.Find(p => p.ProductId == productId);
            if (productOrder == null)
            {        
                 productOrder = new ProductOrder
                {
                Descripcion = product.Descripcion,
                Price = product.Price,
                ProductId = product.ProductId,
                Quantity = decimal.Parse(Request["Quantity"])
                
                };

                orderView.ProductOrders.Add(productOrder);
            }
            else
            {
                productOrder.Quantity += decimal.Parse(Request["Quantity"]);
            } 
            
            //para mandar la lista a los cientes:
            var list1 = db.Customers.ToList();
            list1.Add(new Customer { CustomerId = 0, FirstName = "[Seleccione un Cliente.....]" });
            ViewBag.CustomerId = new SelectList(list1.OrderBy(c => c.LastName), "CustomerId", "FullName");

            return View("NewOrder", orderView);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}