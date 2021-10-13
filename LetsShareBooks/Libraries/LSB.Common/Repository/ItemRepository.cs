using LSB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSB.Repository
{
    public class ItemRepository
    {
        public ItemRepository()
        {

        }

        public async Task<List<Item>> GetItemsAsync(string search, FilterCriteria filters = null)
        {
            //Match search string with Title, Author, Summary to retrive list of items. If filtercriteria is passed, match with
            List<Item> items = new List<Item>();
            return items;
        }

        public async Task<List<Item>> GetItemsAsync(string search)
        {
            //Match search string with Title, Author, Summary to retrive list of items.
            return new List<Item>();
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return new Item();
        }

        public async Task<Item> CreateItemAync(Item item)
        {
            return item;
        }

        public async Task<Item> UpdateItemAync(string id,Item item)
        {
            return item;
        }
        public async Task DeleteItemAync(string id)
        {
            return;
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
