using LagerSystem.DataAccessLayer;
using LagerSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LagerSystem.Repositories
{
    public class StoreRepository
    {
        private StoreContext Context { set; get; }

        public static SearchSortViewModel DEFAULT = new SearchSortViewModel { SearchName = null, SearchPrice = null, SearchShelf = null, SearchDescription = null, SortVariable = "Name", Descending = false };

        public StoreRepository()
        {
            this.Context = new StoreContext();
        }

        public IEnumerable<StockItem> GetAll()
        {
            return this.Context.Items;
        }

        public StockItem GetItemById(int id)
        {
            return this.Context.Items.Find(id);
        }

        public void AddItem(StockItem item)
        {
            this.Context.Items.Add(item);
            this.Context.SaveChanges();
        }
        public void RemoveItem(int id)
        {
            this.Context.Items.Remove(this.Context.Items.Find(id));
            this.Context.SaveChanges();
        }
        public void EditItem(StockItem item)
        {
            StockItem ItemToBeChanged = this.Context.Items.Find(item.ItemID);

            ItemToBeChanged.Name = item.Name;
            ItemToBeChanged.Price = item.Price;
            ItemToBeChanged.Shelf = item.Shelf;
            ItemToBeChanged.Description = item.Description;

            this.Context.SaveChanges();
        }

        public IEnumerable<StockItem> Search(StockItem target)
        {
            var query = this.Context.Items.Where(i => i.ItemID == target.ItemID ||
                                                 i.Name.Contains(target.Name) ||
                                                 i.Price == target.Price ||
                                                 i.Description.Contains(target.Description) ||
                                                 i.Shelf.Contains(target.Shelf)
                                                 );
            return query;
        }

        public IEnumerable<StockItem> Search(SearchSortViewModel targetSort)
        {
            IEnumerable<StockItem> query = this.Context.Items.Where(i => i.Name.Contains(targetSort.SearchName) ||
                                                      i.Price == targetSort.SearchPrice ||
                                                      i.Description.Contains(targetSort.SearchDescription) ||
                                                      i.Shelf.Contains(targetSort.SearchShelf)
                                                      );
            switch (targetSort.SortVariable)
            {
                case "Name":
                    query = query.OrderBy(i => i.Name);
                    break;
                case "Price":
                    query = query.OrderBy(i => i.Price);
                    break;
                case "Shelf":
                    query = query.OrderBy(i => i.Shelf);
                    break;
                case "Description":
                    query = query.OrderBy(i => i.Description);
                    break;
                default:
                    break;
            }
            if (targetSort.Descending)
            {
                query = query.Reverse();
            }
            return query;
        }

        public IEnumerable<CategoryViewModel> GetCategoryPrototypes()
        {

            return from items in this.Context.Items
                   group items by items.Shelf into groups
                   select new CategoryViewModel { Name = groups.Key, ContentCount = groups.Count() };



        }

        public IEnumerable<StockItem> GetAllInCategory(string category)
        {
            return this.Context.Items.Where(i => i.Shelf == category);
        }

        public IEnumerable<StockItem> GetAllSorted(SortViewModel sorter)
        {
            IEnumerable<StockItem> query = this.Context.Items.Select(i => i);
            if (sorter != null)
            {
                
                switch (sorter.SortVariable)
                {
                    case "Name":
                        query = query.OrderBy(i => i.Name);
                        break;
                    case "Price":
                        query = query.OrderBy(i => i.Price);
                        break;
                    case "Shelf":
                        query = query.OrderBy(i => i.Shelf);
                        break;
                    case "Description":
                        query = query.OrderBy(i => i.Description);
                        break;
                    default:
                        break;
                }/*
                switch (sorter.Descending)
                {
                    case true:
                        //query = query.Reverse();
                        break;
                    case false:
                        break;
                    default:
                        break;
                }*/
                
            }
            if (sorter.Descending) return query.Reverse();
            else return query;
            /*
            if (sorter != null && typeof(StockItem).GetProperties().Select(i => i.Name).Contains(sorter.SortVariable))
            {
                if (sorter.Descending)
                {
                    return this.Context.Items.OrderByDescending(item => item.GetProperty(sorter.SortVariable).GetValue(item, null));
                }
                else
                {
                    return this.Context.Items.OrderBy(item => 
                        typeof(StockItem)
                        .GetProperty(sorter.SortVariable)
                        .GetValue(item, null));
                }
            }
            else
            {
                return this.Context.Items;
            }*/
        }
        public IEnumerable<StockItem> SortedContent (IEnumerable<StockItem> content = null, SortViewModel sorter = null)
        {
            IEnumerable<StockItem> query = content;
            if (content == null)
            {
                query = this.Context.Items;
            }
            
            if (sorter != null)
            {
                
                switch (sorter.SortVariable)
                {
                    case "Name":
                        query = query.OrderBy(i => i.Name);
                        break;
                    case "Price":
                        query = query.OrderBy(i => i.Price);
                        break;
                    case "Shelf":
                        query = query.OrderBy(i => i.Shelf);
                        break;
                    case "Description":
                        query = query.OrderBy(i => i.Description);
                        break;
                    default:
                        break;
                }
                if (sorter.Descending)
                {
                    return query.Reverse();
                }
                else return query;
            }
            
            else return query;
        }
    }
}