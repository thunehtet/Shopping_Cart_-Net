using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Shopping.Data;
using Shopping.Models;
using System.IO.Pipes;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;
using static Login_CA.Filters.LoginFilter;

namespace Shopping.Controllers
{
    public class CartController : Controller
    {
        //AddCart Action

        [HttpPost]
        public IActionResult AddCart(int productID, int customerID)
        {
            List<Purchase> purchases = PurchaseData.GetPurchaseList();
            foreach (Purchase pur in purchases)
            {
                if (pur.ProductID == productID)
                {
                    int temp = pur.NumberOfPurchase;
                    temp++;
                    Purchase p = new Purchase
                    {
                        ProductID = productID,
                        CustomerID = customerID,
                        NumberOfPurchase = temp
                    };
                    PurchaseData.UpdateProduct(p);
                }

                else
                {
                    Purchase p = new Purchase
                    {
                        ProductID = productID,
                        CustomerID = customerID,
                        NumberOfPurchase = 1

                    };
                    PurchaseData.AddProduct(p);
                }
            }
            return View();
        }

        public IActionResult Index() 
        {
            
            var userId = HttpContext.Session.GetString("id");
            if (userId == null)
            {
                List<Purchase> purs = PurchaseData.PurchaseHistory(0);
                return View(purs);

            }
            else
            {
                List<Purchase> purs = PurchaseData.PurchaseHistory(Int32.Parse(userId));
                return View(purs);
            }
        }

        public IActionResult AddviewCart(string name, string text,string img,string price,int productID)
        {
            int p = Int32.Parse(price.Substring(1));
            ViewCart view = new ViewCart
            {
                ProductID = productID,
                Description = text,
                Image = img,
                Price = p,
                Name= name,
                NumberOfPurchase= 1

            };
            ViewCartData.AddViewCart(view);
            return View();
        }

        public IActionResult Gettime(int productid, int time)
        {
            var total = 0;
  
            Dictionary<int, int> data =JsonSerializer.Deserialize <Dictionary<int,int>>(HttpContext.Session.GetString("map"));

            if (data.ContainsKey(productid))
            {
                data[productid] = time;
            }
            HttpContext.Session.SetString("map", JsonSerializer.Serialize(data)); 
            
            List<ViewCart> view = ViewCartData.GetViewCart();

            foreach (var vc in view)
            {
                
                if (data.ContainsKey(vc.ProductID))
                {
                    total += data[vc.ProductID] * vc.Price;
                }
        
            }
            return Json(new { total });
           
        }

        //ViewCart Page
        public IActionResult ViewCart()
        {
            var total = 0;
            Dictionary<int,int> map = new Dictionary<int,int>();
           
            List<ViewCart> view = ViewCartData.GetViewCart(); 
           
            foreach(var vc in view)
            {
                //Calculating the total value
                total += vc.Price * vc.NumberOfPurchase;
                map.Add(vc.ProductID, vc.NumberOfPurchase);
            }
            HttpContext.Session.SetString("map", JsonSerializer.Serialize(map));
            ViewData["view"] = view;
            ViewData["total"] = total;
            return View();

        }

        //Checkout Function
        [MustLogin]
        public IActionResult Checkout()
        {
            var id=HttpContext.Session.GetInt32("id");
            if (id == null)
            {
                string url = Request.GetEncodedUrl();
                HttpContext.Session.SetString("returnUrl", url);
                return RedirectToAction("Login", "Login");
            }
                       
            Random rnd = new Random();
            Dictionary<int, int> data = JsonSerializer.Deserialize<Dictionary<int, int>>(HttpContext.Session.GetString("map"));
            

            foreach (KeyValuePair<int,int> c in data)
            {
                   

                CheckOut ck = new CheckOut
                {
                    ProductID = c.Key,
                    CustomerID = Convert.ToInt32(id),
                    Date = DateTime.Now,
                    Quantity = c.Value,
                };

                string ActivationCode = "";
                for (var i = 1; i <= ck.Quantity; i++)
                {
                    //Generating the random Activation Code
                    ActivationCode += Guid.NewGuid().ToString() + ",";
                 }
                //substring last common ,
                ActivationCode = ActivationCode.Substring(0, ActivationCode.Length - 1);
                ck.ActivationCode = ActivationCode;
                CheckOutData.AddCheckOut(ck);


            }
          
            //Upon Checkout, will delete the data from CartData for the user 
            ViewCartData.Delete();

            return RedirectToAction("Purchase", "Browser");
        }

    }
}
