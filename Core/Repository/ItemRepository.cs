using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository
{
    public class ItemRepository
    {
        public ItemRepository()
        {

        }

        public List<Item> GetItems(string search, FilterCriteria filters = null)
        {
            //Match search string with Title, Author, Summary to retrive list of items. If filtercriteria is passed, match with
            return new List<Item>();
        }

        public List<Item> GetItems( string search)
        {
            //Match search string with Title, Author, Summary to retrive list of items.
            return new List<Item>();
        }

    }

    public class FilterCriteria
    {
        public List<string> Genres { get; set; }

        public List<AgeGroup> AgeGroups { get; set; }

        public List<string> Languages { get; set; }

        public List<Condition> Conditions { get; set; }

    }
}
