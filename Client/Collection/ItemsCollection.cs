using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Collection
{
    class ItemsCollection
    {
        public List<AbstractItem> ItemList = new List<AbstractItem>();
        public void Add(AbstractItem abst)
        {
            ItemList.Add(abst);
        }
        public Book GetISBN(Guid isbn)
        {
            return (Book)ItemList.FirstOrDefault(i => i is Book b && b.ISBN == isbn);
        }

        public List<AbstractItem> SearchBySN(Guid sn)
        {
            List<AbstractItem> toReturn = new List<AbstractItem>();
            foreach (var item in ItemList)
            {
                if (item.ISBN == sn)
                    toReturn.Add(item);
            }

            return toReturn;
        }
        public List<AbstractItem> searchListByTitle(string strSearchByName)
        {
            List<AbstractItem> toReturn = new List<AbstractItem>();

            foreach (var item in ItemList)
            {
                if (item.Title.Contains(strSearchByName))
                    toReturn.Add(item);
            }

            return toReturn;
        }
    }
}
