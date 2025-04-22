using System;
using System.Web.Mvc;
using System.Collections.Generic;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using SuitCustomizer.Dnn.Dnn.Team5050.SuitCustomizer.Models;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;

namespace SuitCustomizer.Dnn.Dnn.Team5050.SuitCustomizer.Controllers
{
    [DnnHandleError]
    public class SuitOrderController : DnnController
    {
        [HttpPost]
        // CSRF ellenőrzés kikapcsolva a hibaelhárítás miatt
        // [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult AddToCart(SuitCartItem cartItem)
        {
            try
            {
                // Az öltöny adatait Session-ben tároljuk ideiglenesen
                Session["SuitConfiguration"] = cartItem;
                
                // A kosárhoz adás URL-jét adjuk vissza
                // A SuitOrder/CompleteOrder útvonalhoz irányít
                string redirectUrl = Url.Action("CompleteOrder", "SuitOrder", new {
                    fabric = cartItem.FabricId,
                    style = cartItem.Style,
                    price = cartItem.BasePrice + cartItem.ExtraPrice
                });
                
                // A TempData-t használjuk a konfigurációs adatok átadására
                TempData["SuitConfiguration"] = cartItem;
                
                // Ellenőrizzük, hogy AJAX hívás történt-e
                if (Request.IsAjaxRequest())
                {
                    // AJAX kérés esetén JSON válasz
                    return Json(new { Success = true, RedirectUrl = redirectUrl });
                }
                else
                {
                    // Nem AJAX kérés esetén átirányítás
                    return Redirect(redirectUrl);
                }
            }
            catch (Exception ex)
            {
                // Hiba esetén
                if (Request.IsAjaxRequest())
                {
                    // AJAX kérés esetén JSON hibaüzenet
                    return Json(new { Success = false, Error = ex.Message });
                }
                else
                {
                    // Nem AJAX kérés esetén átirányítás az Index oldalra hibaüzenettel
                    TempData["ErrorMessage"] = "Hiba történt a kosárba helyezéskor: " + ex.Message;
                    return RedirectToAction("Index", "Item");
                }
            }
        }
        
        // Második metódus, amely kifejezetten JSON-t kezel
        [HttpPost]
        public ActionResult AddToCartJson()
        {
            try
            {
                // Az adatok kiolvasása a request-ből
                var reader = new System.IO.StreamReader(Request.InputStream);
                reader.BaseStream.Position = 0;
                var json = reader.ReadToEnd();
                
                // Json deserialization
                var cartItem = Newtonsoft.Json.JsonConvert.DeserializeObject<SuitCartItem>(json);
                
                // Az öltöny adatait Session-ben tároljuk ideiglenesen
                Session["SuitConfiguration"] = cartItem;
                
                // A kosárhoz adás URL-jét adjuk vissza
                string redirectUrl = Url.Action("CompleteOrder", "SuitOrder", new {
                    fabric = cartItem.FabricId,
                    style = cartItem.Style,
                    price = cartItem.BasePrice + cartItem.ExtraPrice
                });
                
                // A TempData-t használjuk a konfigurációs adatok átadására
                TempData["SuitConfiguration"] = cartItem;
                
                // JSON válasz
                return Json(new { Success = true, RedirectUrl = redirectUrl });
            }
            catch (Exception ex)
            {
                // JSON hibaüzenet
                return Json(new { Success = false, Error = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult CompleteOrder(int fabric, string style, decimal price)
        {
            try 
            {
                // Konfigurációs adatok kinyerése a TempData-ból
                var configuration = TempData["SuitConfiguration"] as SuitCartItem;
                
                if (configuration == null)
                {
                    // Ha nincs konfiguráció, akkor egyszerű alapadatokat használunk
                    configuration = new SuitCartItem
                    {
                        FabricId = fabric,
                        Style = style,
                        BasePrice = price,
                        ExtraPrice = 0
                    };
                }
                
                // Adatok továbbadása a nézetnek
                ViewBag.FabricId = fabric;
                ViewBag.Style = style;
                ViewBag.Price = price;
                ViewBag.HotcakesProductId = "SUIT-CUSTOM-BASE";
                ViewBag.Configuration = configuration;
                
                return View();
            }
            catch (Exception ex)
            {
                // Hiba esetén átirányítás a konfigurátor oldalra
                TempData["ErrorMessage"] = "Hiba történt a vásárlás befejezésekor: " + ex.Message;
                return RedirectToAction("Index", "Item");
            }
        }
    }
} 