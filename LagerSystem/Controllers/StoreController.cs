using LagerSystem.Models;
using LagerSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LagerSystem.Controllers
{
    public class StoreController : Controller
    {
        private StoreRepository repo = new StoreRepository();

        // GET: Store
        public ActionResult Index(SortViewModel sorter = null)
        {
            if (sorter == null)
            {
                ViewBag.Sorter = new SortViewModel("Name", false);
            }
            else
            {
                ViewBag.Sorter = sorter;
            } 
            return View(repo.GetAllSorted(sorter));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Price,Shelf,Description")] StockItem item)
        {
            if (ModelState.IsValid)
            {
                repo.AddItem(item);
                return RedirectToAction("Detials", new { id = item.ItemID });
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(repo.GetItemById(id.Value));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,Name,Price,Shelf,Description")] StockItem item)
        {
            if (ModelState.IsValid)
            {
                repo.EditItem(item);
                return RedirectToAction("Detials", new { id = item.ItemID });
            }
            return View();
        }
        public ActionResult Remove(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(repo.GetItemById(id.Value));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id)
        {
            repo.RemoveItem(id);
            return RedirectToAction("Index");
        }
        public ActionResult Search()
        {
            return View(StoreRepository.DEFAULT);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "SearchName,SearchPrice,SearchShelf,SearchDescription")] SearchSortViewModel targetSort)
        {
            targetSort.SortVariable = "Name";
            targetSort.Descending = false;
            ViewBag.SearchSorter = targetSort;
            return View("Results", repo.Search(targetSort));
        }

        public ActionResult Results (SearchSortViewModel targetSort)
        {
            ViewBag.SearchSorter = targetSort;
            return View(repo.Search(targetSort));
        }

        public ActionResult Detials(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(repo.GetItemById(id.Value));
        }

        public ActionResult Categories()
        {
            return View(repo.GetCategoryPrototypes());
        }

        public ActionResult Category(string shelf)
        {
            if (shelf != null)
            {
                ViewBag.Shelf = shelf;
                SearchSortViewModel searchSorter = new SearchSortViewModel(){SearchShelf = shelf};
                ViewBag.SearchSorter = searchSorter;
                return View("Results", repo.Search(searchSorter));
            }
            else return RedirectToAction("Index");
        }
    }
}